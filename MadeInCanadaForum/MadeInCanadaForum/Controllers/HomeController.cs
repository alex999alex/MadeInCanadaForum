using MadeInCanadaForum.Data;
using MadeInCanadaForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using MadeInCanadaForum.Data;

namespace MadeInCanadaForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly MadeInCanadaForumContext _context;
        public HomeController(MadeInCanadaForumContext context)
        {
            _context = context;
        }

        //Home page - all discussions - ../ or ../Home/Index
        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussion
                .Include(d => d.Comments)
                .ToListAsync();
            return View(discussions);
        }

        //Discussion Page - one discussion - ../Home/DiscussionDetails/id
        public async Task<IActionResult> DiscussionDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get the discussion by id from database
            var discussion = await _context.Discussion
                .Include(d => d.Comments)
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        //Privacy page
        public IActionResult Privacy()
        {
            return View();
        }
    }
}

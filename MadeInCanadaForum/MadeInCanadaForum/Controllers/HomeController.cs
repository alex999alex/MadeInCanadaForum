using MadeInCanadaForum.Data;
using MadeInCanadaForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MadeInCanadaForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly MadeInCanadaForumContext _context;
        public HomeController(MadeInCanadaForumContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussion
                .Include(d => d.Comments)
                .ToListAsync();
            return View(discussions);
        }


        public async Task<IActionResult> DiscussionDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

  
            var discussion = await _context.Discussion
                .Include(d => d.Comments)
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
}

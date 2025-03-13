using MadeInCanadaForum.Data;
using MadeInCanadaForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MadeInCanadaForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly MadeInCanadaForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(MadeInCanadaForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussion
                .Where(d => d.IsVisible)
                .Include(d => d.Comments)
                .Include(d => d.ApplicationUser)
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
                .ThenInclude(c => c.ApplicationUser)
                .Include(d => d.ApplicationUser)
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        public async Task<IActionResult> Profile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var discussions = await _context.Discussion
                .Where(d => d.ApplicationUserId == id && d.IsVisible)
                .Include(d => d.Comments)
                .ToListAsync();

            ViewBag.User = user;
            return View(discussions);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

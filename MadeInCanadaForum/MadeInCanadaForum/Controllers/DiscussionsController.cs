using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MadeInCanadaForum.Data;
using MadeInCanadaForum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MadeInCanadaForum.Controllers
{
    [Authorize]
    public class DiscussionsController : Controller
    {
        private readonly MadeInCanadaForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DiscussionsController(MadeInCanadaForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Discussions
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var discussions = await _context.Discussion
                .Where(d => d.ApplicationUserId == userId)
                .Include(d => d.ApplicationUser)
                .Include(d => d.Comments)
                .ToListAsync();
            return View(discussions);
        }

        // GET: Discussions/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
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

        // GET: Discussions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discussions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscussionId,Title,Content,Location,Camera,ImageFile,IsVisible,CreateDate")] MadeInCanadaForum.Models.Discussion discussion)
        {
            discussion.ApplicationUserId = _userManager.GetUserId(User);
            discussion.IsVisible = true;
            discussion.CreateDate = DateTime.Now;

            if (discussion.ImageFile != null)
            {
                discussion.ImageFilename = Guid.NewGuid().ToString() + Path.GetExtension(discussion.ImageFile.FileName);
            }

            if (ModelState.IsValid)
            {
                _context.Add(discussion);
                await _context.SaveChangesAsync();

                if (discussion.ImageFile != null)
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", discussion.ImageFilename);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await discussion.ImageFile.CopyToAsync(fileStream);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(discussion);
        }

        // GET: Discussions/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            
            // Check if the current user is the owner of the discussion
            var userId = _userManager.GetUserId(User);
            if (discussion.ApplicationUserId != userId)
            {
                return Forbid();
            }
            
            return View(discussion);
        }

        // POST: Discussions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscussionId,Title,Content,ImageFile")] Discussion discussion)
        {
            if (id != discussion.DiscussionId)
            {
                return NotFound();
            }
            
            // Check if the current user is the owner of the discussion
            var userId = _userManager.GetUserId(User);
            var existingDiscussion = await _context.Discussion.FindAsync(id);
            
            if (existingDiscussion == null || existingDiscussion.ApplicationUserId != userId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the existing discussion's properties
                    existingDiscussion.Title = discussion.Title;
                    existingDiscussion.Content = discussion.Content;

                    // Handle new image file if uploaded
                    if (discussion.ImageFile != null)
                    {
                        // Delete old image if it exists
                        if (!string.IsNullOrEmpty(existingDiscussion.ImageFilename))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", existingDiscussion.ImageFilename);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Save new image
                        string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(discussion.ImageFile.FileName);
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", newFileName);
                        
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await discussion.ImageFile.CopyToAsync(fileStream);
                        }

                        existingDiscussion.ImageFilename = newFileName;
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionExists(discussion.DiscussionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(discussion);
        }

        // GET: Discussions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .Include(d => d.ApplicationUser)
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
                
            if (discussion == null)
            {
                return NotFound();
            }
            
            // Check if the current user is the owner of the discussion
            var userId = _userManager.GetUserId(User);
            if (discussion.ApplicationUserId != userId)
            {
                return Forbid();
            }

            return View(discussion);
        }

        // POST: Discussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discussion = await _context.Discussion
                .Include(d => d.Comments)  // Include comments
                .FirstOrDefaultAsync(d => d.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }
            
            // Check if the current user is the owner of the discussion
            var userId = _userManager.GetUserId(User);
            if (discussion.ApplicationUserId != userId)
            {
                return Forbid();
            }

            // Delete the image file if it exists
            if (!string.IsNullOrEmpty(discussion.ImageFilename))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", discussion.ImageFilename);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            // Delete associated comments first
            if (discussion.Comments != null)
            {
                _context.Comment.RemoveRange(discussion.Comments);
            }
            
            // Then delete the discussion
            _context.Discussion.Remove(discussion);
            await _context.SaveChangesAsync();

            // If the request is AJAX, return a JSON result
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true });
            }

            // Otherwise, redirect to the index page
            return RedirectToAction(nameof(Index));
        }

        private bool DiscussionExists(int id)
        {
            return _context.Discussion.Any(e => e.DiscussionId == id);
        }
    }
}

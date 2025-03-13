using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MadeInCanadaForum.Data;
using MadeInCanadaForum.Models;

namespace MadeInCanadaForum.Controllers
{
    public class DiscussionsController : Controller
    {
        private readonly MadeInCanadaForumContext _context;

        public DiscussionsController(MadeInCanadaForumContext context)
        {
            _context = context;
        }

        // GET: Discussions
        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussion.ToListAsync();
            return View(discussions);
        }

        // GET: Discussions/Details/5
        public async Task<IActionResult> Details(int? id)
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


        public IActionResult Create()
        {
            return View();
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscussionId,Title,Content,Location,Camera,ImageFile,IsVisible,CreateDate")] MadeInCanadaForum.Models.Discussion discussion)
        {
  
            discussion.ImageFilename = Guid.NewGuid().ToString() + Path.GetExtension(discussion.ImageFile?.FileName);

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


            var discussion = await _context.Discussion.Include("Comments").FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }
            return View(discussion);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MadeInCanadaForum.Models.Discussion discussion)
        {
            if (id != discussion.DiscussionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discussion);
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
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // POST: Discussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discussion = await _context.Discussion.FindAsync(id);
            if (discussion != null)
            {
                _context.Discussion.Remove(discussion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscussionExists(int id)
        {
            return _context.Discussion.Any(e => e.DiscussionId == id);
        }
    }
}

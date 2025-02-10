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
    public class CommentsController : Controller
    {
        private readonly MadeInCanadaForumContext _context;

        public CommentsController(MadeInCanadaForumContext context)
        {
            _context = context;
        }

        // GET: Comments
        //public async Task<IActionResult> Index()
        //{
        //    var madeInCanadaForumContext = _context.Comment.Include(c => c.Discussion);
        //    return View(await madeInCanadaForumContext.ToListAsync());
        //}

        // GET: Comments/Details/5

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comment = await _context.Comment
        //        .Include(c => c.Discussion)
        //        .FirstOrDefaultAsync(m => m.CommentId == id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(comment);
        //}

        // GET: Comments/Create
        public IActionResult Create(int discussionId)
        {
            ViewBag.DiscussionId = discussionId; // Pass the discussion ID to the view
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content,DiscussionId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreateDate = DateTime.Now; // Set the creation date
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Discussions", new { id = comment.DiscussionId }); // Redirect to the discussion details page
            }
            ViewData["DiscussionId"] = comment.DiscussionId; // Pass the discussion ID back to the view
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["DiscussionId"] = comment.DiscussionId; // Pass the discussion ID to the view
            return View(comment);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,Content,DiscussionId")] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.CommentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Discussions", new { id = comment.DiscussionId }); // Redirect to the discussion details page
            }
            ViewData["DiscussionId"] = comment.DiscussionId; // Pass the discussion ID back to the view
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .Include(c => c.Discussion)
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            //delete the comment in database
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();


            //return View(comment);

            // re-direct to ../discussions/edit/{DiscussionId}
            return RedirectToAction("Edit", "Discussions", new { id = comment.DiscussionId });
        }





        // POST: Comments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var comment = await _context.Comment.FindAsync(id);
        //    if (comment != null)
        //    {
        //        _context.Comment.Remove(comment);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentId == id);
        }
    }
}

﻿using System;
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
    public class CommentsController : Controller
    {
        private readonly MadeInCanadaForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(MadeInCanadaForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Create(int discussionId)
        {
            ViewBag.DiscussionId = discussionId; 
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content,DiscussionId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreateDate = DateTime.Now;
                comment.ApplicationUserId = _userManager.GetUserId(User);
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Discussions", new { id = comment.DiscussionId }); 
            }
            ViewData["DiscussionId"] = comment.DiscussionId; 
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
            
            // Check if the current user is the owner of the comment
            var userId = _userManager.GetUserId(User);
            if (comment.ApplicationUserId != userId)
            {
                return Forbid();
            }
            
            ViewData["DiscussionId"] = comment.DiscussionId; 
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
            
            // Check if the current user is the owner of the comment
            var userId = _userManager.GetUserId(User);
            var existingComment = await _context.Comment.FindAsync(id);
            
            if (existingComment == null || existingComment.ApplicationUserId != userId)
            {
                return Forbid();
            }
            
            // Preserve the ApplicationUserId
            comment.ApplicationUserId = userId;
            comment.CreateDate = existingComment.CreateDate;

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
                return RedirectToAction("Details", "Discussions", new { id = comment.DiscussionId }); 
            }
            ViewData["DiscussionId"] = comment.DiscussionId; 
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
            
            // Check if the current user is the owner of the comment
            var userId = _userManager.GetUserId(User);
            if (comment.ApplicationUserId != userId)
            {
                return Forbid();
            }

            //delete the comment in database
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", "Discussions", new { id = comment.DiscussionId });
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentId == id);
        }
    }
}

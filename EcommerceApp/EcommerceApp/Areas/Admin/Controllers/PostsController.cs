﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Data;
using Entities;
using EcommerceApp.Utility;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceApp.Areas.Admin.Controllers;

[Area("Admin"), Authorize]
public class PostsController : Controller
{
    private readonly DatabaseContext _context;

    public PostsController(DatabaseContext context)
    {
        _context = context;
    }

    // GET: Admin/Posts
    public async Task<IActionResult> Index()
    {
        var databaseContext = _context.Posts.Include(p => p.Category);
        return View(await databaseContext.ToListAsync());
    }

    // GET: Admin/Posts/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Posts == null)
        {
            return NotFound();
        }

        var post = await _context.Posts
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    // GET: Admin/Posts/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Post post, IFormFile? Image)
    {
        if (ModelState.IsValid)
        {
            post.ImageUrl = FileHelper.FileLoader(Image);
            _context.Add(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
        return View(post);
    }

    // GET: Admin/Posts/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Posts == null)
        {
            return NotFound();
        }

        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Description", post.CategoryId);
        return View(post);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Post post, IFormFile Image, bool DelImg)
    {
        if (id != post.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                if (Image != null)
                {
                    post.ImageUrl = FileHelper.FileLoader(Image);
                }
                if (DelImg)
                {
                    post.ImageUrl = string.Empty;
                    FileHelper.FileTerminator(post.ImageUrl);
                }
                _context.Update(post);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(post.Id))
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
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Description", post.CategoryId);
        return View(post);
    }

    // GET: Admin/Posts/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Posts == null)
        {
            return NotFound();
        }

        var post = await _context.Posts
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    // POST: Admin/Posts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Posts == null)
        {
            return Problem("Entity set 'DatabaseContext.Posts'  is null.");
        }
        var post = await _context.Posts.FindAsync(id);
        if (post != null)
        {
            _context.Posts.Remove(post);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PostExists(int id)
    {
      return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}

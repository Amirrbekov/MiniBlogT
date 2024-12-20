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
public class CategoriesController : Controller
{
    private readonly DatabaseContext _context;

    public CategoriesController(DatabaseContext context)
    {
        _context = context;
    }

    // GET: Admin/Categories
    public async Task<IActionResult> Index()
    {
          return _context.Categories != null ? 
                      View(await _context.Categories.ToListAsync()) :
                      Problem("Entity set 'DatabaseContext.Categories'  is null.");
    }

    // GET: Admin/Categories/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Categories == null)
        {
            return NotFound();
        }

        var category = await _context.Categories
            .FirstOrDefaultAsync(m => m.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // GET: Admin/Categories/Create
    public IActionResult Create()
    {
        ViewData["ParentId"] = new SelectList(_context.Categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category, IFormFile? Image)
    {
        if (ModelState.IsValid)
        {
            category.ImageUrl = FileHelper.FileLoader(Image);
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    // GET: Admin/Categories/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Categories == null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        ViewData["ParentId"] = new SelectList(_context.Categories, "Id", "Name");
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Category category, IFormFile? Image, bool DelImg)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                if (Image != null)
                {
                    category.ImageUrl = FileHelper.FileLoader(Image);
                }
                if (DelImg)
                {
                    FileHelper.FileTerminator(category.ImageUrl);
                    category.ImageUrl = string.Empty;
                }
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
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
        return View(category);
    }

    // GET: Admin/Categories/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Categories == null)
        {
            return NotFound();
        }

        var category = await _context.Categories
            .FirstOrDefaultAsync(m => m.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Admin/Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Categories == null)
        {
            return Problem("Entity set 'DatabaseContext.Categories'  is null.");
        }
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists(int id)
    {
      return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}

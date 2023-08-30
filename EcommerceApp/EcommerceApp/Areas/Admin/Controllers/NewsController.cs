using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Data;
using Entities;
using EcommerceApp.Utility;

namespace EcommerceApp.Areas.Admin.Controllers;

[Area("Admin")]
public class NewsController : Controller
{
    private readonly DatabaseContext _context;

    public NewsController(DatabaseContext context)
    {
        _context = context;
    }

    // GET: Admin/News
    public async Task<IActionResult> Index()
    {
          return _context.News != null ? 
                      View(await _context.News.ToListAsync()) :
                      Problem("Entity set 'DatabaseContext.News'  is null.");
    }

    // GET: Admin/News/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.News == null)
        {
            return NotFound();
        }

        var news = await _context.News
            .FirstOrDefaultAsync(m => m.Id == id);
        if (news == null)
        {
            return NotFound();
        }

        return View(news);
    }

    // GET: Admin/News/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/News/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(News news, IFormFile? Image)
    {
        if (ModelState.IsValid)
        {
            news.ImageUrl = FileHelper.FileLoader(Image);
            _context.Add(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(news);
    }

    // GET: Admin/News/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.News == null)
        {
            return NotFound();
        }

        var news = await _context.News.FindAsync(id);
        if (news == null)
        {
            return NotFound();
        }
        return View(news);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, News news, IFormFile? Image, bool DelImg)
    {
        if (id != news.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                if (Image != null)
                {
                    news.ImageUrl = FileHelper.FileLoader(Image);
                }
                if (DelImg)
                {
                    news.ImageUrl = string.Empty;
                    FileHelper.FileTerminator(news.ImageUrl);
                }
                _context.Update(news);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsExists(news.Id))
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
        return View(news);
    }

    // GET: Admin/News/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.News == null)
        {
            return NotFound();
        }

        var news = await _context.News
            .FirstOrDefaultAsync(m => m.Id == id);
        if (news == null)
        {
            return NotFound();
        }

        return View(news);
    }

    // POST: Admin/News/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.News == null)
        {
            return Problem("Entity set 'DatabaseContext.News'  is null.");
        }
        var news = await _context.News.FindAsync(id);
        if (news != null)
        {
            _context.News.Remove(news);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool NewsExists(int id)
    {
      return (_context.News?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}

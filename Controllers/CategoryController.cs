using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;

namespace ExpenseTracker.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            return _context.Categories != null ?
                View(await _context.Categories.ToListAsync()) : Problem("Entity set 'ApplicationDbcontext.Categories' is null.");
        }

      

        // GET: Category/AddOrEdit
        public IActionResult AddOrEdit(int id=0)
        {
            if (id == 0)
            {
                return View(new CategoryClass());
            }
            return View(_context.Categories.Find(id));
        }

        // POST: Category/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryID,Title,Icon,Type")] CategoryClass categoryClass)
        {
            if (ModelState.IsValid)
            {
                if(categoryClass.CategoryID == 0)
                    _context.Add(categoryClass);
                else
                    _context.Update(categoryClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryClass);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
            }
            var categoryClass = await _context.Categories.FindAsync(id);
            if (categoryClass != null)
            {
                _context.Categories.Remove(categoryClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryClassExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }
    }
}

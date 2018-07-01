using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iSnippets.Models;

namespace iSnippets.Controllers
{
    public class codesController : Controller
    {
        private readonly iSnippetsContext _context;

        public codesController(iSnippetsContext context)
        {
            _context = context;
        }

        // GET: codes
        public async Task<IActionResult> Index(string search)
        {
            var query = _context.code.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                var searchLower = search.ToLower();
                query = query.Where(q =>
                                   (q.Language.ToLower().Contains(searchLower)
                                   || q.Description.ToLower().Contains(searchLower)));
            }
            return View(await query.ToListAsync());
        }

        // GET: codes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var code = await _context.code
                .SingleOrDefaultAsync(m => m.Id == id);
            if (code == null)
            {
                return NotFound();
            }

            return View(code);
        }

        // GET: codes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: codes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,codeSnippet,Language")] code code)
        {
            if (ModelState.IsValid)
            {
                code.Id = Guid.NewGuid();
                _context.Add(code);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(code);
        }

        // GET: codes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var code = await _context.code.SingleOrDefaultAsync(m => m.Id == id);
            if (code == null)
            {
                return NotFound();
            }
            return View(code);
        }

        // POST: codes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,codeSnippet,Language")] code code)
        {
            if (id != code.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(code);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!codeExists(code.Id))
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
            return View(code);
        }

        // GET: codes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var code = await _context.code
                .SingleOrDefaultAsync(m => m.Id == id);
            if (code == null)
            {
                return NotFound();
            }

            return View(code);
        }

        // POST: codes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var code = await _context.code.SingleOrDefaultAsync(m => m.Id == id);
            _context.code.Remove(code);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool codeExists(Guid id)
        {
            return _context.code.Any(e => e.Id == id);
        }
    }
}

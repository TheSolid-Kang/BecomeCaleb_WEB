using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.TwoMitesTbl;
using BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema;
using ASPMVC_Practice.Controllers;
using BecomeCaleb_WEB.Controllers.Caleb;

namespace BecomeCaleb_WEB.Controllers.TwoMites
{
    public class _TFBibleController : BaseController<_TFBibleController>
    {
        private readonly TwoMitesContext _context;

        public _TFBibleController(ILogger<_TFBibleController> logger) : base(logger)
        {
            _context = new TwoMitesContext();
        }



        // GET: _TFBible
        public async Task<IActionResult> Index()
        {
              return _context._TFBibles != null ? 
                          View(await _context._TFBibles.ToListAsync()) :
                          Problem("Entity set 'TwoMitesContext._TFBibles'  is null.");
        }

        // GET: _TFBible/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context._TFBibles == null)
            {
                return NotFound();
            }

            var _TFBible = await _context._TFBibles
                .FirstOrDefaultAsync(m => m.BibleSeq == id);
            if (_TFBible == null)
            {
                return NotFound();
            }

            return View(_TFBible);
        }

        // GET: _TFBible/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: _TFBible/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BibleSeq,Testament,Chapter,Verse,Descript")] _TFBible _TFBible)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_TFBible);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(_TFBible);
        }

        // GET: _TFBible/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context._TFBibles == null)
            {
                return NotFound();
            }

            var _TFBible = await _context._TFBibles.FindAsync(id);
            if (_TFBible == null)
            {
                return NotFound();
            }
            return View(_TFBible);
        }

        // POST: _TFBible/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BibleSeq,Testament,Chapter,Verse,Descript")] _TFBible _TFBible)
        {
            if (id != _TFBible.BibleSeq)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_TFBible);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_TFBibleExists(_TFBible.BibleSeq))
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
            return View(_TFBible);
        }

        // GET: _TFBible/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context._TFBibles == null)
            {
                return NotFound();
            }

            var _TFBible = await _context._TFBibles
                .FirstOrDefaultAsync(m => m.BibleSeq == id);
            if (_TFBible == null)
            {
                return NotFound();
            }

            return View(_TFBible);
        }

        // POST: _TFBible/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context._TFBibles == null)
            {
                return Problem("Entity set 'TwoMitesContext._TFBibles'  is null.");
            }
            var _TFBible = await _context._TFBibles.FindAsync(id);
            if (_TFBible != null)
            {
                _context._TFBibles.Remove(_TFBible);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool _TFBibleExists(int id)
        {
          return (_context._TFBibles?.Any(e => e.BibleSeq == id)).GetValueOrDefault();
        }
    }
}

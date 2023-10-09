using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Migration;
using BecomeCaleb_WEB.Models;

namespace BecomeCaleb_WEB.Controllers
{
    public class _TCDiaryController : Controller
    {
        private readonly CalebContext _context;

        public _TCDiaryController(CalebContext context)
        {
            _context = context;
        }

        // GET: _TCDiary
        public async Task<IActionResult> Index()
        {
              return _context._TCDiaries != null ? 
                          View(await _context._TCDiaries.ToListAsync()) :
                          Problem("Entity set 'CalebContext._TCDiaries'  is null.");
        }

        // GET: _TCDiary/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context._TCDiaries == null)
            {
                return NotFound();
            }

            var _TCDiary = await _context._TCDiaries
                .FirstOrDefaultAsync(m => m.DiarySeq == id);
            if (_TCDiary == null)
            {
                return NotFound();
            }

            return View(_TCDiary);
        }

        // GET: _TCDiary/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: _TCDiary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChurchSeq,DiarySeq,InDate,Title,Record,Remark,LastUserSeq,LastDateTime")] _TCDiary _TCDiary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_TCDiary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(_TCDiary);
        }

        // GET: _TCDiary/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context._TCDiaries == null)
            {
                return NotFound();
            }

            var _TCDiary = await _context._TCDiaries.FindAsync(id);
            if (_TCDiary == null)
            {
                return NotFound();
            }
            return View(_TCDiary);
        }

        // POST: _TCDiary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChurchSeq,DiarySeq,InDate,Title,Record,Remark,LastUserSeq,LastDateTime")] _TCDiary _TCDiary)
        {
            if (id != _TCDiary.DiarySeq)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_TCDiary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_TCDiaryExists(_TCDiary.DiarySeq))
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
            return View(_TCDiary);
        }

        // GET: _TCDiary/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context._TCDiaries == null)
            {
                return NotFound();
            }

            var _TCDiary = await _context._TCDiaries
                .FirstOrDefaultAsync(m => m.DiarySeq == id);
            if (_TCDiary == null)
            {
                return NotFound();
            }

            return View(_TCDiary);
        }

        // POST: _TCDiary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context._TCDiaries == null)
            {
                return Problem("Entity set 'CalebContext._TCDiaries'  is null.");
            }
            var _TCDiary = await _context._TCDiaries.FindAsync(id);
            if (_TCDiary != null)
            {
                _context._TCDiaries.Remove(_TCDiary);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool _TCDiaryExists(int id)
        {
          return (_context._TCDiaries?.Any(e => e.DiarySeq == id)).GetValueOrDefault();
        }
    }
}

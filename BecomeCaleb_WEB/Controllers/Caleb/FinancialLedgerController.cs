using ASPMVC_Practice.Controllers;
using BecomeCaleb_WEB.Models.CalebTbl;
using Engine._01.DBMgr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BecomeCaleb_WEB.Controllers.Caleb
{
    public class FinancialLedgerController : BaseController<FinancialLedgerController>
    {
        public FinancialLedgerController(ILogger<FinancialLedgerController> logger) : base(logger)
        {
            _context = new CalebContext();
        }

        private readonly CalebContext _context;

        // GET: FinancialLedgerController
        public async Task<IActionResult> Index()
        {
            return _context._TCFinancialLedgers != null ?
                        View(await _context._TCFinancialLedgers.ToListAsync()) :
                        Problem("Entity set 'CalebContext._TCFinancialLedgers'  is null.");
        }
        public async Task<IActionResult> MonthlyUsageAmount()
        {
            using (var dbMgr = new MSSQL_Mgr())
            {
                var list = dbMgr.SelectList<_VCategoryUsePrice>(DbMgr.DB_CONNECTION.CALEB, "EXEC SP_GetMonthlyUsageAmount");
                list.ForEach(e => Console.WriteLine(e.YYYY));
                return View(list);
            }

        }


        // GET: FinancialLedgerController/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null || _context._TCFinancialLedgers == null)
            {
                return NotFound();
            }

            var _TCDiary = await _context._TCFinancialLedgers.FirstOrDefaultAsync(m => m.FinancialSeq == id);
            if (_TCDiary == null)
            {
                return NotFound();
            }

            return View(_TCDiary);
        }

        // GET: FinancialLedgerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinancialLedgerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FinancialLedgerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FinancialLedgerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FinancialLedgerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FinancialLedgerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using ASPMVC_Practice.Controllers;
using BecomeCaleb_WEB.Models.CalebTbl;
using Engine._01.DBMgr;
using Engine._02.KMP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BecomeCaleb_WEB.Controllers.Caleb
{
    public class FinancialLedgerController : BaseController<FinancialLedgerController>
    {
        public FinancialLedgerController(ILogger<FinancialLedgerController> logger) : base(logger)
        {
            _context = new CalebContext();
            _TCMinors = _context._TCMinors.ToList();
            _TCMinors = _TCMinors.FindAll(_e => _e.MajorSeq == 4);// 4 == 가계부유형
        }

        private readonly CalebContext _context;
        private readonly List<_TCMinor> _TCMinors;

        private List<string> SplitSearchKeywords(string _str, string _delimiter)
        {
            List<string> _TCDiaries = new();
            if (_str.Contains(","))
            {
                foreach (var _keyword in _str.Split(","))
                {
                    if (_keyword.Equals(""))
                        continue;
                    var keyword = _keyword.Trim();
                    _TCDiaries.Add(keyword);
                }
            }
            else
            {
                var keyword = _str.Trim();
                _TCDiaries.Add(keyword);
            }
            return _TCDiaries;
        }


        // GET: FinancialLedgerController
        public async Task<IActionResult> Index()
        {
            return _context._TCFinancialLedgers != null ?
                        View(await _context._TCFinancialLedgers.ToListAsync()) :
                        Problem("Entity set 'CalebContext._TCFinancialLedgers'  is null.");
        }
        public async Task<IActionResult> MonthlyUsageAmount(List<string>? CategoryMirs)
        {
            TempData["ChartDatas"] = "";
            using (var dbMgr = new MSSQL_Mgr())
            {
                var list = dbMgr.SelectList<_VCategoryUsePrice>(DbMgr.DB_CONNECTION.CALEB, "EXEC SP_GetMonthlyUsageAmount");
                list = GetMonthlyUsage(list);
                //list.ForEach(e => Console.WriteLine(e.YYYY));
                if (0 != CategoryMirs.Count)
                {
                    var iterable = from _item in CategoryMirs select _item;
                    string data = iterable.Aggregate((item1, item2) => item1 += $", {item2}");
                    var searchKeywords = SplitSearchKeywords(data, ",");
                    CreateChartData(list, searchKeywords);
                }

                return View(list);
            }
        }
        private List<_VCategoryUsePrice> GetMonthlyUsage(List<_VCategoryUsePrice> _list)
        {
            Dictionary<int, HashSet<int>> map = new Dictionary<int, HashSet<int>>();
            foreach (_VCategoryUsePrice _item in _list)
            {
                if (false == map.ContainsKey((int)_item.YYYY))
                    map.Add((int)_item.YYYY, new HashSet<int>());

                map[(int)_item.YYYY].Add((int)_item.MM);
            }

            List<_VCategoryUsePrice> list = new List<_VCategoryUsePrice>();
            foreach(var _pair in map)
            {
                int yyyy = _pair.Key;
                HashSet<int> set = _pair.Value;
                foreach(int _mm in set)
                {
                    foreach(var _mir in _TCMinors)
                    {
                        list.Add(new _VCategoryUsePrice() { YYYY = yyyy, MM = _mm, CategoryMir = _mir.MinorSeq, Category = _mir.MinorName, TotPrice = 0});
                    }
                }
            }
            
            foreach(var _item in list)
            {
                var item = _list.Find(_e => _e.YYYY == _item.YYYY 
                && _e.MM == _item.MM 
                && _e.CategoryMir == _item.CategoryMir);
                if(null != item)
                {
                    _item.TotPrice = item.TotPrice;
                }
            }

            return list;

        }
        private void CreateChartData(List<_VCategoryUsePrice> _list, List<string> _searchKeywords)
        {
            string[] arrRainbowColors = { "#ff0000", "#ff8c00", "#ffff00", "#008000", "#0000ff", "#4b0082", "#800080" };
            var iter = arrRainbowColors.GetEnumerator();

            var labels = GetChartLabels(_list);
            StringBuilder strBuil = new StringBuilder(2048);
            strBuil.AppendLine($"data: {{");
            strBuil.AppendLine($"   labels: [{labels}],");
            strBuil.AppendLine($"   datasets:");
            strBuil.Append($"   [");

            foreach (var _keyword in _searchKeywords)
            {
                if (false == iter.MoveNext())
                    break;

                var iterable = from _item in _list where _item.CategoryMir.ToString().Equals(_keyword) select _item.TotPrice.ToString() ;
                string data = iterable.Aggregate((item1, item2) => item1 += $", {item2}");
                //string data = GetSearchKeywordCount(_TCDiaries, labels, _keyword);
                strBuil.AppendLine($"{{");

                strBuil.AppendLine($"   label: '{_TCMinors.Find(_e => _e.MinorSeq.ToString() == _keyword).MinorName}',");
                strBuil.AppendLine($"   data: [{data}],");
                strBuil.AppendLine($"   type: 'bar', // 'bar' type, 전체 타입과 같다면 생략가능    ");
                strBuil.AppendLine($"   backgroundColor: ['{iter.Current.ToString()}'],");
                strBuil.AppendLine($"   borderColor: ['{iter.Current.ToString()}']");
                strBuil.Append($"}},");
            }
            string chartDatas = strBuil.ToString();
            chartDatas = chartDatas.Substring(0, chartDatas.LastIndexOf(","));
            chartDatas += "]}";
            TempData["ChartDatas"] = chartDatas;
        }
        private string GetChartLabels(List<_VCategoryUsePrice> _list)
        {
            SortedSet<string> setChartX = new SortedSet<string>();
            foreach (var _diary in _list)
            {
                string year = _diary.YYYY.ToString();
                string month = _diary.MM.ToString();
                if (month?.Length < 2)
                    month = "0" + month;

                string dateYearMonth = $"{year}.{month}";
                setChartX.Add(dateYearMonth);
            }

            string labels = "";
            foreach (var charX in setChartX)
            {
                labels += $"'{charX}',";
            }
            labels = labels.Substring(0, labels.LastIndexOf(","));
            return labels;
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

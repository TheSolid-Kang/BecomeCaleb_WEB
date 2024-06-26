﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.CalebTbl;
using ASPMVC_Practice.Controllers;
using Engine._02.KMP;
using System.Text;
using Engine._01.DBMgr;

namespace BecomeCaleb_WEB.Controllers.Caleb
{
    public class _TCDiaryController : BaseController<_TCDiaryController>
    {
        private readonly CalebContext _context;

        public _TCDiaryController(ILogger<_TCDiaryController> logger) : base(logger)
        {
            _context = new CalebContext();
        }

        /*231009: context를 받아온는 코드가 없으므로 삭제해야함.
public _TCDiaryController(CalebContext context)
{
   _context = context;
}
*/
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
        public async Task<IActionResult> SearchKeywords()
        {
            TempData["SearchKeyword"] = "";
            TempData["ChartDatas"] = "";
            var _TCDiaries = await _context._TCDiaries.ToListAsync();
            _TCDiaries = _TCDiaries?.OrderBy(_e => _e.InDate).ToList();
            return _context._TCDiaries != null ?
            View(_TCDiaries) :
            Problem("Entity set 'CalebContext._TCDiaries'  is null.");
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchKeywords(string _searchKeyword)
        {
            TempData["SearchKeyword"] = "";

            if (string.IsNullOrEmpty(_searchKeyword))
                return RedirectToAction(nameof(SearchKeywords));//검색어 없이 검색한 경우

            string[] arrColors = { "#ff0000", "#ff8c00", "#ffff00", "#008000", "#0000ff", "#4b0082", "#800080" };
            var _TCDiaries = await _context._TCDiaries.ToListAsync();
            var searchKeywords = SplitSearchKeywords(_searchKeyword, ",");
            searchKeywords.Remove("");

            InsertSearchRecord(_searchKeyword);

            CreateChartData(_TCDiaries, searchKeywords);

            foreach (var keyword in searchKeywords)
            {
                int colorIdx = searchKeywords.IndexOf(keyword);
                var convertedKeyword = GetConvertedText(keyword, arrColors[colorIdx]); //색상 바꾸기
                _TCDiaries.ForEach(_e => _e.Record = _e.Record?.Replace(keyword, convertedKeyword));
            }

            _TCDiaries.RemoveAll(_e => false == _e.Record?.Contains("<span"));
            //_TCDiaries = _TCDiaries.OrderByDescending(_e => _e.InDate).ToList();
            //_TCDiaries = _TCDiaries.OrderBy(_e => _e.InDate).ToList();
            _TCDiaries = (from _e in _TCDiaries
                         orderby _e.InDate ascending
                         select _e).ToList();



            return _context._TCDiaries != null ? View(_TCDiaries) : Problem("Entity set 'CalebContext._TCDiaries'  is null.");
        }

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

        private string GetConvertedText(string _text, string _color)
        {
            StringBuilder strBuil = new StringBuilder();
            strBuil.Append($"<span style=\"color:{_color}; font-weight:ariel; font-size : large;\">");
            strBuil.Append($"[{_text}]");
            strBuil.Append("</span>");
            return strBuil.ToString();
        }
        private void InsertSearchRecord(string _searchKeyword)
        {

            using (var dbMgr = new MSSQL_Mgr())
            {
                string query = $"INSERT INTO _TCSearchRecord(ChurchSeq, SearchKeyword, LastUserSeq, LastDateTime ) VALUES(1,'{_searchKeyword}', 2, GETDATE());";

                dbMgr.GetDataSet(DbMgr.DB_CONNECTION.CALEB, query);
            }
        }
        private void CreateChartData(List<_TCDiary> _list, List<string> _searchKeywords)
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

                string data = GetSearchKeywordCount(_list, labels, _keyword);
                strBuil.AppendLine($"{{");

                strBuil.AppendLine($"   label: '{_keyword}',");
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
        private string GetChartLabels(List<_TCDiary> _list)
        {
            SortedSet<string> setChartX = new SortedSet<string>();
            foreach (var _diary in _list)
            {
                string year = _diary.InDate?.Year.ToString();
                string month = _diary.InDate?.Month.ToString();
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
        private string GetSearchKeywordCount(List<_TCDiary> _list, string _labels, string _searchKeyword)
        {
            var listLabels = new List<string>();
            if (_labels.Contains(","))
                listLabels = _labels.Split(",").ToList();
            else
                listLabels.Add(_labels);

            _KMP kmp = new _KMP();
            Dictionary<string, List<_TCDiary>> _mapTCDiaries = new();
            foreach (var _chartX in listLabels)
            {
                _mapTCDiaries.Add(_chartX, new List<_TCDiary>());
            }

            _list.ForEach(e =>
            {
                string year = e.InDate?.Year.ToString();
                string month = e.InDate?.Month.ToString();
                if (month.Length < 2) { month = "0" + month; }
                string dateYearMonth = $"'{year}.{month}'";

                _mapTCDiaries[dateYearMonth].Add(e);
            });

            string strKeywordCount = "";
            foreach (var _listTCDiary in _mapTCDiaries)
            {
                var iterable = from element in _listTCDiary.Value select element;
                int monthCnt = 0;
                foreach (var item in iterable)
                {
                    monthCnt += kmp.get_searched_address(item.Record, _searchKeyword).Count;
                }
                strKeywordCount += monthCnt.ToString() + ",";

            }
            strKeywordCount = strKeywordCount.Substring(0, strKeywordCount.Length - 1);

            System.Diagnostics.Debug.WriteLine("  ");
            return strKeywordCount;
        }
    }
}

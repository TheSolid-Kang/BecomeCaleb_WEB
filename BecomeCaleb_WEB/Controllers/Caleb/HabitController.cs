using ASPMVC_Practice.Controllers;
using BecomeCaleb_WEB.Models.CalebHabitTbl;
using BecomeCaleb_WEB.Models.ViewModel.Caleb.Habit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BecomeCaleb_WEB.Controllers.Caleb
{
    public class HabitController : BaseController<HabitController>
    {
        private readonly CalebHabitContext _context;

        public HabitController(ILogger<HabitController> logger) : base(logger)
        {
            _context = new CalebHabitContext();
        }

        /// <summary>
        /// WHAT: ViewBag에 필요한 SelectList 아이템들을 준비합니다.
        /// WHY: View에서 드롭다운 목록에 사용할 데이터를 제공하기 위해서입니다.
        /// HOW: 각 마스터 테이블에서 활성화된 아이템들을 조회하여 SelectList로 변환합니다.
        /// </summary>
        private void PrepareViewBagItems()
        {
            ViewBag._TCHHabits = new SelectList(
                _context._TCHHabits
                    .Where(h => h.IsDeleted == false || h.IsDeleted == null)
                    .OrderBy(h => h.HabitName),
                "HabitSeq",
                "HabitName"
            );

            ViewBag._TCHLocations = new SelectList(
                _context._TCHLocations
                    .Where(l => l.IsDeleted == false || l.IsDeleted == null)
                    .OrderBy(l => l.LocationName),
                "LocationSeq",
                "LocationName"
            );

            ViewBag._TCHStates = new SelectList(
                _context._TCHStates
                    .Where(s => s.IsDeleted == false || s.IsDeleted == null)
                    .OrderBy(s => s.StateName),
                "StateSeq",
                "StateName"
            );

            ViewBag._TCHActions = new SelectList(
                _context._TCHActions
                    .Where(a => a.IsDeleted == false || a.IsDeleted == null)
                    .OrderBy(a => a.ActionName),
                "ActionSeq",
                "ActionName"
            );

            ViewBag._TCHCountermeasures = new SelectList(
                _context._TCHCountermeasures
                    .Where(c => c.IsDeleted == false || c.IsDeleted == null)
                    .OrderBy(c => c.CountermeasureName),
                "CountermeasureSeq",
                "CountermeasureName"
            );
        }

        /// <summary>
        /// WHAT: 새로운 습관 발생 기록 생성 페이지를 표시합니다.
        /// WHY: 사용자가 새로운 습관 발생을 기록할 수 있도록 합니다.
        /// HOW: 빈 ViewModel을 생성하고 필요한 드롭다운 데이터를 준비합니다.
        /// </summary>
        public IActionResult Create()
        {
            PrepareViewBagItems();
            return View(new HabitOccurrenceViewModel());
        }

        /// <summary>
        /// WHAT: 새로운 습관 발생 기록을 저장합니다.
        /// WHY: 사용자가 입력한 습관 발생 데이터를 데이터베이스에 저장합니다.
        /// HOW: 트랜잭션을 사용하여 모든 관련 데이터를 안전하게 저장합니다.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HabitOccurrenceViewModel viewModel)
        {
            // Navigation Property 관련 ModelState 제거
            foreach (var key in ModelState.Keys.ToList())
            {
                if (key.Contains("Navigation"))
                {
                    ModelState.Remove(key);
                }
            }

            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // 1. 기본 레코드 저장
                    viewModel.OccurrenceRecord.LastDateTime = DateTime.Now;
                    _context._TCHOccurrenceRecords.Add(viewModel.OccurrenceRecord);
                    await _context.SaveChangesAsync();

                    var occurrenceSeq = viewModel.OccurrenceRecord.OccurrenceSeq;

                    // 2. 상태 데이터 저장
                    if (viewModel.OccurrenceStates != null && viewModel.OccurrenceStates.Any())
                    {
                        foreach (var state in viewModel.OccurrenceStates)
                        {
                            state.OccurrenceSeq = occurrenceSeq;
                            state.LastDateTime = DateTime.Now;
                            _context._TCHOccurrenceStates.Add(state);
                        }
                    }

                    // 3. 행동 데이터 저장
                    if (viewModel.OccurrenceActions != null && viewModel.OccurrenceActions.Any())
                    {
                        foreach (var action in viewModel.OccurrenceActions)
                        {
                            action.OccurrenceSeq = occurrenceSeq;
                            action.LastDateTime = DateTime.Now;
                            _context._TCHOccurrenceActions.Add(action);
                        }
                    }

                    // 4. 대처방법 데이터 저장
                    if (viewModel.OccurrenceCountermeasures != null && viewModel.OccurrenceCountermeasures.Any())
                    {
                        foreach (var countermeasure in viewModel.OccurrenceCountermeasures)
                        {
                            countermeasure.OccurrenceSeq = occurrenceSeq;
                            countermeasure.LastDateTime = DateTime.Now;
                            _context._TCHOccurrenceCountermeasures.Add(countermeasure);
                        }
                    }

                    // 5. 상관관계 요인 저장
                    if (viewModel.CorrelationFactor != null)
                    {
                        viewModel.CorrelationFactor.OccurrenceSeq = occurrenceSeq;
                        viewModel.CorrelationFactor.LastDateTime = DateTime.Now;
                        _context._TCHCorrelationFactors.Add(viewModel.CorrelationFactor);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "습관 기록 저장 중 오류 발생");
                    ModelState.AddModelError("", "저장 중 오류가 발생했습니다: " + ex.Message);

                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var error in errors)
                    {
                        _logger.LogError(error.ErrorMessage);
                    }
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    _logger.LogError(error.ErrorMessage);
                }
            }

            PrepareViewBagItems();
            return View(viewModel);
        }
        /// <summary>
        /// WHAT: 습관 발생 기록 목록을 표시합니다.
        /// WHY: 저장된 모든 습관 발생 기록을 사용자가 확인할 수 있도록 합니다.
        /// HOW: 모든 관련 데이터를 포함하여 조회합니다.
        /// </summary>
        public async Task<IActionResult> Index(int? habitSeq = null)
        {
            // 기본 데이터 조회 - 선택된 습관의 데이터만 필터링
            var records = await _context._TCHOccurrenceRecords
                .Include(r => r.HabitSeqNavigation)
                .Include(r => r.LocationSeqNavigation)
                .Include(r => r._TCHOccurrenceStates)
                .Include(r => r._TCHOccurrenceCountermeasures)
                .Where(r => !habitSeq.HasValue || r.HabitSeq == habitSeq)
                .OrderByDescending(r => r.OccurrenceDateTime)
                .ToListAsync();
            // 습관 목록을 준비
            var habits = await _context._TCHHabits
                .Where(h => h.IsDeleted == false || h.IsDeleted == null)
                .OrderBy(h => h.HabitName)
                .ToListAsync();

            // SelectList 생성 - 전체 보기 옵션도 포함
            var habitSelectList = new List<SelectListItem>
    {
        new SelectListItem { Text = "전체 보기", Value = "" }
    };
            habitSelectList.AddRange(habits.Select(h =>
                new SelectListItem
                {
                    Text = h.HabitName,
                    Value = h.HabitSeq.ToString(),
                    Selected = habitSeq.HasValue && h.HabitSeq == habitSeq
                }));

            ViewBag.HabitList = habitSelectList;
            // 현재 청정 기간 계산
            var lastRecord = records.FirstOrDefault();
            ViewBag.CurrentCleanPeriod = lastRecord?.CleanPeriodDays ?? 0;

            // 최장 청정 기간 계산
            ViewBag.MaxCleanPeriod = records.Max(r => r.CleanPeriodDays) ?? 0;

            // 대처 성공률 계산
            var countermeasures = records.SelectMany(r => r._TCHOccurrenceCountermeasures).ToList();
            ViewBag.SuccessRate = countermeasures.Any()
                ? Math.Round((double)countermeasures.Count(c => c.IsSuccessful) / countermeasures.Count * 100)
                : 0;

            // 월별 데이터 준비
            var monthlyData = records
                .GroupBy(r => r.OccurrenceDateTime.ToString("yyyy-MM"))
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Month = g.Key,
                    AvgCleanPeriod = g.Average(r => r.CleanPeriodDays ?? 0)
                })
                .ToList();

            ViewBag.MonthLabels = monthlyData.Select(d => d.Month);
            ViewBag.MonthlyData = monthlyData.Select(d => d.AvgCleanPeriod);

            // 대처방법 효과성 데이터 준비
            var countermeasureEffectiveness = await _context._TCHOccurrenceCountermeasures
                .Include(c => c.CountermeasureSeqNavigation)
                .GroupBy(c => c.CountermeasureSeqNavigation.CountermeasureName)
                .Select(g => new
                {
                    Name = g.Key,
                    SuccessRate = g.Count(c => c.IsSuccessful) * 100.0 / g.Count()
                })
                .OrderByDescending(x => x.SuccessRate)
                .Take(5)
                .ToListAsync();

            ViewBag.CountermeasureLabels = countermeasureEffectiveness.Select(c => c.Name);
            ViewBag.CountermeasureData = countermeasureEffectiveness.Select(c => c.SuccessRate);

            return View(records);
        }
        [HttpPost]
        public async Task<IActionResult> SaveChanges([FromBody] HabitOccurrenceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 메인 레코드 업데이트
                _context._TCHOccurrenceRecords.Update(viewModel.OccurrenceRecord);

                // 상태 데이터 업데이트
                _context._TCHOccurrenceStates.RemoveRange(
                    _context._TCHOccurrenceStates.Where(s =>
                        s.OccurrenceSeq == viewModel.OccurrenceRecord.OccurrenceSeq));
                _context._TCHOccurrenceStates.AddRange(viewModel.OccurrenceStates);

                // 행동 데이터 업데이트
                _context._TCHOccurrenceActions.RemoveRange(
                    _context._TCHOccurrenceActions.Where(a =>
                        a.OccurrenceSeq == viewModel.OccurrenceRecord.OccurrenceSeq));
                _context._TCHOccurrenceActions.AddRange(viewModel.OccurrenceActions);

                // 대처방법 데이터 업데이트
                _context._TCHOccurrenceCountermeasures.RemoveRange(
                    _context._TCHOccurrenceCountermeasures.Where(c =>
                        c.OccurrenceSeq == viewModel.OccurrenceRecord.OccurrenceSeq));
                _context._TCHOccurrenceCountermeasures.AddRange(viewModel.OccurrenceCountermeasures);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ex.Message);
            }
        }
    }
}
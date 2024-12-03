using BecomeCaleb_WEB.Models.CalebHabitTbl;

namespace BecomeCaleb_WEB.Models.ViewModel.Caleb.Habit
{

    /// <summary>
    /// 습관 발생 기록을 위한 통합 ViewModel
    /// </summary>
    public class HabitOccurrenceViewModel
    {
        // 메인 레코드
        public _TCHOccurrenceRecord OccurrenceRecord { get; set; }

        // 상태 데이터
        public List<_TCHOccurrenceState> OccurrenceStates { get; set; }

        // 행동 데이터
        public List<_TCHOccurrenceAction> OccurrenceActions { get; set; }

        // 대처방법 데이터
        public List<_TCHOccurrenceCountermeasure> OccurrenceCountermeasures { get; set; }

        // 상관관계 요인
        public _TCHCorrelationFactor CorrelationFactor { get; set; }

        public HabitOccurrenceViewModel()
        {
            OccurrenceRecord = new _TCHOccurrenceRecord();
            OccurrenceStates = new List<_TCHOccurrenceState>();
            OccurrenceActions = new List<_TCHOccurrenceAction>();
            OccurrenceCountermeasures = new List<_TCHOccurrenceCountermeasure>();
            CorrelationFactor = new _TCHCorrelationFactor();
        }
    }

}

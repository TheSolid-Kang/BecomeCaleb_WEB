class HabitFormManager {
    constructor() {
        // 초기화 시 현재 시간 설정
        this.initDateTime();
        this.setupEventListeners();
    }

    // 현재 시간 초기화
    initDateTime() {
        const now = new Date();
        now.setMinutes(now.getMinutes() - now.getTimezoneOffset());
        document.getElementById('CurDateTime').value = now.toISOString().slice(0, 16);
    }

    // 이벤트 리스너 설정
    setupEventListeners() {
        // 각 추가 버튼에 이벤트 연결
        document.querySelector('.add-state-btn').addEventListener('click', () => this.addState());
        document.querySelector('.add-action-btn').addEventListener('click', () => this.addAction());
        document.querySelector('.add-countermeasure-btn').addEventListener('click', () => this.addCountermeasure());
    }

    // 상태 추가
    addState() {
        const container = document.getElementById('statesContainer');
        const index = container.children.length;
        const template = this.createStateTemplate(index);
        this.appendElement(container, template);
    }

    // 행동 추가
    addAction() {
        const container = document.getElementById('actionsContainer');
        const index = container.children.length;
        const template = this.createActionTemplate(index);
        this.appendElement(container, template);
    }

    // 대처방법 추가
    addCountermeasure() {
        const container = document.getElementById('countermeasuresContainer');
        const index = container.children.length;
        const template = this.createCountermeasureTemplate(index);
        this.appendElement(container, template);
    }

    // 항목 제거
    removeItem(button, className) {
        button.closest('.' + className).remove();
    }

    // 상태 템플릿 생성
    createStateTemplate(index) {
        return `
            <div class="state-group dynamic-item">
                <div class="row g-2">
                    <!-- 상태 선택 필드 -->
                    <div class="col-8">
                        <select name="OccurrenceStates[${index}].StateSeq" class="form-select" required>
                            <option value="">-- 감정/상태 선택 --</option>
                            ${this.getStateOptions()}
                        </select>
                    </div>
                    <!-- 강도 선택 필드 -->
                    <div class="col-3">
                        <select name="OccurrenceStates[${index}].Intensity" class="form-select" required>
                            <option value="">강도</option>
                            <option value="1">매우 약함</option>
                            <option value="2">약함</option>
                            <option value="3">보통</option>
                            <option value="4">강함</option>
                            <option value="5">매우 강함</option>
                        </select>
                    </div>
                    <!-- 삭제 버튼 -->
                    <div class="col-1">
                        <button type="button" class="btn btn-outline-danger btn-sm" 
                                onclick="habitForm.removeItem(this, 'state-group')">
                            <i class="fas fa-times"></i>
                        </button>
                    </div>
                </div>
            </div>
        `;
    }

    // [나머지 템플릿 생성 메서드들은 비슷한 패턴으로 구현]
}

// 페이지 로드 시 초기화
document.addEventListener('DOMContentLoaded', () => {
    window.habitForm = new HabitFormManager();
});
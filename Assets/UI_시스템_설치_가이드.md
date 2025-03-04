# Unity UI 시스템 설치 가이드

## 모든 씬에서 사용 가능한 UI 캔버스 설정 방법

### 1. 기본 설정

1. 새 빈 오브젝트를 생성하고 이름을 "UIManager"로 지정합니다.
2. "UIManager" 오브젝트에 `UIManager.cs` 스크립트를 추가합니다.
3. "UIManager" 오브젝트 아래에 캔버스 오브젝트를 생성합니다:
   - 메뉴: GameObject > UI > Canvas
4. 캔버스 설정:
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1920 x 1080 (또는 원하는 해상도)
   - Match: 0.5 (권장)

### 2. UI 요소 구성

1. 캔버스 아래에 필요한 모든 UI 패널을 생성합니다:
   - MainMenu (시작 메뉴 UI)
   - InGame (게임 중 UI)
   - GameOver (게임 오버 UI)
   - 기타 필요한 UI 패널들...
2. 각 패널은 빈 게임 오브젝트로 생성하고, 그 안에 필요한 UI 요소들을 배치합니다.
3. 필요하지 않은 패널은 초기에 비활성화해 둡니다.

### 3. 프리팹으로 변환

1. 완성된 "UIManager" 오브젝트를 프로젝트 뷰로 드래그하여 프리팹으로 저장합니다.
2. 모든 씬에서 이 프리팹을 사용하거나, 시작 씬에만 배치합니다 (싱글톤으로 유지됨).

### 4. 씬 관리

다음 방법 중 하나를 선택하여 UI 시스템을 초기화합니다:

#### 방법 1: 시작 씬 사용
1. 게임의 첫 씬에 UIManager 프리팹을 배치합니다.
2. 이후 모든 씬 전환에서 UI는 유지됩니다.

#### 방법 2: 프로그래밍 방식으로 초기화
```csharp
// 게임 시작 시 UIManager 초기화
if (UIManager.Instance == null)
{
    // 프리팹을 로드하여 인스턴스화
    GameObject uiManagerPrefab = Resources.Load<GameObject>("UIManager");
    Instantiate(uiManagerPrefab);
}
```

### 5. 사용 방법

UIManager에 접근하여 UI를 제어합니다:
```csharp
// UI 표시
UIManager.Instance.ShowUI("MainMenu");

// UI 숨기기
UIManager.Instance.HideUI("InGame");

// 게임 상태에 따라 UI 업데이트
UIManager.Instance.UpdateUI(GameState.Playing);
```

### 주의사항

1. Canvas Scaler 설정을 화면 해상도에 맞게 조정하세요.
2. UI 요소의 이름은 코드에서 참조하는 이름과 정확히 일치해야 합니다.
3. 새 씬을 빌드할 때 반드시 빌드 설정에 포함하세요. 
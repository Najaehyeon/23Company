using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static UIManager Instance { get; private set; }
    
    // 캔버스 오브젝트 참조
    [SerializeField] private Canvas mainCanvas;
    
    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환시에도 파괴되지 않음
            
            // 캔버스가 지정되지 않았다면 자식에서 찾음
            if (mainCanvas == null)
                mainCanvas = GetComponentInChildren<Canvas>();
                
            // 씬 로드 이벤트 등록
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            // 중복 인스턴스 제거
            Destroy(gameObject);
        }
    }
    
    private void OnDestroy()
    {
        // 이벤트 구독 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    // 씬 로드 이벤트 핸들러
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 변경될 때마다 필요한 처리
        AdjustCanvasSettings();
    }
    
    // 캔버스 설정 조정
    private void AdjustCanvasSettings()
    {
        if (mainCanvas != null)
        {
            // 캔버스 설정 업데이트
            // 예: 해상도에 따른 스케일 조정 등
        }
    }
    
    // UI 활성화/비활성화 메서드 예시
    public void ShowUI(string uiName)
    {
        Transform uiElement = mainCanvas.transform.Find(uiName);
        if (uiElement != null)
            uiElement.gameObject.SetActive(true);
    }
    
    public void HideUI(string uiName)
    {
        Transform uiElement = mainCanvas.transform.Find(uiName);
        if (uiElement != null)
            uiElement.gameObject.SetActive(false);
    }
    
    // 게임 진행 상태에 따라 UI 업데이트
    public void UpdateUI(GameState state)
    {
        // 게임 상태에 따라 UI 업데이트
        switch (state)
        {
            case GameState.MainMenu:
                ShowUI("MainMenu");
                HideUI("InGame");
                HideUI("GameOver");
                break;
            case GameState.Playing:
                HideUI("MainMenu");
                ShowUI("InGame");
                HideUI("GameOver");
                break;
            case GameState.GameOver:
                HideUI("MainMenu");
                HideUI("InGame");
                ShowUI("GameOver");
                break;
        }
    }
}

// 게임 상태 열거형
public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
} 
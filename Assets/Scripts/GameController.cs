using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // 현재 게임 상태
    private GameState currentState = GameState.MainMenu;
    
    private void Start()
    {
        // 시작 시 UI 상태 설정
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateUI(currentState);
        }
        else
        {
            Debug.LogWarning("UIManager가 씬에 없습니다. UI가 제대로 표시되지 않을 수 있습니다.");
        }
    }
    
    // 게임 시작 메서드
    public void StartGame()
    {
        currentState = GameState.Playing;
        
        // UI 업데이트
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateUI(currentState);
        }
        
        // 게임 로직 시작...
    }
    
    // 게임 오버 메서드
    public void GameOver()
    {
        currentState = GameState.GameOver;
        
        // UI 업데이트
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateUI(currentState);
        }
    }
    
    // 다른 씬으로 이동하는 메서드
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
} 
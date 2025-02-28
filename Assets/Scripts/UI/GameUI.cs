using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // 체력 슬라이드 구현, 점수 업데이트
    Slider hpSlider ; //0~100
    Text scoreText;

    public GameObject pausePanel;
    public GameObject gameOverPanel;
    Player player;

    private void Awake()
    {
        scoreText = GetComponentInChildren<Text>();
        hpSlider = GetComponentInChildren<Slider>();
        player =FindObjectOfType<Player>();
    }

    private void Update()
    {
        scoreText.text = ItemManager.Instance.totalScore.ToString(); // 점수 가져와서 텍스트로 보여주기
        hpSlider.value = player.currentHealth;
    }

    public void ActivePausePanel() // Pause할 때 PausePanel 뜨게 하는 메서드
    {
        Time.timeScale = 0f; // 게임 중단해서 시간 멈춤
        pausePanel.SetActive(true); // PausePanel 활성화
    }
    public void ActiveGameOverUI() // 체력 다 닳았을 때 뜨는 게임 오버 UI
    {
        gameOverPanel.SetActive(true); // 게임 오버 패널 활성화
    }
}

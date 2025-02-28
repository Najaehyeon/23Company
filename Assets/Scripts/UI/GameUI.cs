using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // ü�� �����̵� ����, ���� ������Ʈ
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
        scoreText.text = ItemManager.Instance.totalScore.ToString(); // ���� �����ͼ� �ؽ�Ʈ�� �����ֱ�
        hpSlider.value = player.currentHealth;
    }

    public void ActivePausePanel() // Pause�� �� PausePanel �߰� �ϴ� �޼���
    {
        Time.timeScale = 0f; // ���� �ߴ��ؼ� �ð� ����
        pausePanel.SetActive(true); // PausePanel Ȱ��ȭ
    }
    public void ActiveGameOverUI() // ü�� �� ����� �� �ߴ� ���� ���� UI
    {
        gameOverPanel.SetActive(true); // ���� ���� �г� Ȱ��ȭ
    }
}

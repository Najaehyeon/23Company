using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // ü�� �����̵� ����, ���� ������Ʈ


    Slider hpSlider;
    Text scoreText;

    public GameObject pausePanel;
    public GameObject gameOverPanel;

    private void Awake()
    {
        scoreText = GetComponentInChildren<Text>();
        hpSlider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        //scoreText.text = ItemManager.Instance.totalScore.ToString(); // ���� �����ͼ� �ؽ�Ʈ�� �����ֱ�
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

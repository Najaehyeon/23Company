using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : UIManager
{
    public Text bestScoreTxt; // �ְ����� �ؽ�Ʈ
    public Text currentScoreTxt; // �������� �ؽ�Ʈ

    private void Update()
    {
        currentScoreTxt.text = ItemManager.Instance.totalScore.ToString(); // ���� ���� ����
        // �ְ� ������ ������� 
    }
}

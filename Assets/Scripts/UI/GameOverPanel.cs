using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : UIManager
{
    public Text bestScoreTxt; // 최고점수 텍스트
    public Text currentScoreTxt; // 현재점수 텍스트

    private void Update()
    {
        currentScoreTxt.text = ItemManager.Instance.totalScore.ToString(); // 현재 점수 띄우기
        // 최고 점수도 띄워야함 
    }
}

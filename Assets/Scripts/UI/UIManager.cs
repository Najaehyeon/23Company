using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void Restart() // 게임 다시 시작하는 메서드 ==> 게임 오버 패널에서도 사용
    {
        SceneManager.LoadScene("GameScene");
        ItemManager.Instance.totalScore = 0;
        Time.timeScale = 1.0f;
    }

    public void Home() // 스타트 씬으로 돌아가는 메서드 => 게임 오버 패널에서도 사용
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1.0f;
    }
}

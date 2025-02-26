using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void Restart() // ���� �ٽ� �����ϴ� �޼��� ==> ���� ���� �гο����� ���
    {
        SceneManager.LoadScene("GameScene");
        Player.Instance.init();
        Time.timeScale = 1.0f;
        ItemManager.Instance.totalScore = 0;

    }

    public void Home() // ��ŸƮ ������ ���ư��� �޼��� => ���� ���� �гο����� ���
    {
        Player.Instance.init();
        Time.timeScale = 1.0f;
        
        SceneManager.LoadScene("StartScene");
    }
}

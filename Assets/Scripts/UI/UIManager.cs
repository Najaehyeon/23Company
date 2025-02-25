using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void Restart() // ���� �ٽ� �����ϴ� �޼��� ==> ���� ���� �гο����� ���
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1.0f;
    }

    public void Home() // ��ŸƮ ������ ���ư��� �޼��� => ���� ���� �гο����� ���
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1.0f;
    }
}

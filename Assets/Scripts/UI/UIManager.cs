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
        
        AudioManager.instance.BGMPlay(1); // 게임 씬용 BGM 재생
    }

    public void Home() // ��ŸƮ ������ ���ư��� �޼��� => ���� ���� �гο����� ���
    {
        Player.Instance.init();
        Time.timeScale = 1.0f;
        
        SceneManager.LoadScene("StartScene");
        
        AudioManager.instance.BGMPlay(0); // 홈 화면용 BGM 재생
    }
}

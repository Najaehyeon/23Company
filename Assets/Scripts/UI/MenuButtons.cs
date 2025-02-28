using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public SpriteRenderer playerImage;
    public GameObject optionPanel;

    public void StartGame() // 게임 시작 버튼 누를 때 실행되는 메서드
    {
        SceneManager.LoadScene("GameScene");
        AudioManager.instance.BGMPlay(1); // 게임 씬용 BGM 재생
        ItemManager.Instance.totalScore = 0;
    }

    public void ActiveOption() // 옵션 버튼 누를 때 실행되는 메서드
    {
        playerImage.sortingOrder = -1;
        optionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        optionPanel.SetActive(false);
        playerImage.sortingOrder = 1;
    }

    public void QuitGame() // Quit버튼 누를 때 실행되는 메서드
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // 에디터에서는 게임을 중지합니다.
#else
        Application.Quit();  // 빌드된 애플리케이션에서는 종료합니다.
#endif
    }
}

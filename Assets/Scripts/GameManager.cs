using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

    private void Awake() // 싱글톤
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 게임매니저는 하나만 존재하고 씬 변경시 파괴되서는 안됨
        }
        else
        {
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다");
            Destroy(gameObject);
        }
    }

    public void GameStart() // 싱글톤으로 관리되는 게임 매니저에서 해당 메서드를 통해 씬을 경유하게 되면 기존 오브젝트를 파괴받지 않고 로드 할 수 있다
    {
        SceneManager.LoadScene("SampleScene_jaehyeon");
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;  // 에디터에서는 게임을 중지합니다.
        Application.Quit();  // 빌드된 애플리케이션에서는 종료합니다.
    }
}

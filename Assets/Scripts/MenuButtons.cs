using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public SpriteRenderer playerImage;
    public GameObject optionPanel;

    public void StartGame() // ���� ���� ��ư ���� �� ����Ǵ� �޼���
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ActiveOption() // �ɼ� ��ư ���� �� ����Ǵ� �޼���
    {
        playerImage.sortingOrder = -1;
        optionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        optionPanel.SetActive(false);
        playerImage.sortingOrder = 1;
    }

    public void QuitGame() // Quit��ư ���� �� ����Ǵ� �޼���
    {
        UnityEditor.EditorApplication.isPlaying = false;  // �����Ϳ����� ������ �����մϴ�.
        Application.Quit();  // ����� ���ø����̼ǿ����� �����մϴ�.
    }
}

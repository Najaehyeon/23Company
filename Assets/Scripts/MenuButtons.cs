using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject optionPanel;

    public void StartGame() // ���� ���� ��ư ���� �� ����Ǵ� �޼���
    {
        SceneManager.LoadScene("SampleScene_jaehyeon");
    }

    public void ActiveOption() // �ɼ� ��ư ���� �� ����Ǵ� �޼���
    {
        optionPanel.SetActive(true);
    }

    public void QuitGame() // Quit��ư ���� �� ����Ǵ� �޼���
    {
        UnityEditor.EditorApplication.isPlaying = false;  // �����Ϳ����� ������ �����մϴ�.
        Application.Quit();  // ����� ���ø����̼ǿ����� �����մϴ�.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

    private void Awake() // �̱���
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���ӸŴ����� �ϳ��� �����ϰ� �� ����� �ı��Ǽ��� �ȵ�
        }
        else
        {
            Debug.LogWarning("���� �ΰ� �̻��� ���� �Ŵ����� �����մϴ�");
            Destroy(gameObject);
        }
    }

    public void LoadScene() // �̱������� �����Ǵ� ���� �Ŵ������� �ش� �޼��带 ���� ���� �����ϰ� �Ǹ� ���� ������Ʈ�� �ı����� �ʰ� �ε� �� �� �ִ�
    {
        SceneManager.LoadScene("���̸�");
    }
}

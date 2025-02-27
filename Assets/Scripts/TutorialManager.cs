using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject introKey;  // ù ��° UI
    public GameObject introUI;   // �� ��° UI
    private int clickCount = 0;  // Ŭ�� Ƚ�� ����
    private bool isTutorialActive = false; // Ʃ�丮�� Ȱ��ȭ ����

    void Start()
    {
        // ó�� ���� �ε�� �� Ʃ�丮���� ������� �ʾҴٸ� ����
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            Time.timeScale = 0f; // ���� �Ͻ� ����
            introKey.SetActive(true);
            isTutorialActive = true; // Ʃ�丮���� ���� ������ ǥ��
        }
    }

    void Update()
    {
        // Ʃ�丮���� Ȱ��ȭ�� ��쿡�� Ŭ�� �̺�Ʈ ó��
        if (isTutorialActive && Input.anyKeyDown)
        {
            clickCount++;

            if (clickCount == 1)
            {
                introKey.SetActive(false);
                introUI.SetActive(true);
            }
            else if (clickCount == 2)
            {
                introUI.SetActive(false);
                PlayerPrefs.SetInt("Tutorial", 1); // Ʃ�丮�� �Ϸ� ���� ����
                Time.timeScale = 1f; // ���� �簳
                isTutorialActive = false; // Ʃ�丮�� ����
            }
        }
    }
}
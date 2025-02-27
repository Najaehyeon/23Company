using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject introKey;  // ù ��° UI
    public GameObject introUI;   // �� ��° UI
    private int clickCount = 0;  // Ŭ�� Ƚ�� ����

    void Start()
    {
        // ó�� ���� �ε�� �� Ʃ�丮���� ������� �ʾҴٸ� ����
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            Time.timeScale = 0f; // ���� �Ͻ� ����
            introKey.SetActive(true);
        }
    }

    void Update()
    {
        // ���콺�� Ŭ���ϸ�
        if (Input.GetMouseButtonDown(0))
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
            }
        }
    }
}
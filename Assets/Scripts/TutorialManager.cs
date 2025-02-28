using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject introKey;  // 첫 번째 UI
    public GameObject introUI;   // 두 번째 UI
    private int clickCount = 0;  // 클릭 횟수 추적
    private bool isTutorialActive = false; // 튜토리얼 활성화 여부

    void Start()
    {
        // 처음 씬이 로드될 때 튜토리얼이 실행되지 않았다면 실행
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            Time.timeScale = 0f; // 게임 일시 정지
            introKey.SetActive(true);
            isTutorialActive = true; // 튜토리얼이 실행 중임을 표시
        }
    }

    void Update()
    {
        // 튜토리얼이 활성화된 경우에만 클릭 이벤트 처리
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
                PlayerPrefs.SetInt("Tutorial", 1); // 튜토리얼 완료 상태 저장
                Time.timeScale = 1f; // 게임 재개
                isTutorialActive = false; // 튜토리얼 종료
            }
        }
    }
}
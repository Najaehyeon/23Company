using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템을 관리하는 클래스 (점수 관리 등)
public class ItemManager : MonoBehaviour
{
    // 싱글톤 패턴
    private static ItemManager _instance;   // ItemManager 인스턴스를 저장하는 변수
    public static ItemManager Instance      // 싱글턴 인스턴스 접근을 위한 프로퍼티
    {
        get
        {
            // 인스턴스가 없으면 경고 메시지 띄우기
            if (_instance == null) 
            {
                Debug.LogError("ItemManager 인스턴스가 존재하지 않습니다!");
            }
            return _instance;
        }
    }
    
    public int totalScore = 0;  // 총 점수 저장 변수
    
    void Awake()
    {
        // 🔹 싱글턴 중복 방지
        if (_instance == null)  // 만약 이미 인스턴스가 존재하면 새로 생성된 객체를 삭제
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시에도 오브젝트를 파괴하지 않음
        }
        else
        {
            Destroy(gameObject);   // 이미 존재하는 인스턴스가 있으면 새로운 객체를 삭제
        }
    }
    
    // 점수 추가 메서드
    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log($"점수 추가됨: {score}, 현재 점수: {totalScore}");
    }
}

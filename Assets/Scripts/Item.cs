using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Build.Content;
using UnityEngine;

public class Item : MonoBehaviour
{
    // OnCollect() → 자식 클래스에서 오버라이드하여 아이템별 효과 추가 가능
    // OnCollisionEnter2D → 플레이어와 충돌하면 OnCollect() 호출
    
    // 아이템이 플레이어와 충돌하면 호출됨 (모든 아이템 공통 기능)
    protected virtual void OnCollect()
    {
        Debug.Log($"{gameObject.name} 아이템 획득!");
        Destroy(gameObject); // 아이템 제거
    }
    
    // 충돌 감지. 플레이어와 충돌하면 OnCollect 실행
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCollect();
        }
    }
}

public class ItemManager : MonoBehaviour
{
    // 점수 추가 (AddScore())
    // 싱글턴 패턴 사용 → ItemManager.Instance로 어디서든 접근 가능
    
    // 싱글톤
    private static ItemManager _instance;   // ItemManager 클래스의 단일 인스턴스(객체)를 저장하는 변수
    public static ItemManager Instance      // ItemManager 인스턴스에 접근할 수 있도록 하는 프로퍼티. 내부적으로 _instance를 반환해서 싱글톤을 구현
    {
        get
        {
            if (_instance == null)  // _instance가 null이면 경고 메시지를 띄움
            {
                Debug.LogError("ItemManager 인스턴스가 존재하지 않습니다!");
            }
            return _instance;
        }
    }
    
    // 점수 변수 (예시로 설정)
    public int totalScore = 0;
    
    void Awake()
    {
        // 중복 방지
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // 점수 추가 메서드
    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log($"점수 추가됨: {score}, 현재 점수: {totalScore}");
    }
}

public class ScoreItem : Item
{
    // OnCollect() 오버라이드 → 아이템이 사라질 때 점수 추가
    // 아이템 종류에 따라 점수 다르게 설정 (Awake())
    
    public enum ItemType
    {
        sign,    // 1점 아이템
        bottle,  // 2점 아이템
        chest    // 3점 아이템
    }
    
    public ItemType itemType;
    public int scoreAmount;

    public void Start()
    {
        // 아이템 종류에 따른 점수 설정
        switch (itemType)
        {
            case ItemType.sign:
                scoreAmount = 1;
                break;
            case ItemType.bottle:
                scoreAmount = 2;
                break;
            case ItemType.chest:
                scoreAmount = 3;
                break;
        }
    }
    
    // 아이템 수집 시 점수 추가 (부모 클래스의 OnCollect()를 오버라이드)
    protected override void OnCollect()
    {
        base.OnCollect(); // 아이템 제거(부모 클래스의 OnCollect 실행)
        ItemManager.Instance.AddScore(scoreAmount); // 점수 추가
    }
}

public class EffectItem : Item
{
    // OnCollect() 오버라이드 → 힐 or 속도 증가 기능 적용
    // Heal(), SpeedUp() → 효과 적용 (나중에 추가할 수도 있음)
    
    public enum ItemType
    {
        chicken,    // 체력 회복
        fireball    // 속도 증가
    }
    
    public ItemType itemType;

    // 아이템 수집 시 효과 적용 (부모 클래스의 OnCollect()를 오버라이드)
    protected override void OnCollect()
    {
        base.OnCollect(); // 아이템 제거

        switch (itemType)
        {
            case ItemType.chicken:
                Heal();
                break;
            case ItemType.fireball:
                SpeedUp();
                break;
        }
    }

    void Heal()
    {
        Debug.Log("체력 회복!");
    }
    
    void SpeedUp()
    {
        Debug.Log("속도 증가!");
    }
}

public class ItemSpawner
{
    
}
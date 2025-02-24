using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 점수형 아이템 클래스
public class ScoreItem : Item
{
    public enum ItemType
    {
        Sign,    // 1점 아이템
        Bottle,  // 2점 아이템
        Coin,    // 3점 아이템
        Chest    // 4점 아이템
    }
    
    public ItemType itemType;   // 아이템 종류
    public int scoreAmount;     // 아이템 점수 값

    public void Start()
    {
        // 아이템 종류에 따라 점수 값 설정
        switch (itemType)
        {
            case ItemType.Sign:
                scoreAmount = 1;
                break;
            case ItemType.Bottle:
                scoreAmount = 2;
                break;
            case ItemType.Coin:
                scoreAmount = 3;
                break;
            case ItemType.Chest:
                scoreAmount = 4;
                break;
        }
    }
    
    // 아이템 획득 시 점수를 추가
    protected override void OnCollect()
    {
        base.OnCollect(); // 아이템 제거 (부모 클래스의 OnCollect 실행)
        ItemManager.Instance.AddScore(scoreAmount); // 점수 추가
    }
}

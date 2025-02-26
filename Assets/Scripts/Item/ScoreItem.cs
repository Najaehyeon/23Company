using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 점수형 아이템 클래스
public class ScoreItem : Item
{
    public enum ItemType
    {
        Sign,    // 10점 아이템
        Bottle,  // 20점 아이템
        Coin,    // 30점 아이템
        Chest,   // 40점 아이템
        Bone     // 50점 아이템
    }

    [SerializeField] private ItemType itemType;   // 아이템 종류
    private int scoreAmount;     // 아이템 점수 값

    public void Start()
    {
        // 아이템 종류에 따라 점수 값 설정
        switch (itemType)
        {
            case ItemType.Sign:
                scoreAmount = 10;
                break;
            case ItemType.Bottle:
                scoreAmount = 20;
                break;
            case ItemType.Coin:
                scoreAmount = 30;
                break;
            case ItemType.Chest:
                scoreAmount = 40;
                break;
            case ItemType.Bone:
                scoreAmount = 50;
                break;
        }
    }

    // 아이템 획득 시 점수를 추가
    protected override void OnCollect(Player player)
    {
        if (!Player.Instance.isInvincible)
        {
            base.OnCollect(player); // 아이템 제거 (부모 클래스의 OnCollect 실행)
            ItemManager.Instance.AddScore(scoreAmount); // 점수 추가
        }
    }
}

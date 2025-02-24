using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기능형 아이템 클래스
public class EffectItem : Item
{
    public enum ItemType
    {
        Chicken,    // 체력 회복
        Fireball    // 속도 증가
    }
    
    public ItemType itemType;   // 아이템 종류

    // 아이템 획득 시 효과를 적용
    protected override void OnCollect()
    {
        base.OnCollect(); // 아이템 제거 (부모 클래스의 OnCollect 실행)

        switch (itemType)
        {
            case ItemType.Chicken:
                Heal();
                break;
            case ItemType.Fireball:
                SpeedUp();
                break;
        }
    }

    // 체력 회복 기능
    void Heal()
    {
        Debug.Log("체력 회복!");
    }
    
    // 속도 증가 기능
    void SpeedUp()
    {
        Debug.Log("속도 증가!");
    }
}

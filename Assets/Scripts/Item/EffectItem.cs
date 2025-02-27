using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// 기능형 아이템 클래스
public class EffectItem : Item
{
    public enum ItemType
    {
        Chicken,    // HP 회복
        Fireball,   // 속도 증가
        Poison,     // HP 지속 감소
        HpPotion    // HP 회복
    }

    public ItemType itemType;   // 아이템 종류

    // 아이템 획득 시 효과 적용 (Item 클래스의 OnCollect를 오버라이드)
    protected override void OnCollect(Player player)
    {
        if (!Player.Instance.isInvincible)
        {
            ApplyEffect(player);    // 플레이어에게 효과 적용
            base.OnCollect(player); // 아이템 제거
        }
    }

    // 아이템 효과 적용
    private void ApplyEffect(Player player)
    {
        switch (itemType)
        {
            case ItemType.Chicken:
                player.Heal(10);    // 체력 10 회복
                break;
            case ItemType.Fireball:
                player.SpeedUp(5f, 5f); // 속도 2 증가, 5초 후 원래 속도로 복귀
                break;
            case ItemType.Poison:
                player.StartCoroutine(player.TakePoisonDamage(3, 5, 0.5f)); // 3 데미지, 5번 반복, 0.5초 간격
                break;
            case ItemType.HpPotion:
                player.Heal(15);    // 체력 15 회복
                break;
        }
    }
}

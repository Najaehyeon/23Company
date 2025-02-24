using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// 기능형 아이템 클래스
public class EffectItem : Item
{
    public enum ItemType
    {
        Chicken,    // 체력 회복
        Fireball,   // 속도 증가
        Poison      // 체력 감소
    }
    
    public ItemType itemType;   // 아이템 종류

    // 아이템 획득 시 효과 적용
    protected override void OnCollect()
    {
        base.OnCollect(); // 아이템 제거 (부모 클래스의 OnCollect 실행)

        // Player player = FindObjectOfType<Player>(); // 플레이어 찾기
        //
        // if (player != null)
        // {
        //     switch (itemType)
        //     {
        //         case ItemType.Chicken:
        //             player.Heal(10);    // 체력 10 회복
        //             break;
        //         case ItemType.Fireball:
        //             player.SpeedUp(2f, 5f); // 속도 2 증가, 5초 후 원래 속도로 복귀
        //             break;
        //         case ItemType.Poison:
        //             player.TakeDamage(10);  // 체력 10 감소
        //             break;
        //     }
        // }
    }
    
    // //플레이어 클래스에 넣어주기
    // public void Heal(int amount)
    // {
    //     currentHealth += amount;  // 체력 회복
    //     if (currentHealth >= maxHealth) 
    //     {
    //         currentHealth = maxHealth; // 최대 체력 초과 방지
    //     }
    // }
    
    // //플레이어 클래스에 넣어주기
    // public void TakeDamage(int amount)
    // {
    //     currentHealth -= amount;  // 체력 감소
    //     if (currentHealth <= 0)
    //     {
    //         currentHealth = 0; // 체력이 0 이하로 내려가지 않도록 방지
    //     }
    // }
    
    // //플레이어 클래스에 넣어주기
    // public void SpeedUp(float amount, float duration)
    // {
    //     speed += amount; // 속도 증가
    //     Invoke(nameof(ResetSpeed), duration);   // 일정 시간 후 원래 속도로 복귀
    // }
    // private void ResetSpeed()   // 속도를 원래대로 초기화
    // {
    //     speed -= 2f; // 원래 속도로 복귀
    // }
}

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
        Poison      // HP 지속 감소
    }
    
    public ItemType itemType;   // 아이템 종류
    
    // 아이템 획득 시 효과 적용 (Item 클래스의 OnCollect를 오버라이드)
    protected override void OnCollect(Player player)
    {
        ApplyEffect(player);    // 플레이어에게 효과 적용
        base.OnCollect(player); // 아이템 제거
    }
    
    // 아이템 효과 적용
    private void ApplyEffect(Player player)
    {
        // switch (itemType)
        // {
        //     case ItemType.Chicken:
        //         player.Heal(10);    // 체력 10 회복
        //         break;
        //     case ItemType.Fireball:
        //         player.SpeedUp(2f, 5f); // 속도 2 증가, 5초 후 원래 속도로 복귀
        //         break;
        //     case ItemType.Poison:
        //         player.StartCoroutine(player.TakePoisonDamage(3, 5, 0.5f)); // 3 데미지, 5번 반복, 0.5초 간격
        //         break;
        // }
    }
    
    // //플레이어 클래스에 넣어주기
    // public void Heal(int amount)    // 플레이어 HP 회복
    // {
    //     currentHealth += amount;  // 체력 회복
    //     if (currentHealth >= maxHealth) 
    //     {
    //         currentHealth = maxHealth; // 최대 체력 초과 방지
    //     }
    // }
    //
    // //플레이어 클래스에 넣어주기
    // public void SpeedUp(float amount, float duration)   // 플레이어 속도 증가
    // {
    //     speed += amount; // 속도 증가
    //     Invoke(nameof(ResetSpeed), duration);   // 일정 시간 후 원래 속도로 복귀
    // }
    // private void ResetSpeed()   // 속도를 원래대로 초기화
    // {
    //     speed -= 2f; // 원래 속도로 복귀(규태님 나중에 2f 대신 플레이어 기본 속도로 넣어주세여)
    // }
    //
    // //플레이어 클래스에 넣어주기
    // public void TakeDamage(int amount)
    // {
    //     currentHealth -= amount;  // 체력 감소
    //     if (currentHealth <= 0)
    //     {
    //         currentHealth = 0; // 체력이 0 이하로 내려가지 않도록 방지
    //     }
    // }
    //
    // //플레이어 클래스에 넣어주기
    // public IEnumerator TakePoisonDamage(int damage, int times, float interval)  // 일정 시간마다 HP 감소하는 코루틴
    // {
    //     for (int i = 0; i < times; i++)
    //     {
    //         TakeDamage(damage);
    //         Debug.Log($"독 데미지! 피해 {damage}, 현재 체력 {currentHealth}, 남은 횟수 {times - i - 1}번");
    //         yield return new WaitForSeconds(interval);  // n초(interval) 대기
    //     }
    // }
}

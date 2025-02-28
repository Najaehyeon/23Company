using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour // 1-20강
{
    //public int maxHealth = 100;  // 최대 체력
    //private int currentHealth;   // 현재 체력

    //public float damageInterval = 2f;  // 체력 감소 주기 (2초마다 감소)
    //public int damageAmount = 5;       // 감소할 체력량

    //private Animator animator;         // 애니메이터
    //private bool isTakingDamage = false;  // 체력이 감소하는 중인지 확인

    //public Slider healthBar;  // UI 체력바

    //void Start()
    //{
    //    currentHealth = maxHealth;
    //    animator = GetComponent<Animator>();

    //    StartCoroutine(DamageOverTime());  // 체력 감소 코루틴 시작
    //}

    //IEnumerator DamageOverTime()
    //{
    //    while (currentHealth > 0)
    //    {
    //        yield return new WaitForSeconds(damageInterval);  // 일정 시간 기다림
    //        TakeDamage(damageAmount);
    //    }
    //}

    //void TakeDamage(int damage)
    //{
    //    if (currentHealth <= 0) return;  // 체력이 0 이하라면 실행하지 않음

    //    currentHealth -= damage;  // 체력 감소
    //    if (healthBar != null)
    //        healthBar.value = (float)currentHealth / maxHealth;  // 체력 UI 업데이트

    //    Debug.Log("체력 감소: " + currentHealth);

    //    // 애니메이션 실행
    //    if (animator != null)
    //    {
    //        animator.SetTrigger("TakeDamage");
    //    }

    //    if (currentHealth <= 0)
    //    {
    //        Die();
    //    }
    //}

    //void Die()
    //{
    //    Debug.Log("플레이어 사망!");
    //    if (animator != null)
    //    {
    //        animator.SetTrigger("IsDie");  // 사망 애니메이션 실행
    //    }
    //    StopAllCoroutines();  // 체력 감소 멈춤
    //}
}
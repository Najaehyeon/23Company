using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour // 1-20��
{
    //public int maxHealth = 100;  // �ִ� ü��
    //private int currentHealth;   // ���� ü��

    //public float damageInterval = 2f;  // ü�� ���� �ֱ� (2�ʸ��� ����)
    //public int damageAmount = 5;       // ������ ü�·�

    //private Animator animator;         // �ִϸ�����
    //private bool isTakingDamage = false;  // ü���� �����ϴ� ������ Ȯ��

    //public Slider healthBar;  // UI ü�¹�

    //void Start()
    //{
    //    currentHealth = maxHealth;
    //    animator = GetComponent<Animator>();

    //    StartCoroutine(DamageOverTime());  // ü�� ���� �ڷ�ƾ ����
    //}

    //IEnumerator DamageOverTime()
    //{
    //    while (currentHealth > 0)
    //    {
    //        yield return new WaitForSeconds(damageInterval);  // ���� �ð� ��ٸ�
    //        TakeDamage(damageAmount);
    //    }
    //}

    //void TakeDamage(int damage)
    //{
    //    if (currentHealth <= 0) return;  // ü���� 0 ���϶�� �������� ����

    //    currentHealth -= damage;  // ü�� ����
    //    if (healthBar != null)
    //        healthBar.value = (float)currentHealth / maxHealth;  // ü�� UI ������Ʈ

    //    Debug.Log("ü�� ����: " + currentHealth);

    //    // �ִϸ��̼� ����
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
    //    Debug.Log("�÷��̾� ���!");
    //    if (animator != null)
    //    {
    //        animator.SetTrigger("IsDie");  // ��� �ִϸ��̼� ����
    //    }
    //    StopAllCoroutines();  // ü�� ���� ����
    //}
}
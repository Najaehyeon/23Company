using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Build.Content;
using UnityEngine;

// 아이템 기본 클래스 (부모 클래스)
public class Item : MonoBehaviour
{
    // +++++ 오디오 관련 +++++
    AudioSource _audioSource;   // 오디오 소스
    public AudioClip itemSound; // 아이템 획득 효과음

    // +++++ 오디오 관련 +++++
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>(); // 오디오 소스 가져오기
    }

    // 플레이어가 아이템을 획득했을 때 실행되는 메서드 (모든 아이템 공통)
    protected virtual void OnCollect(Player player)
    {
        // +++++ 오디오 관련 +++++
        if (_audioSource != null && itemSound != null)
        {
            AudioSource.PlayClipAtPoint(itemSound, transform.position);    // 효과음 재생
            
            // _audioSource.PlayOneShot(itemSound);
            // Destroy(gameObject, itemSound.length); // 효과음 길이만큼 기다렸다가 삭제 (이렇게 하면 아이템 바로 안 사라짐..)
        }
        
        Destroy(gameObject); // 아이템 제거
        Debug.Log($"{gameObject.name} 아이템 획득!");
    }
    
    // 충돌 감지. 플레이어와 충돌하면 OnCollect 실행
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) // "Player" 태그 오브젝트와 충돌했는지 확인
        {
            Player player = collider.GetComponent<Player>();    // 충돌한 오브젝트에서 Player 가져오기
            if (player != null)
            {
                OnCollect(player);    // 아이템 획득 처리
            }
            else
            {
                Debug.LogWarning("플레이어 객체가 없습니다!");
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// SFX(게임 효과음) 열거형
public enum SFXType
{
    Jump,           // Element 0
    Slide,          // Element 1
    Hit,            // Element 2
    Die,            // Element 3
    ScoreItem,      // Element 4
    ItemChicken,    // Element 5
    ItemPoison,     // Element 6
    ItemFireball    // Element 7
}

// UIS(UI 효과음) 열거형
public enum UISType
{
    PopUp
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;    // 싱글톤을 위한 인스턴스

    public AudioSource BGM, SFX, UIS;               // 각 유형별 오디오 소스
    public AudioClip[] BGMArr, SFXArr, UISArr;      // 각 오디오 클립 배열
    
    public AudioSource[] childBGMs;

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 씬이 변경되어도 파괴 X
        }
        else
        {
            Destroy(gameObject);    // 기존 AudioManager가 있다면 현재 오브젝트를 삭제
        }
    }

    private void Start()
    {
        if (BGM != null && BGM.clip == null)
        {
            for (int i = 0; i < BGM.clip.length; i++)
            {
                childBGMs[i].clip = Resources.Load<AudioClip>(BGM.clip.name); 
            }
        }
    }

    // BGM 재생
    public void BGMPlay(int numb)
    {
        // 이미 BGM이 재생 중이라면 정지
        if (BGM.isPlaying)
        {
            BGM.Stop();
        }
        
        // 배열 범위 내에서만 실행되도록 검사
        if (numb >= 0 && numb < BGMArr.Length)
        {
            BGM.clip = BGMArr[numb];    // 선택한 BGM 클립 설정
            BGM.Play();                 // BGM 재생
        }
    }

    // SFX 재생
    public void SFXPlay(SFXType type)
    {
        int index = (int)type;      // SFXType 열거형을 정수로 변환
        
        // 배열 범위 내에서만 실행되도록 검사
        if (index >= 0 && index < SFXArr.Length)
        {
            SFX.PlayOneShot(SFXArr[index]); // 효과음 한 번 재생
        }
    }
    
    // UI 사운드 재생
    public void UISPlay(UISType type)
    {
        int index = (int)type;      // UISType 열거형을 정수로 변환
        
        // 배열 범위 내에서만 실행되도록 검사
        if (index >= 0 && index < UISArr.Length)
        {
            UIS.PlayOneShot(UISArr[index]); // 효과음 한 번 재생
        }
    }
}
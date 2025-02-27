using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXType
{
    Jump,
    Slide,
    Die,
    Item,
    Posion
}

public class AudioManager_LYR : MonoBehaviour
{
    public static AudioManager_LYR instance;

    public AudioSource BGM, SFX;
    public AudioClip[] BGMArr, SFXArr;
    
    // 예시) SFXArr: 0 Jump, 1 Slide, 2 Die, 3 Item, 4 Posion 등

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void BGMPlay(int numb)
    {
        if (BGM.isPlaying)
        {
            BGM.Stop();
        }
        
        BGM.clip = BGMArr[numb];
        BGM.Play();
    }

    public void SFXPlay(SFXType type)
    {
        SFX.PlayOneShot(SFXArr[(int)type]);
    }
}

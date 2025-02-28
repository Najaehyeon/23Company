using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource backgroundSound;

    Slider soundSlider;

    private void Awake()
    {
        soundSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        backgroundSound.volume = soundSlider.value;
    }
}

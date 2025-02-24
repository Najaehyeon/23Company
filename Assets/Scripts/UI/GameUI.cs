using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    Slider hpSlider;
    Text scoreText;

    int score;

    private void Awake()
    {
        scoreText = GetComponentInChildren<Text>();
        hpSlider = GetComponentInChildren<Slider>();
    }
}

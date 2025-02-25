using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : UIManager
{
    public void Resume() // 게임 재개하는 메서드
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}

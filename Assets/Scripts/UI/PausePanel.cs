using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : UIManager
{
    public void Resume() // ���� �簳�ϴ� �޼���
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}

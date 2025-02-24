using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pauseUI;

    public void Pause()
    {
        pauseUI.SetActive(true);
    }
}

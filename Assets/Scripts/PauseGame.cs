using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseBck;

    public GameObject resumeBtn;

    public void Pause()
    {
        pauseBck.SetActive(true);
        resumeBtn.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseBck.SetActive(false);
        resumeBtn.SetActive(false);
        Time.timeScale = 1;
    }
}

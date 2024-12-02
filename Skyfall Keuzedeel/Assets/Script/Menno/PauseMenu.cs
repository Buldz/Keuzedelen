using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    private bool isPaused = false;


    private void Start()
    {
        isPaused = false;
        PauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Play()
    {
        isPaused = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
}

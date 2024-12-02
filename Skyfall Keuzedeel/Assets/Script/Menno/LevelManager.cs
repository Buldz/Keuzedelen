using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string SceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
        System.Console.WriteLine("BYE!!!");
    }
}

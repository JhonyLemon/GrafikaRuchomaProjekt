using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

public class PauseMenu : MonoBehaviour
{
    public static bool PausedGame = false;

    public GameObject PauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(PausedGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
        {
            a.Play();
        }
        PausedGame = false;
    }

    public void Pause ()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach(AudioSource a in audios)
        {
            a.Pause();
        }
        PausedGame = true;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene(0);
        PausedGame = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
}

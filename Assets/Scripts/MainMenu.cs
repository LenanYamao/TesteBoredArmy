using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    private void Start()
    {
        var delay = PlayerPrefs.GetFloat("spawnDelay");
        if(delay == 0)
        {
            PlayerPrefs.SetFloat("spawnDelay", 1f);
        }
        var duration = PlayerPrefs.GetInt("duration");
        if(duration == 0)
        {
            PlayerPrefs.SetInt("duration", 60);
        }
    }
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}

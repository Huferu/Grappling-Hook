using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource aSource;
    public Slider mouseSens;
    public Slider musicVol;
    public static float mouseSensetivity = 0.5f;
    public static float musicVolume = 0.5f;

    private void Start()
    {
        mouseSens.value = mouseSensetivity;
        musicVol.value = musicVolume;
    }

    public void OnSenseChanged()
    {
       mouseSensetivity = mouseSens.value;
    }

    public void OnVolumeChange()
    {
        musicVolume = musicVol.value;
        aSource.volume = musicVolume;
    }

        public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}

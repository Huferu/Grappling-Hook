using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int currentLevel;
    private AudioSource winMusic;
    private AudioSource player;
    public AudioClip clickSound;
    public AudioClip keySound;
    public AudioClip switchSound;

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        winMusic = GetComponent<AudioSource>();
        player = gameObject.AddComponent<AudioSource>();
        currentLevel = 0;

    }

    public void LoadLevel(int levelNr)
    {
        currentLevel = levelNr;
        SceneManager.LoadScene(levelNr);
        winMusic.Stop();

    }
    public void ReloadLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        LoadLevel(0);
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        LoadLevel(currentLevel);

    }

    public void PlayWinMusic()
    {
        winMusic.Play();
    }

    public void PlayClick()
    {
        player.clip = clickSound;
        player.Play();

    }

    public void PlaySwitch()
    {
        player.clip = switchSound;
        player.Play();
    }

    public void PlayKey()
    {
        player.clip = keySound;
        player.Play();
    }
}

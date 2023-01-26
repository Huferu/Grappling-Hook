using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumController : MonoBehaviour
{
    private AudioSource aSource;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
        aSource.volume = MainMenu.musicVolume;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSet : MonoBehaviour
{
    private float gunVolume;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gunVolume=GameManager.instance.GetSaveFloat("GunVolume",1f);
        audioSource.volume *= gunVolume;
    }
}

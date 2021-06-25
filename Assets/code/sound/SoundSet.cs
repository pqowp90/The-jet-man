using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSet : MonoBehaviour
{
    private float gunVolume;
    private AudioSource audioSource;
    [SerializeField]
    private string saveName; 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gunVolume=GameManager.instance.GetSaveFloat(saveName,1f);
        audioSource.volume *= gunVolume;
    }
}

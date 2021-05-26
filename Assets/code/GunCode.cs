using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCode : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClip;
    [SerializeField]
    private playerMove playerMove;
    private AudioSource audioSource;
    private float gunVolume;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gunVolume=GameManager.instance.GetSaveFloat("GunVolume",0.3f);
    }

    private void BarSsaChong(){
        playerMove.Shoting(2.6f);
    }
    private void DdangSound(){
        audioSource.clip = audioClip[0];
        audioSource.pitch=0.8f;
        audioSource.volume = 0.3f;
        Play();
    }
    private void DadaSound(){
        audioSource.clip = audioClip[1];
        audioSource.pitch=1f;
        audioSource.volume = 0.6f;
        Play();
    }
    
    private void PpangSound(){
        audioSource.clip = audioClip[2];
        audioSource.pitch=1.2f;
        audioSource.volume = 0.17f;
        Play();
    }
    private void Play(){
        audioSource.volume *= gunVolume;
        audioSource.time = 0f;
        audioSource.Play();
    }
}

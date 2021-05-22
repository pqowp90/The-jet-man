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
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        audioSource.volume = 1f;
        Play();
    }
    
    private void PpangSound(){
        audioSource.clip = audioClip[2];
        audioSource.pitch=1.2f;
        audioSource.volume = 0.2f;
        Play();
    }
    private void Play(){
        audioSource.time = 0f;
        audioSource.Play();
    }
}

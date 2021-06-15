using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClip;
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
    }
    private void SoundGogo()
    {
        audioSource.volume = 0.4f;
        audioSource.clip = audioClip[Random.Range(0,3)];
        audioSource.time = 0f;
        audioSource.Play();
    }
    private void JumpSound()
    {
        audioSource.volume = 1f;
        audioSource.clip = audioClip[3];
        audioSource.time = 0f;
        audioSource.Play();
    }
}

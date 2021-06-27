using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundMusic : MonoBehaviour
{
    
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClip;
    private int num;
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume",0.3f);
    }
    public void FaidOut(){
        StartCoroutine(Restart());
    }
    public IEnumerator Restart(){
        audioSource.DOFade(0f,1.3f);
        yield return new WaitForSeconds(1.3f);
        audioSource.clip = audioClip[num];
        audioSource.time = 7.7f;
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume",0.3f);
        MusicStart();
    }
    public void MusicStart(){
        audioSource.Stop();
        //audioSource.time = 0f;
        audioSource.Play();
    }
}

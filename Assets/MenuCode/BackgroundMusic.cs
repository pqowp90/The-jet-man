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
    private float startVol;
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        startVol = audioSource.volume;
    }
    public void FaidOut(){
        StartCoroutine(Restart());
    }
    public IEnumerator Restart(){
        audioSource.DOFade(0f,1.3f);
        yield return new WaitForSeconds(1.3f);
        
        audioSource.clip = audioClip[num];
        audioSource.time = 7.7f;
        MusicStart();
    }
    public void MusicStart(){
        audioSource.DOFade(startVol,0.2f);
        //audioSource.volume = startVol;
        audioSource.Stop();
        //audioSource.time = 0f;
        audioSource.Play();
    }
}

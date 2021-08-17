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
    [SerializeField]
    private AudioSource[] audioSources;
    int biggest;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gunVolume=GameManager.instance.GetSaveFloat("GunVolume",1f);
    }

    private void BarSsaChong(){
        playerMove.Shoting(2.6f);
    }
    private void DdangSound(){
        FindEmpty(0f,1f,0.4f,audioClip[0]);
    }
    private void DadaSound(){
        FindEmpty(0f,1f,0.6f,audioClip[1]);
    }
    
    private void PpangSound(){
        FindEmpty(0f,1.2f,0.17f,audioClip[2]);
    }
    private void Dadadadadadadada(){
        FindEmpty(0f,1f,0.17f,audioClip[3]);
    }
    private void darararararara(){
        FindEmpty(0f,1.1f,0.17f,audioClip[4]);
    }

    private void FindEmpty(float time,float pitch,float volume,AudioClip clip){
        for(int i=0;i<audioSources.Length;i++){
            if(!audioSources[i].isPlaying){
                audioSources[i].clip = clip;
                audioSources[i].pitch=pitch;
                audioSources[i].volume = volume;
                audioSources[i].time = time;
                audioSources[i].Play();
                break;
            }
        }
        biggest = 0;
        for(int i=1;i<audioSources.Length;i++){
            if(audioSources[i].time>audioSources[biggest].time){
                biggest = i;
            }
        }
        audioSources[biggest].clip = clip;
        audioSources[biggest].pitch=pitch;
        audioSources[biggest].volume = volume;
        audioSources[biggest].time = time;
        audioSources[biggest].Play();
    }
}

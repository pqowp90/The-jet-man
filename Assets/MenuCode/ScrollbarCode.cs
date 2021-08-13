using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ScrollbarCode : MonoBehaviour
{
    [SerializeField]
    private float firstNum;
    [SerializeField]
    public string abc;
    [SerializeField]
    private Text Num;



    private Slider scrollbar;
    private AudioMixer mixer;
    void Start()
    {
        mixer = Resources.Load<AudioMixer>("mixer");
        scrollbar = GetComponent<Slider>();
        scrollbar.value = GameManager.instance.GetSaveFloat(abc,firstNum);
    }

    void Update()
    {
        Num.text = string.Format("{0}",(int)(scrollbar.value*100));
    }
    public void Set(){
        GameManager.instance.SetSaveFloat(abc,scrollbar.value);
    }
    public void SoundVolume(float val){
        mixer.SetFloat(abc,Mathf.Log10(val)*20);
    }
}

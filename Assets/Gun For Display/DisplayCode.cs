using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DisplayCode : MonoBehaviour
{
    [SerializeField]
    private Transform hi;
    [SerializeField]
    private float coolTime=0f;
    [SerializeField]
    private Check[] hicol = new Check[2];
    void Start()
    {
        
    }
    void Update(){
        if(coolTime>=0){
            coolTime-=Time.deltaTime;
        }
    }
    public void ButtenDowntownBaby(int hihihi){
        
        if(coolTime>=0)return;
        if(hihihi == 0){
            if(!hicol[0].ohohoh)return;
            //Right
            hi.DOLocalMoveX(hi.localPosition.x-600f,0.35f);
        }
        else{
            if(!hicol[1].ohohoh)return;
            //Left
            hi.DOLocalMoveX(hi.localPosition.x+600f,0.35f);
        }
        coolTime = 0.5f;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class multyName : MonoBehaviour
{    
    public Image hpbar;
    public float hp=1f;
    void Start()
    {
    }

    void Update()
    {
        
        hpbar.fillAmount = hp;
    }
}

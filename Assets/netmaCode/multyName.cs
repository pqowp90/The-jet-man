using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class multyName : MonoBehaviour
{    
    public Image hpbar;
    public float hp=1f;
    Transform player;
    void Start()
    {
        player=transform.parent.GetChild(1).transform;
    }

    void Update()
    {
        if(player==null)
            Destroy(gameObject);
        hpbar.fillAmount = hp;
    }
}

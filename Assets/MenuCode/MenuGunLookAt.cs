using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGunLookAt : MonoBehaviour
{
    [SerializeField]
     private Animator gunAnimator;
    public int GunSet=0;
    void Start()
    {
        
    }

    void Update()
    {
        gunAnimator.SetInteger("GunSet",GunSet);
    }
}

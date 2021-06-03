﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed=10;
    public ParticleSystem ps;
    [SerializeField]
    private bool isPull;
    private Animator animator;
    public int bulletSet;
    public void Awake(){
        animator = GetComponent<Animator>();
    }
    public void StartDeley(){
        Invoke("DestroyBullet",3f);
    }
    private void Update()
    {
        if(animator!=null)
            animator.SetInteger("BulletSet",bulletSet);
        if(ps!=null){//잔상회전
            ParticleSystem.MainModule main = ps.main;
            if(main.startRotation.mode == ParticleSystemCurveMode.Constant)
                main.startRotation = -transform.eulerAngles.z*Mathf.Deg2Rad;
        }
        //-----------------------------------------------------------------------------------------
        transform.Translate(Vector2.right*speed*Time.deltaTime);
    }
    private void DestroyBullet(){
        ObjectPoolling.ReturnObject(this);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    public ParticleSystem ps;
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ps!=null){
            ParticleSystem.MainModule main = ps.main;
            if(main.startRotation.mode == ParticleSystemCurveMode.Constant)
                main.startRotation = -transform.eulerAngles.z*Mathf.Deg2Rad;
        }
    }
}

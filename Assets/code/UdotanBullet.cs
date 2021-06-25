using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UdotanBullet : BulletMove
{
    private Vector3 diff;
    private Transform playertransform;
    private float rotationZ;
    public float myRotationZ;
    private float myTime,lookTime=2f;
    private void Start(){
        playertransform = GameManager.instance.player.transform;
    }
    private void LookAt(){
        myTime+=Time.deltaTime;
        diff = playertransform.position - transform.position;
        diff.Normalize();
        rotationZ = Mathf.Atan2(diff.y,diff.x)*Mathf.Rad2Deg;
        myRotationZ+=(((myRotationZ>rotationZ)?-100:100)+Random.Range(-80f,80f))*Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y,myRotationZ);
    }
    public void Reset(){
        myTime=0;
    }
    protected override void Update()
    {
        base.Update();
        if(myTime<lookTime)
            LookAt();
    }
    
}

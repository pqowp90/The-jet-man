using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDrone1 : EnemyMove
{
    private Vector3 diff;
    private float rotationZ;
    private Transform myGun;
    private float gunX,gunY,gunRadian,gunRotateDegree;
    [SerializeField]
    private Transform barSsaPos;
    [SerializeField]
    private int gunDamage,gunStun;
    private float shotingtime;
    private Transform playertransform;
    private AllPoolManager allPoolManager;
    [SerializeField]
    private float shotDeley=2f;
    void Start()
    {
        allPoolManager = GameManager.instance.allPoolManager;
        playertransform = GameManager.instance.player.transform;
        myRigidbodyhi = GetComponent<Rigidbody2D>();
        myGun = transform.GetChild(0);
    }
    
    protected override void Move()
    {
        base.Move();
        if(myGun==null){
            myGun = transform.GetChild(0);
        }
        GunAngle();
        aBarssaDeley();
    }
    private void GunAngle(){
        diff = playertransform.position - transform.position;
        diff.Normalize();
        rotationZ = Mathf.Atan2(diff.y,diff.x)*Mathf.Rad2Deg;
        myGun.rotation = Quaternion.Euler(myGun.rotation.x,myGun.rotation.y,rotationZ);
        gunRadian = rotationZ*Mathf.PI/180f;
        gunX = 80 * Mathf.Cos(gunRadian);
        gunY = 80 * Mathf.Sin(gunRadian);
    }
    private void aBarssaDeley(){
        shotingtime+=Time.deltaTime;
        if(shotingtime>shotDeley){
            shotingtime=0f;
            Shoting();
        }
    }
    private void Shoting(){
        var bullet = allPoolManager.GetPool(4).GetComponent<BulletMove>();
        if(bullet==null)return;

        myRigidbodyhi.AddForce(new Vector3(-gunX,-gunY,0f));
        bullet.transform.position=barSsaPos.transform.position;
        bullet.transform.rotation = Quaternion.Euler(0,0,rotationZ);
        bullet.bulletSet = 0;
        bullet.bulletDagage = gunDamage;
        bullet.stun = gunStun;
        bullet.gameObject.SetActive(true);
        transform.DOKill();
    }


    protected override void Reset(){
        base.Reset();
        Debug.Log("dd");
    }
    
}

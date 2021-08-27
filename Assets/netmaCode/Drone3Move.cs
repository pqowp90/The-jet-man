using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Drone3Move : EnemyMove
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
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
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
        aBarssaDeley();
    }
    
    private void aBarssaDeley(){
        shotingtime+=Time.deltaTime;
        if(shotingtime>shotDeley){
            shotingtime=0f;
            animator.SetTrigger("Shot");
        }
    }
    
    public void Shoting(){
        transform.DOKill();
        var bullet = allPoolManager.GetPool(10).GetComponent<BulletMove>();
        if(bullet==null)return;
        
        myRigidbodyhi.AddForce(new Vector3(-gunX,-gunY,0f));
        bullet.transform.position=barSsaPos.transform.position;
        bullet.transform.rotation = Quaternion.Euler(0,0,90f);
        bullet.bulletSet = 0;
        bullet.bulletDagage = gunDamage;
        bullet.stun = gunStun;
        bullet.gameObject.SetActive(true);
    }
}

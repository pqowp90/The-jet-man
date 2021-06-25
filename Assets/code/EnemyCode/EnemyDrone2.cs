using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDrone2 : EnemyMove
{
    private Vector3 diff;
    private Transform myGun;
    private float gunRadian,gunRotateDegree;
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
            animator.SetTrigger("Attack");
        }
    }
    protected override void Die(Collider2D other)
    {
        base.Die(other);
    }
    public void Shoting(){
        transform.DOKill();
        for(int i=0;i<6;i++){
            if(Random.Range(0,2)==0)continue;
            var bullet = allPoolManager.GetPool(4).GetComponent<BulletMove>();
            if(bullet==null)return;
            bullet.transform.position=transform.position+new Vector3((i>=3)?-0.1f:0.1f,((i+1)%3f)*0.13f,0f);
            bullet.GetComponent<UdotanBullet>().myRotationZ=(i>=3)?180:0f;
            bullet.GetComponent<UdotanBullet>().Reset(); 
            bullet.bulletSet = 0;
            bullet.bulletDagage = gunDamage;
            bullet.stun = gunStun;
            bullet.gameObject.SetActive(true);
            bullet.GetComponent<Animator>().SetBool("Bolt",true);
        }
    }
}
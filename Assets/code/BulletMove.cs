using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed=10;
    public ParticleSystem ps;
    [SerializeField]
    private bool isUdo;
    private Animator animator;
    public int bulletSet;
    public int bulletDagage;
    public float stun=200f;
    [SerializeField]
    private bool enemy;
    private AllPooler allPooler;
    [SerializeField]
    private GameObject DesEffect;
    [SerializeField]
    private bool misa;
    public void Awake(){
        allPooler = GetComponent<AllPooler>();
        animator = GetComponent<Animator>();
    }
    public void StartDeley(){
        CancelInvoke();
        Invoke("DestroyBullet",3f);
    }

    protected virtual void Update()
    {
        if(GameManager.instance.isEnding)
            Destroy(gameObject);
        if(!enemy){
            if(animator!=null)
            animator.SetInteger("BulletSet",bulletSet);
            if(ps!=null){//잔상회전
                ParticleSystem.MainModule main = ps.main;
                if(main.startRotation.mode == ParticleSystemCurveMode.Constant)
                    main.startRotation = -transform.eulerAngles.z*Mathf.Deg2Rad;
            }
        }
        transform.Translate(Vector2.right*speed*Time.deltaTime);
    }
    protected void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == 11||(misa)?true:other.CompareTag("Laser")||other.gameObject.layer == 9){
            if(other.CompareTag("MainCamera")) return;
            if(enemy)
                DespawnBullet();
            //else DestroyBullet();
        }
    }
    public void BulletSet(){
        switch(bulletSet){
            case 0:
            animator.Play("PulseAnimation");
            break;
            case 1:
            animator.Play("BoltAnimation");
            break;
        }
    }
    public void DespawnBullet(){
        DesEffect = GameManager.instance.allPoolManager.GetPool(5);
        DesEffect.transform.position = transform.position;
        DesEffect.SetActive(true);
        allPooler.Despawn();
    }
    public void DespawnHealPack(){
        DesEffect = GameManager.instance.allPoolManager.GetPool(9);
        DesEffect.transform.position = transform.position;
        DesEffect.SetActive(true);
        allPooler.Despawn();
    }
    public void DestroyBullet(){
        CancelInvoke();
        ObjectPoolling.ReturnObject(this);
    }


    [PunRPC]
    void DirRPC(Vector3 pos,Quaternion ang, int Set, int DAM,int STN){
        transform.position = pos;
        transform.rotation = ang;
        bulletSet = Set;
        bulletDagage = DAM;
        stun = STN;
        gameObject.SetActive(true);
        gameObject.layer = 15;
    }
    
}

using System.Collections;
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
    public int bulletDagage;
    public float stun=200f;
    [SerializeField]
    private bool enemy;
    [SerializeField]
    private Sprite[] sprite;
    public void Awake(){
        
        animator = GetComponent<Animator>();
    }
    public void StartDeley(){
        CancelInvoke();
        Invoke("DestroyBullet",3f);
        
    }

    private void Update()
    {
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
    public void DestroyBullet(){
        CancelInvoke();
        ObjectPoolling.ReturnObject(this);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed=10;
    GameManager gameManager;
    public ParticleSystem ps;
    void Start(){
        ps = GetComponentInChildren<ParticleSystem>();
        gameManager=GameManager.instance;
    }
    void Update()
    {
        if(ps!=null){//잔상회전
            ParticleSystem.MainModule main = ps.main;
            if(main.startRotation.mode == ParticleSystemCurveMode.Constant)
                main.startRotation = -transform.eulerAngles.z*Mathf.Deg2Rad;
        }
        //-----------------------------------------------------------------------------------------
        Destroy(gameObject,3);
        transform.Translate(Vector2.right*speed*Time.deltaTime);
    }
}

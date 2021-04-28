using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    private Rigidbody2D myrigidbody;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator gunAnimator;
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject barSsaPos;
    private float rotateDegree,headRotate,radian,x,y;
    private Vector3 oPosition,target;
    private bool pistolCoolTime=true;
    private int GunSet=0;
    void Start()
    {
        myrigidbody=GetComponent<Rigidbody2D>();
        spriteRenderer=GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        oPosition = transform.position;
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rotateDegree = Mathf.Atan2(target.y - oPosition.y, target.x - oPosition.x)*Mathf.Rad2Deg;
        headRotate = Mathf.Atan2(target.y - oPosition.y, Mathf.Abs(target.x - oPosition.x))*Mathf.Rad2Deg;
        //spriteRenderer.flipX=(rotateDegree<90&&rotateDegree>-90)?true:false;
        transform.rotation=new Quaternion(0f,(rotateDegree<90&&rotateDegree>-90)?0f:180f,0f,0f);
        transform.GetChild(0).transform.rotation = Quaternion.Euler (0f, (rotateDegree<90&&rotateDegree>-90)?0f:180f,headRotate/3f);
        transform.GetChild(1).transform.rotation = Quaternion.Euler (0f, (rotateDegree<90&&rotateDegree>-90)?0f:180f,headRotate);//HeadRotation
        //-----------------------------------------------------------------------------------------
        radian = rotateDegree*Mathf.PI/180f;
        x = 160 * Mathf.Cos(radian);
        y = 260 * Mathf.Sin(radian);
        if(y>0)y/=2;
        
        myrigidbody.velocity=new Vector2(Mathf.Clamp(myrigidbody.velocity.x,-20f,20f),Mathf.Clamp(myrigidbody.velocity.y,-20f,20f));
        if(Input.GetMouseButtonDown(0)) {
            if(!pistolCoolTime)return;
            pistolCoolTime=false;
            StartCoroutine(PistolCooltime());
            GameObject bullet;
            bullet = Instantiate(bulletPrefab);
            bullet.transform.position=barSsaPos.transform.position;
            bullet.transform.rotation = Quaternion.Euler(0,0,rotateDegree);
            gunAnimator.SetTrigger("Shot");
            myrigidbody.AddForce(new Vector3(-x,-y,0f));//Rebound
        }
        //-----------------------------------------------------------------------------------------
        //this.transform.position =  new Vector2(x, y);
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if(wheelInput>0){
            if(GunSet<2)
                GunSet++;
            gunAnimator.SetInteger("GunSet",GunSet);
        }
        if(wheelInput<0){
            if(GunSet>0)
                GunSet--;
            gunAnimator.SetInteger("GunSet",GunSet);
        }

    }
    private IEnumerator Opening(){
        yield return new WaitForSeconds(0.2f);
    }
    private IEnumerator PistolCooltime(){
        yield return new WaitForSeconds(0.15f);
        pistolCoolTime=true;
    }
}
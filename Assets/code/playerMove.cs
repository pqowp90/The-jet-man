using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMove : MonoBehaviour
{
    private Rigidbody2D myrigidbody;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator gunAnimator;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private playercamera playerCamera;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject shotGunBulletPrefab;
    [SerializeField]
    private GameObject barSsaPos;
    [SerializeField]
    private GameObject hiLaser;
    private float rotateDegree,headRotate,radian,x,y,coolTime,goTime;
    private Vector3 oPosition,target;
    private int GunSet=0;
    private bool No=true;
    private bool NoNo=true;
    GameObject bullet;

    void Start()
    {
        playerCamera=FindObjectOfType<playercamera>();
        hiLaser=GameObject.Find("HiLaser");
        hiLaser.SetActive(false);
        animator=gameObject.GetComponent<Animator>();
        animator.SetBool("NoNo",true);
        myrigidbody=GetComponent<Rigidbody2D>();
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(coolTime>0)
            coolTime-=Time.deltaTime;
        if(No){//StartAni
            myrigidbody.velocity = new Vector2(2.5f,myrigidbody.velocity.y);
            if(transform.position.x>5.65f){
                myrigidbody.AddForce(new Vector3(100f,170));
                No=false;
                animator.SetBool("NoNo",false);
            }
            return;
        }
        if(NoNo){
            if(transform.position.x>10f){
                NoNo=false;
                hiLaser.SetActive(true);
                hiLaser.GetComponent<Animator>().SetTrigger("hihi");
                StartCoroutine(Laser());
            }
        }
        else {
            goTime = hiLaser.transform.position.x+(0.8f*Time.deltaTime);
            hiLaser.transform.position = new Vector3(goTime,hiLaser.transform.position.y,0);
            playerCamera.maxPos = new Vector2(goTime,playerCamera.maxPos.y);
        }
        //-----------------------------------------------------------------------------------------
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
        x = 80 * Mathf.Cos(radian);
        y = 200 * Mathf.Sin(radian);
        if(y>0)y/=2;
        
        myrigidbody.velocity=new Vector2(Mathf.Clamp(myrigidbody.velocity.x,-20f,20f),Mathf.Clamp(myrigidbody.velocity.y,-20f,20f));
        if(Input.GetMouseButtonDown(0)) {
            if(!EventSystem.current.IsPointerOverGameObject()&&coolTime<=0f){
                gunAnimator.SetBool("Shoting",true);
                gunAnimator.SetTrigger("Shot");
                switch(GunSet){
                    case 0:
                    Shoting(1f);
                    coolTime = 0.15f;
                    break;
                    case 1:
                    coolTime = 0.1f;
                    break;
                    case 2:
                    Shoting(0.4f);
                    coolTime = 0.7f;
                    break;
                }
                
            }
        }
        if(Input.GetMouseButtonUp(0)){
            gunAnimator.SetBool("Shoting",false);
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
        if(wheelInput!=0){
            switch(GunSet){
                case 0:
                barSsaPos = GameObject.Find("PistolShotingPos");
                break;
                case 1:
                barSsaPos = GameObject.Find("RifleShotingPos");
                break;
                case 2:
                barSsaPos = GameObject.Find("ShotGunShotingPos");
                break;
            }
        }
        
        
    }
    private void enabledAE(){
        animator.enabled=false;
    }
    public void Shoting(float a){
        myrigidbody.AddForce(new Vector3(-x/a,-y/a,0f));
        if(GunSet==2){
            for(int i=0;i<2;i++){
                bullet = Instantiate(shotGunBulletPrefab);
                bullet.transform.position=barSsaPos.transform.position;
                bullet.transform.rotation = Quaternion.Euler(0,0,rotateDegree+Random.Range(7,20f));
            }
            for(int i=0;i<2;i++){
                bullet = Instantiate(shotGunBulletPrefab);
                bullet.transform.position=barSsaPos.transform.position;
                bullet.transform.rotation = Quaternion.Euler(0,0,rotateDegree+Random.Range(-7,7));
            }
            for(int i=0;i<2;i++){
                bullet = Instantiate(shotGunBulletPrefab);
                bullet.transform.position=barSsaPos.transform.position;
                bullet.transform.rotation = Quaternion.Euler(0,0,rotateDegree+Random.Range(-20,-7));
            }
            return;
        }
        bullet = Instantiate(bulletPrefab);
        bullet.transform.position=barSsaPos.transform.position;
        bullet.transform.rotation = Quaternion.Euler(0,0,rotateDegree);
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Laser"){
            Time.timeScale=0f;
            Debug.Log("die!!");
        }
    }
    private IEnumerator Laser()
    {
        yield return new WaitForSeconds(2.2f);
        hiLaser.GetComponent<Animator>().enabled=false;
    }
}
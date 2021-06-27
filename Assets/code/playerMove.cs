using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class playerMove : MonoBehaviour
{
    private Rigidbody2D myrigidbody;
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer[] childSpriteRenderers;
    [SerializeField]
    private Animator gunAnimator;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private playercamera playerCamera;
    [SerializeField]
    private GameObject barSsaPos;
    [SerializeField]
    private AudioClip[] audioClip;
    private AudioSource audioSource;
    private float rotateDegree,headRotate,radian,x,y,goTime,wheelInput;
    [SerializeField]
    private float coolTime;
    public int maxHp=60;
    public int hp=0;
    private Vector3 oPosition,target;
    private int GunSet=0;
    private bool No=true;
    float cameraWidth;
    [SerializeField]
    private int[] gunset=new int[2];
    [SerializeField]
    private int[] gunDamage = new int[10];

    [SerializeField]
    private int[] gunStun = new int[10];
    private HpBar hpBar;
    [SerializeField]
    private Light2D light2D;
    [SerializeField]
    private Collider2D myHitBox;
    private bool isdead=false;
    private bool damaged=false;
    [SerializeField]
    private GameObject ending;
    private AudioSource backgroundMusic;
    void Start()
    {
        if(FindObjectOfType<BackgroundMusic>()!=null)
            backgroundMusic = FindObjectOfType<BackgroundMusic>().GetComponent<AudioSource>();
        GameManager.instance.isdead = false;
        audioSource = GetComponent<AudioSource>();
        light2D = GetComponentInChildren<Light2D>();
        hpBar = transform.GetComponentInChildren<HpBar>();
        hp = maxHp;
        GameManager gameManager = GameManager.instance;
        cameraWidth=Camera.main.orthographicSize*Camera.main.aspect;
        playerCamera=FindObjectOfType<playercamera>();
        animator=gameObject.GetComponent<Animator>();
        animator.SetBool("NoNo",true);
        myrigidbody=GetComponent<Rigidbody2D>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        LoadGunSet();
        GunSet=gunset[0];
        gunAnimator.SetInteger("GunSet",gunset[0]);
        shotPosSet();
    }
    public void LoadGunSet(){
        gunset[0]=PlayerPrefs.GetInt("Select1",-1);
        gunset[1]=PlayerPrefs.GetInt("Select2",-1);
        gunDamage[gunset[0]]+=PlayerPrefs.GetInt("S1UP")*3;
        if(gunset[1]!=-1)
            gunDamage[gunset[1]]+=PlayerPrefs.GetInt("S2UP")*3;
    }
    void Update()
    {
        if(isdead)return;
        if(coolTime>0)
            coolTime-=Time.deltaTime;
        if(No){//StartAni
            myrigidbody.velocity = new Vector2(2.5f,myrigidbody.velocity.y);
            if(transform.position.x>5.65f){
                myrigidbody.AddForce(new Vector3(100f,200f));
                No=false;
                animator.SetBool("NoNo",false);
            }
            return;
        }
        HeadRotation();
        radian = rotateDegree*Mathf.PI/180f;
        x = 80 * Mathf.Cos(radian);
        y = 200 * Mathf.Sin(radian);
        if(y>0)y*=0.5f;
        
        
        myrigidbody.velocity=new Vector2(Mathf.Clamp(myrigidbody.velocity.x,-20f,20f),Mathf.Clamp(myrigidbody.velocity.y,-20f,20f));

        if(Input.GetMouseButton(0)&&coolTime<=0f&&!EventSystem.current.IsPointerOverGameObject())
            gunAnimator.SetBool("Shoting",true);

        if(Input.GetMouseButtonDown(0)&&!EventSystem.current.IsPointerOverGameObject()) {
            if(!EventSystem.current.IsPointerOverGameObject()&&coolTime<=0f){
                gunAnimator.SetBool("Shoting",true);
                if(GunSet!=1)
                    gunAnimator.SetTrigger("Shot");
                
                switch(GunSet){
                    case 0:
                    Shoting(1f);
                    coolTime = 0.15f;
                    break;
                    case 1:
                    coolTime = 0.2f;
                    break;
                    case 2:
                    Shoting(0.5f);
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
        if(!gunAnimator.GetBool("Shoting")){
            wheelInput = Input.GetAxis("Mouse ScrollWheel");
            if(wheelInput>0){
                GunSet=gunset[0];
                gunAnimator.SetInteger("GunSet",gunset[0]);
                coolTime = 0.1f;
            }
            else if(wheelInput<0&&gunset[1]!=-1){
                GunSet=gunset[1];
                gunAnimator.SetInteger("GunSet",gunset[1]);
                coolTime = 0.1f;
            }
            if(wheelInput!=0){
                shotPosSet();
            }
        }
        
        
    }
    public void GUNSETTING(int hi){
        if(gunset[hi]>=0){
            GunSet=gunset[hi];
            gunAnimator.SetInteger("GunSet",gunset[hi]);
            shotPosSet();
        }
    }
    private void HeadRotation(){
        oPosition = transform.position;
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rotateDegree = Mathf.Atan2(target.y - oPosition.y, target.x - oPosition.x)*Mathf.Rad2Deg;
        headRotate = Mathf.Atan2(target.y - oPosition.y, Mathf.Abs(target.x - oPosition.x))*Mathf.Rad2Deg;
        //spriteRenderer.flipX=(rotateDegree<90&&rotateDegree>-90)?true:false;
        transform.rotation=new Quaternion(0f,(rotateDegree<90&&rotateDegree>-90)?0f:180f,0f,0f);
        transform.GetChild(0).transform.rotation = Quaternion.Euler (0f, (rotateDegree<90&&rotateDegree>-90)?0f:180f,headRotate/3f);
        transform.GetChild(1).transform.rotation = Quaternion.Euler (0f, (rotateDegree<90&&rotateDegree>-90)?0f:180f,headRotate);//HeadRotation
    }
    private void shotPosSet(){
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
    private void enabledAE(){
        animator.enabled=false;
    }
    public void Shoting(float a){
        myrigidbody.AddForce(new Vector3(-x/a,-y/a,0f));
        if(GunSet==2){
            for(int i=0;i<3;i++){
                for(int j=0;j<2;j++){
                    var sBullet = ObjectPoolling.GetObject();
                    sBullet.transform.position=barSsaPos.transform.position;
                    sBullet.transform.rotation = Quaternion.Euler(0,0,rotateDegree+Random.Range(-20+(13*i),-20+(13*(i+1))));
                    sBullet.bulletSet = 1;
                    sBullet.bulletDagage = gunDamage[GunSet];
                    sBullet.stun = gunStun[GunSet];
                    sBullet.gameObject.SetActive(true);
                }
            }
            return;
        }
        var bullet = ObjectPoolling.GetObject();
        bullet.transform.position=barSsaPos.transform.position;
        bullet.transform.rotation = Quaternion.Euler(0,0,rotateDegree);
        bullet.bulletSet = 0;
        bullet.bulletDagage = gunDamage[GunSet];
        bullet.stun = gunStun[GunSet];
        bullet.gameObject.SetActive(true);

    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Laser"){
            
            hp = 0;
            StartCoroutine(DieManNONO());
            return; 
        }
        if(other.gameObject.layer==14){
            BulletMove bulletMove = other.transform.parent.GetComponent<BulletMove>();
            if(damaged){
                bulletMove.DespawnBullet();
                return;
            }
            hp -= bulletMove.bulletDagage;
            bulletMove.DespawnBullet();
            hpBar.sethealth(hp,maxHp);
            if(hp <= 0){
                StartCoroutine(DieManNONO());
                return; 
            }
            StartCoroutine(Hit());
        }
    }
    private IEnumerator DieManNONO(){
        if(backgroundMusic!=null)
            backgroundMusic.Stop();
        gunAnimator.SetBool("Shoting",false);
        childSpriteRenderers[1].transform.parent.transform.DOLocalMove(new Vector3(Random.Range(-0.2f,0.2f),-1.1f,0f),1.2f);
        childSpriteRenderers[1].transform.parent.transform.DOLocalRotate(new Vector3(Random.Range(-50f,50f),0f,0f),1.2f);
        //animator.enabled=true;
        //animator.Play("playerdie");
        myrigidbody.velocity=Vector2.zero;
        myHitBox.enabled = false;
        var bullet = GameManager.instance.allPoolManager.GetPool(0);
        ParticleSystem.MainModule parmain = bullet.GetComponent<ParticleSystem>().main;
        parmain.startColor=new Color(0.6603774f,0.2143111f,0.2143111f,1f);
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
        isdead=true;
        GameManager.instance.isdead = true;
        //childSpriteRenderers[1].transform.SetParent(null);
        audioSource.clip = audioClip[1];
        audioSource.time = 0;
        audioSource.Play();
        Time.timeScale = 0.1f;
        playerCamera.zoom = 2f;
        playerCamera.hihi = null;
        playerCamera.bound=false;
        GameManager.instance.SaveAddMoney();
        yield return new WaitForSeconds(0.2f);
        ending.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        
        playerCamera.zoom = 0f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    private IEnumerator Hit(){
        audioSource.clip = audioClip[0];
        audioSource.time = 0;
        audioSource.Play();
        damaged = true;
        //myHitBox.enabled = false;
        playerCamera.startshake(0.2f,0.3f);
        spriteRenderer.color = new Color(1f,1f,1f,1f);
        DOTween.To(()=>light2D.color,colorL=>light2D.color=colorL,new Color(1f,0.4849057f,0.4849057f,1f),0.1f);
        //light2D.color = new Color(1f,0.4849057f,0.4849057f,1f);
        yield return new WaitForSeconds(0.1f);
        DOTween.To(()=>light2D.color,colorL=>light2D.color=colorL,new Color(1f,1f,1f,1f),0.2f);
        //light2D.color = new Color(1f,1f,1f,1f);
        for(int i=0;i<10;i++){
            SetColor(new Color(0.5f,0.5f,0.5f,1f));
            yield return new WaitForSeconds(0.05f);
            SetColor(new Color(1f,1f,1f,1f));
            yield return new WaitForSeconds(0.05f);
        }
        damaged = false;
        //myHitBox.enabled = true;
    }
    private void SetColor(Color color){
        for(int i=0;i<childSpriteRenderers.Length;i++){
            spriteRenderer.color = color;
            childSpriteRenderers[i].color = color;
        }
    }
    

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

public class playerMove : MonoBehaviourPunCallbacks, IPunObservable
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
    private GameObject gunFire;
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
    [SerializeField]
    private float[] lifetime = new float[10];
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
    private bool isMulty;
    public multyName multyName;
    public PhotonView PV;
    private Vector3 curPos;
    private Vector3 curTarget;
    private NetWorkManager netWorkManager;

    void Start()
    {
        netWorkManager = FindObjectOfType<NetWorkManager>();
        PV = GetComponent<PhotonView>();
        Time.timeScale = 1f;
        animator=gameObject.GetComponent<Animator>();
        if(SceneManager.GetActiveScene().buildIndex==4){
            MultySpawn();
            isMulty = true;
            animator.enabled = false;
            //multyName = FindObjectOfType<multyName>();
            var nicknameHiHi = multyName.GetComponentInChildren<nickname>().GetComponent<Text>();
            nicknameHiHi.text = PV.IsMine? PhotonNetwork.NickName:PV.Owner.NickName ;
            nicknameHiHi.color = PV.IsMine? Color.green:Color.red ;
            if(PV.IsMine)
                FindObjectOfType<CinemachineVirtualCamera>().Follow = transform;
        }
        //Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if(FindObjectOfType<BackgroundMusic>()!=null)
            backgroundMusic = FindObjectOfType<BackgroundMusic>().GetComponent<AudioSource>();
        GameManager.instance.isdead = false;
        audioSource = GetComponent<AudioSource>();
        hpBar = transform.GetComponentInChildren<HpBar>();
        hp = maxHp;
        GameManager gameManager = GameManager.instance;
        cameraWidth=Camera.main.orthographicSize*Camera.main.aspect;
        playerCamera=FindObjectOfType<playercamera>();
        
        if(!isMulty)
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
        if(isMulty){
            
            HeadRotation((PV.IsMine)?Camera.main.ScreenToWorldPoint(Input.mousePosition):curTarget);
            multyName.hp = (float)hp/(float)maxHp;
            multyName.transform.position = transform.position;
            if(!PV.IsMine){
                gunAnimator.SetInteger("GunSet",GunSet);
                if((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
                else transform.position = Vector3.Lerp(transform.position,curPos,Time.deltaTime * 10);

                return;
            }
        }
        if(coolTime>0)
            coolTime-=Time.deltaTime;
        if(!isMulty){
            if(No){//StartAni
                myrigidbody.velocity = new Vector2(2.5f,myrigidbody.velocity.y);
                if(transform.position.x>5.65f){
                    myrigidbody.AddForce(new Vector3(100f,200f));
                    No=false;
                    animator.SetBool("NoNo",false);
                }
                return;
            }
        }
        
        //HeadRotation();z
        radian = rotateDegree*Mathf.PI/180f;
        x = 80 * Mathf.Cos(radian);
        y = 200 * Mathf.Sin(radian);
        if(y>0)y*=0.5f;
        if(!(Time.timeScale==0f))
            HeadRotation(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        
        myrigidbody.velocity=new Vector2(Mathf.Clamp(myrigidbody.velocity.x,-20f,20f),Mathf.Clamp(myrigidbody.velocity.y,-20f,20f));
        
        if(Input.GetMouseButton(0)&&coolTime<=0f&&!EventSystem.current.IsPointerOverGameObject())
            if(gunAnimator.GetBool("Shoting")==false){
                gunAnimator.SetBool("Shoting",true);
                gunAnimator.SetTrigger("Shot");
            }

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
                    case 3:
                    
                    coolTime = 0.15f;
                    break;
                    case 4:
                    Shoting(0.5f);
                    coolTime = 0.2f;
                    break;
                    case 5:
                    
                    coolTime = 0.2f;
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
                gunAnimator.ResetTrigger("Shot");
                coolTime = 0.1f;
            }
            else if(wheelInput<0&&gunset[1]!=-1){
                GunSet=gunset[1];
                gunAnimator.SetInteger("GunSet",gunset[1]);
                gunAnimator.ResetTrigger("Shot");
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
            gunAnimator.ResetTrigger("Shot");
            shotPosSet();
        }
    }
    private void HeadRotation(Vector3 target){
        oPosition = transform.position;
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
            case 3:
            barSsaPos = GameObject.Find("ScarShotingPos");
            break;
            case 4:
            barSsaPos = GameObject.Find("ShotGun2ShotingPos");
            break;
            case 5:
            barSsaPos = GameObject.Find("VectorShotingPos");
            break;            
        }
    }
    private void enabledAE(){
        animator.enabled=false;
    }
    public void Shoting(float a){
        gunFire.transform.position = barSsaPos.transform.position;
        gunFire.transform.rotation = barSsaPos.transform.rotation;
        gunFire.GetComponent<Animator>().SetTrigger("Shot");
        gunFire.GetComponent<Animator>().SetFloat("Blend",(float)GunSet);
        myrigidbody.AddForce(new Vector3(-x/a,-y/a,0f));
        if(GunSet==2||GunSet==4){
            for(int i=0;i<3;i++){
                for(int j=0;j<2;j++){
                    var sBullet = ObjectPoolling.GetObject();
                    if(isMulty){
                        sBullet.GetComponent<PhotonView>().RPC("DirRPC",
                            RpcTarget.All,barSsaPos.transform.position,
                            Quaternion.Euler(0,0,rotateDegree+Random.Range(-20+(13*i),-20+(13*(i+1)))),
                            1,
                            gunDamage[GunSet],
                            gunStun[GunSet]);
                            Itismine(sBullet.gameObject);
                    }
                    else {

                        sBullet.transform.position=barSsaPos.transform.position;
                        sBullet.transform.rotation = Quaternion.Euler(0,0,rotateDegree+Random.Range(-20+(13*i),-20+(13*(i+1))));
                        sBullet.bulletSet = 1;
                        sBullet.bulletDagage = gunDamage[GunSet];
                        sBullet.stun = gunStun[GunSet];
                        sBullet.gameObject.SetActive(true);
                    }
                    
                }
            }
            return;
        }
        var bullet = ObjectPoolling.GetObject();
        if(isMulty){
            bullet.GetComponent<PhotonView>().RPC("DirRPC",
            RpcTarget.All,barSsaPos.transform.position,
            Quaternion.Euler(0,0,rotateDegree),
            0,
            gunDamage[GunSet],
            gunStun[GunSet]);
            Itismine(bullet.gameObject);
        }
        else{
            bullet.transform.position=barSsaPos.transform.position;
            bullet.transform.rotation = Quaternion.Euler(0,0,rotateDegree);
            bullet.bulletSet = 0;
            bullet.bulletDagage = gunDamage[GunSet];
            bullet.stun = gunStun[GunSet];
            bullet.gameObject.SetActive(true);
            ParticleSystem m_System = bullet.GetComponentInChildren<ParticleSystem>();
            ParticleSystem.MainModule main = m_System.main;
            main.startLifetimeMultiplier = lifetime[GunSet];
        }
        
    }
    [PunRPC]
    void DIERPC(int score){
        Debug.Log("hihihihihi");
        //if(PV.IsMine)return;
        netWorkManager.WinLose(!PV.IsMine);
    }
    void Itismine(GameObject bullet){
        if(PV.IsMine){
            bullet.layer = 9;
        }
        gunAnimator.SetTrigger("Shot");
    }
    void OnTriggerEnter2D(Collider2D other){
        if(isdead)return;
        if(isMulty&&!PV.IsMine) return;
        if(other.tag=="Laser"){
            
            hp = 0;
            StartCoroutine(DieManNONO());
            return; 
        }
        if(other.gameObject.layer==14||other.gameObject.layer==15){
            BulletMove bulletMove;
            if(other.gameObject.layer==14){
                bulletMove = other.GetComponentInParent<BulletMove>();
                if(bulletMove==null){
                    bulletMove = other.GetComponent<BulletMove>();
                }
            }else {
                bulletMove = other.GetComponent<BulletMove>();
            }
            
            bool isMultyBullet=other.gameObject.layer==15;
            hp -= bulletMove.bulletDagage;
            hp=Mathf.Clamp(hp,0,maxHp);
            if(damaged){
                if(isMultyBullet){
                    bulletMove.DestroyBullet();
                }else
                    bulletMove.DespawnBullet(); 
                return;
            }
            
            
            if(isMultyBullet){
                    bulletMove.DestroyBullet();
            }else{
                if(bulletMove.bulletDagage>0)
                    bulletMove.DespawnBullet();
                else
                    bulletMove.DespawnHealPack();
            }
            if(!isMulty)
                hpBar.sethealth(hp,maxHp);
            if(bulletMove.bulletDagage<0)return;
            if(hp <= 0){        
                StartCoroutine(DieManNONO());
                return; 
            }
            StartCoroutine(Hit());
        }
    }
    private IEnumerator DieManNONO(){
        if(isMulty){
            PV.RPC("DIERPC",RpcTarget.All,0);
            Debug.Log("you lose");
            isdead=true;
            Time.timeScale = 0.1f;
            yield return new WaitForSeconds(0.3f);
            Time.timeScale = 1f;

            
            MultySpawn();
            isdead=false;
        }
        else{
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
    }
    private IEnumerator Hit(){
        if(!isMulty){
            audioSource.clip = audioClip[0];
            audioSource.time = 0;
            audioSource.Play();
            damaged = true;
            //myHitBox.enabled = false;
            playerCamera.startshake(0.2f,0.3f);
            spriteRenderer.color = new Color(1f,1f,1f,1f);
            float outer = light2D.pointLightOuterRadius;
            light2D.pointLightOuterRadius = 9f;
            DOTween.To(()=>light2D.color,colorL=>light2D.color=colorL,new Color(1f,0.4849057f,0.4849057f,1f),0.1f);
            //light2D.color = new Color(1f,0.4849057f,0.4849057f,1f);
            yield return new WaitForSeconds(0.1f);
            light2D.pointLightOuterRadius = outer;
            DOTween.To(()=>light2D.color,colorL=>light2D.color=colorL,new Color(0.9716981f,0.8263388f,0.4904325f,1f),0.2f);
            //light2D.color = new Color(1f,1f,1f,1f);
            for(int i=0;i<10;i++){
                SetColor(new Color(0.5f,0.5f,0.5f,1f));
                yield return new WaitForSeconds(0.05f);
                SetColor(new Color(1f,1f,1f,1f));
                yield return new WaitForSeconds(0.05f);
            }
            damaged = false;
            //myHitBox.enabled = true;
        }else{
            damaged = false;
        }
    }
    private void SetColor(Color color){
        for(int i=0;i<childSpriteRenderers.Length;i++){
            spriteRenderer.color = color;
            childSpriteRenderers[i].color = color;
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
        if(stream.IsWriting){
            stream.SendNext(Camera.main.ScreenToWorldPoint(Input.mousePosition)) ;
            stream.SendNext(transform.position);
            stream.SendNext(hp);
            stream.SendNext(GunSet);
        }
        else{
            curTarget = (Vector3)stream.ReceiveNext();
            curPos = (Vector3)stream.ReceiveNext();
            hp = (int)stream.ReceiveNext();
            GunSet = (int)stream.ReceiveNext();
            
        }
    }
    void MultySpawn(){
        hp = maxHp;
        transform.position = new Vector3(Random.Range(2f,-2f),0f,0f);
    }

}
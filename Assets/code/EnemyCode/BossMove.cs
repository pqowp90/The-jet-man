using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossMove : MonoBehaviour
{
    [SerializeField]
    private Transform eye,myEyero,bar,mybar,eyeXPos,barssaPos1,barssaPos2,barssaPos3;
    private Vector3 diff;
    private float rotationZ;
    [SerializeField]
    private Animator bossAnimator;
    private int hihihi;
    private AllPoolManager allPoolManager;
    [SerializeField]
    private int gunStun,gunDamage;
    [SerializeField]
    private int hp,maxHp;
    private GameObject bullet;
    [SerializeField]
    private HpBar hpBar;
    void Start()
    {
        hp=maxHp;
        MoveStart();
        StartCoroutine(GoGo());
        allPoolManager = GameManager.instance.allPoolManager;
    }
    private IEnumerator Shoting(){
        yield return new WaitForSeconds(0.5f);
        var bullet = allPoolManager.GetPool(4).GetComponent<BulletMove>();
        bullet.transform.position = barssaPos1.position;
        bullet.transform.rotation = barssaPos1.rotation;
        bullet.transform.Rotate(new Vector3(0f,0f,90f));
        bullet.bulletSet = 0;
        bullet.bulletDagage = 5;
        bullet.stun = 5;
        bullet.gameObject.SetActive(true);
        bullet.GetComponent<Animator>().SetBool("Bolt",false);

        yield return new WaitForSeconds(0.14f);

        for(int i=0;i<2;i++){
            bullet = allPoolManager.GetPool(4).GetComponent<BulletMove>();
            bullet.transform.position = ((i==0)?barssaPos2:barssaPos3).position;
            bullet.transform.rotation = ((i==0)?barssaPos2:barssaPos3).rotation;
            bullet.transform.Rotate(new Vector3(0f,0f,90f));
            //bullet.GetComponent<UdotanBullet>().myRotationZ = ((i==0)?barssaPos2:barssaPos3).eulerAngles.z+90f;
            //bullet.GetComponent<UdotanBullet>().Reset(); 
            bullet.bulletSet = 0;
            bullet.bulletDagage = 5;
            bullet.stun = 5;
            bullet.gameObject.SetActive(true);
            bullet.GetComponent<Animator>().SetBool("Bolt",false);
            
            //bullet.transform.Rotate(new Vector3(0f,0f,90f));
        }
    }
    void MoveStart(){
        transform.DOMove(new Vector3(Random.Range(GameManager.instance.GoMin.position.x+0.5f,GameManager.instance.GoMax.position.x-0.5f)
        ,Random.Range(GameManager.instance.GoMin.position.y+1.5f,GameManager.instance.GoMax.position.y+1.5f),0f),1.5f).SetEase(Ease.Linear);
    }
    void LookAt(){
        transform.DORotate(new Vector3(0f,0f,rotationZ-90f),(hihihi==0)?1.5f:3f);
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Bullet")&&gameObject.activeSelf == true){
            bullet = GameManager.instance.allPoolManager.GetPool(0);
            bullet.transform.position = (other.transform.position+transform.position)/2;
            bullet.SetActive(true);
            BulletMove bulletMove = other.GetComponent<BulletMove>();
            StartCoroutine(Damaged());
            if(other.gameObject.activeSelf == true){
                hp -= bulletMove.bulletDagage;
                if(hp<=0){
                    Destroy(gameObject);
                }
                hpBar.sethealth(hp,maxHp);
                bulletMove.DestroyBullet();
            }
        }
        
    }
    private IEnumerator Damaged(){
        SetColor(new Color(0.5f,0.5f,0.5f,1f));
        yield return new WaitForSeconds(0.1f);
        SetColor(new Color(1f,1f,1f,1f));
    }
    private void SetColor(Color color){
        for(int i=0;i<transform.childCount;i++){
            if(transform.GetChild(i).GetComponent<SpriteRenderer>()!=null)
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
        }
    }
    private IEnumerator GoGo(){
        bool hellohi=true;
        while(true){
            MoveStart();
            yield return new WaitForSeconds(1.5f);
            hihihi=Random.Range(0,3);
            bossAnimator.SetFloat("Blend",(float)hihihi);
            bossAnimator.SetTrigger("attack");
            LookAt();  
            hellohi=(hellohi)?false:true;
            if(hellohi)
                switch(hihihi){
                    case 0:
                    StartCoroutine(Shoting());
                    break;
                    case 1:
                    yield return new WaitForSeconds(1f);
                    transform.DOKill();
                    yield return new WaitForSeconds(0.5f);
                    break;
                    case 2:
                    yield return new WaitForSeconds(2.2f);
                    MisailGo();
                    break;
                }
            
            yield return new WaitForSeconds(1.5f); 
        }
    }
    void MisailGo(){
        for(int i=0;i<2;i++){
            var misa = allPoolManager.GetPool(7);
            misa.transform.parent=transform.GetChild(0);
            misa.transform.localRotation = Quaternion.Euler(0f,0f,90f);
            misa.transform.localPosition = new Vector3(0.654f*((i==0)?1f:-1f),0f);
            misa.SetActive(true);
        }
    }
    void Update()
    {
        bar.position=new Vector3(bar.position.x,mybar.position.y,0f);
        diff = GameManager.instance.player.transform.position - transform.position;
        diff.Normalize();
        rotationZ = Mathf.Atan2(diff.y,diff.x)*Mathf.Rad2Deg;

        myEyero.rotation = Quaternion.Euler(myEyero.rotation.x,myEyero.rotation.y,rotationZ);
        eye.rotation = Quaternion.Euler(myEyero.rotation.x,myEyero.rotation.y,rotationZ*0.6f+36f);
        eyeXPos.position=myEyero.GetChild(0).position;
        eye.localPosition=new Vector2(eyeXPos.localPosition.x,eye.localPosition.y);
        transform.position+=new Vector3(0f,0.6f,0f)*Time.deltaTime;
    }
}

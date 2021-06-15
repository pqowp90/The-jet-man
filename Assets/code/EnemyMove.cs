using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    private float radian,x,y,rotateDegree;
    GameManager gameManager;
    private Rigidbody2D myrigidbody;
    private SpriteRenderer spriteRenderer;
    private HpBar hpBar;
    public int maxHp=60;
    public int hp=0;
    private GameObject bullet;
    private AllPooler allPooler;
    
    void Awake()
    {
        allPooler = GetComponent<AllPooler>();
        hpBar = GetComponentInChildren<HpBar>();
        hp = maxHp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        myrigidbody = GetComponent<Rigidbody2D>();
        gameManager = GameManager.instance;
        MoveStart();
    }
    private void OnEnable(){
        spriteRenderer.color = new Color(1f,1f,1f,1f);
        SetColor(new Color(1f,1f,1f,1f));
        hp = maxHp;
    }
    private void EnemyReset(){
        hp = maxHp;
        MoveStart();
    }
    void Update()
    {
        myrigidbody.AddForce(new Vector2(0f,gameManager.speedenemy*Time.deltaTime));
        if(gameManager.GoMin.position.y-1>transform.position.y)
            MoveStart();
    }
    private void MoveStart(){
        transform.DOMove(new Vector3(Random.Range(gameManager.GoMin.position.x,gameManager.GoMax.position.x)
        ,Random.Range(gameManager.GoMin.position.y+2f,gameManager.GoMax.position.y+2f),0f),2f);
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Bullet")&&gameObject.activeSelf == true){
            bullet = gameManager.allPoolManager.GetPool(0);
            bullet.transform.position = (other.transform.position+transform.position)/2;
            bullet.SetActive(true);
            BulletMove bulletMove = other.GetComponent<BulletMove>();
            StartCoroutine(Damaged());
            rotateDegree=other.gameObject.transform.eulerAngles.z;
            radian = rotateDegree*Mathf.PI/180f;
            x = bulletMove.stun*Mathf.Cos(radian);
            y = bulletMove.stun*Mathf.Sin(radian);
            myrigidbody.AddForce(new Vector3(x,y,0f));
            if(other.gameObject.activeSelf == true){
                hp -= bulletMove.bulletDagage;
                if(hp<=0){
                    bullet = gameManager.allPoolManager.GetPool(2);
                    bullet.transform.position = transform.position;
                    bullet.SetActive(true);
                    bullet = gameManager.allPoolManager.GetPool(0);
                    bullet.transform.position = (other.transform.position+transform.position)/2;
                    bullet.SetActive(true);
                    allPooler.Dispown();
                    
                }
                hpBar.sethealth(hp,maxHp);
                bulletMove.DestroyBullet();
            }
        }
        if(other.gameObject.layer==12){
            transform.DOKill();
        }
    }
    private IEnumerator Damaged(){
        transform.DOKill();
        spriteRenderer.color = new Color(0.5f,0.5f,0.5f,1f);
        SetColor(new Color(0.5f,0.5f,0.5f,1f));
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(1f,1f,1f,1f);
        SetColor(new Color(1f,1f,1f,1f));
    }
    private void SetColor(Color color){
        for(int i=0;i<transform.childCount;i++){
            if(transform.GetChild(i).GetComponent<SpriteRenderer>()!=null)
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
        }
    }
    private void Die(){
        transform.DOKill();

    }
}

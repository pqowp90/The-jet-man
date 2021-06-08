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
    public float stun=200f;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myrigidbody = GetComponent<Rigidbody2D>();
        gameManager = GameManager.instance;
        MoveStart();
    }
    void Update()
    {
        
    }
    private void MoveStart(){
        transform.DOMove(new Vector3(Random.Range(gameManager.spawnMinPos.x,gameManager.spawnMaxPos.x)
        ,Random.Range(gameManager.spawnMinPos.y,gameManager.spawnMaxPos.y),0f),2f);
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Bullet")){
            StartCoroutine(Damaged());
            rotateDegree=other.gameObject.transform.eulerAngles.z;
            radian = rotateDegree*Mathf.PI/180f;
            Debug.Log(rotateDegree);
            x = stun*Mathf.Cos(radian);
            y = stun*Mathf.Sin(radian);
            myrigidbody.AddForce(new Vector3(x,y,0f));
            if(other.gameObject.activeSelf == true)
                other.gameObject.GetComponent<BulletMove>().DestroyBullet();
        }
    }
    private IEnumerator Damaged(){
        spriteRenderer.color = new Color(1f,0.5f,0.5f,1f);
        for(int i=0;i<transform.childCount;i++){
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f,0.4f,0.4f,1f);
        }
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(1f,1f,1f,1f);
        for(int i=0;i<transform.childCount;i++){
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        }
    }
}

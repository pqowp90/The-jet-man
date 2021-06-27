using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject focusPoint;
    [SerializeField]
    private GameObject startWall;
    [SerializeField]
    public Camera playerCamera;
    private GameObject black,mouse;
    private GameObject ESC;
    [SerializeField]
    private GameObject dronePrefab;
    [SerializeField]
    private Animator startLaser2;
    private float timeSpeed=1f;
    public float recoilResistance{get;private set;}
    public GameObject player{get; private set;}
    private Light2D playerLight;
    private bool ESCON=false;
    [SerializeField]
    private bool menu=false;
    public bool showMouse=false;
    public float gameTime;
    private int BEST;
    private static GameManager _instance;
    public static GameManager instance
    {
        get{
            if(_instance == null){
                _instance = FindObjectOfType<GameManager>();
                if(_instance == null){
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    public Vector2 maxPos{get;private set;}
    public Vector2 minPos{get;private set;}
    [SerializeField]
    private Transform spawnGoMin;
    private GameObject drone;
    [SerializeField]
    private Transform spawnGoMax;
    public Transform GoMin;
    public Transform GoMax;
    public float speedenemy;
    private bool goUp=false;
    public AllPoolManager allPoolManager{get; private set;}
    [SerializeField]
    private float spawnDeley;
    public bool isdead=false;
    public int chkEnemy=0;
    public int nowMoney;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private Slider progressSlider;
    [SerializeField]
    private Slider progressSliderBEST;
    private int moneySum;
    [SerializeField]
    private GameObject WWWWW;
    [SerializeField]
    private int clearTime;
    public bool isEnding;
    [SerializeField]
    private GameObject sky,white;
    [SerializeField]
    private Text endingText;
    
    void Awake()
    {
        
        moneySum = PlayerPrefs.GetInt("MONEY",1000);
        if(!menu){
            player = FindObjectOfType<playerMove>().gameObject;
            playerLight = player.GetComponentInChildren<Light2D>();
        }
        allPoolManager = FindObjectOfType<AllPoolManager>();
        recoilResistance=0.8f;
        focusPoint = GameObject.Find("FocusPoint");
        playerCamera = FindObjectOfType<Camera>();
        black = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        ESC = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        if(!menu){
            mouse = FindObjectOfType<MousePointer>().gameObject;
            StartCoroutine(Opening());
        }
        maxPos=new Vector2(9f,9f);
        minPos=new Vector2(-9f,-9f);
        
        
    }
    public void AddMoney(int money){
        nowMoney+=money;
        moneyText.text = string.Format("{0}$",nowMoney);
    }
    public void SaveAddMoney(){
        PlayerPrefs.SetInt("MONEY",moneySum+nowMoney);
        PlayerPrefs.SetInt("BEST",BEST);
    }
    void Update()
    {
        

        if(goUp&&!menu){
            playerCamera.GetComponent<playercamera>().maxPos.y += Time.deltaTime;
        }
        
        if(!menu&&!showMouse){
            //Cursor.visible = false;
        }
        if(Input.GetKeyDown(KeyCode.Escape)&&!menu){
            Continue();
        }
        if(!menu){
            gameTime+=Time.deltaTime;
            progressSlider.value = gameTime/clearTime;
            if(!isEnding)
                playerLight.pointLightOuterRadius = 5.53f + (gameTime/clearTime)*9f;
            if(gameTime>BEST)
                BEST = (int)gameTime;
            progressSliderBEST.value = (float)BEST/(float)clearTime;
            if(gameTime>clearTime&&!isEnding){
                
                
                StartCoroutine(Ending());
            }
        }
        
    }
    private IEnumerator Ending(){
        DOTween.To(()=>playerLight.intensity,inten=>playerLight.intensity=inten,13f,2f);
        yield return new WaitForSeconds(2f);
        
        sky.SetActive(true);
        isEnding=true;
        yield return new WaitForSeconds(0.3f);
        // player.GetComponent<SpriteRenderer>().sortingLayerName = "1";
        // playerMove playerMove = player.GetComponent<playerMove>();
        // for(int i=0;i<playerMove.childSpriteRenderers.Length;i++){
        //     playerMove.childSpriteRenderers[i].sortingLayerName = "1";
        // }
        
        DOTween.To(()=>white.GetComponent<SpriteRenderer>().color,colorL=>white.GetComponent<SpriteRenderer>().color=colorL,new Color(1f,1f,1f,0f),0.2f);
        DOTween.To(()=>playerLight.intensity,inten=>playerLight.intensity=inten,1f,3f);
        yield return new WaitForSeconds(2f);
        //player.transform.DOMoveY(player.transform.position.y+10,20f);
        yield return new WaitForSeconds(1f);
        endingText.DOText("Well done secret agent I knew you could do it",6f);
        yield return new WaitForSeconds(13f);
        SceneManager.LoadScene("Menu");

    }
    private IEnumerator SpawnDrone(){
        float RandomY;
        float RandomX;
        
        while(true){
            
            spawnDeley = Random.Range(5f,7f);
            RandomY = Random.Range(spawnGoMin.position.y,spawnGoMax.position.y);
            RandomX = Random.Range(spawnGoMin.position.x,spawnGoMax.position.x);

            for(int i=0;i<3;i++){
                yield return new WaitForSeconds(0.2f);
                drone = allPoolManager.GetPool(1);
                drone.transform.position = new Vector3(RandomX,RandomY,0f);
                drone.SetActive(true);
            }
            if(Random.Range(0,2)==0&&gameTime>20f){
                drone = allPoolManager.GetPool(6);
                drone.transform.position = new Vector3(RandomX,RandomY,0f);
                drone.SetActive(true);
            }
            if(Random.Range(0,3)==0&&gameTime>60f){
                StartCoroutine(MisailBarssa());
            }
            yield return new WaitForSeconds(spawnDeley-gameTime/200f);
        }
    }
    private IEnumerator MisailBarssa(){
        yield return new WaitForSeconds(Random.Range(4,8));
        float saveX=player.transform.position.x;
        WWWWW.transform.position = player.transform.position;
        for(int i=0;i<3;i++){
            WWWWW.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            WWWWW.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
        drone = allPoolManager.GetPool(7);
        drone.transform.position = new Vector3(saveX,playerCamera.transform.position.y-4f,0f);
        drone.SetActive(true);
    }
    private IEnumerator LaserBbang(){
        yield return new WaitForSeconds(2f);
    }
    private IEnumerator Opening(){
        yield return new WaitForSeconds(0.95f);
        timeSpeed = 0.2f;
        Time.timeScale = timeSpeed;
        yield return new WaitForSeconds(0.25f);
        timeSpeed = 1f;
        Time.timeScale = timeSpeed;
        yield return new WaitForSeconds(1.7f);
        black.SetActive(true);
        timeSpeed = 0.2f;
        Time.timeScale = timeSpeed;
        startWall.transform.DOMoveY(0.13f,3);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitForSeconds(0.1f);
        playerCamera.GetComponent<playercamera>().hihi = null;
        timeSpeed = 1f;
        Time.timeScale = timeSpeed;
        black.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        playerCamera.GetComponent<playercamera>().minPos.x = 7.65f;
        playerCamera.GetComponent<playercamera>().hihi = focusPoint;
        startLaser2.SetTrigger("barrsa");
        playerCamera.GetComponent<playercamera>().maxPos.y = playerCamera.transform.position.y;
        goUp = true;
        
        
        
        
        //playerCamera.GetComponent<playercamera>().hihi = focusPoint;
        StartCoroutine(SpawnDrone());
    }
    // void Quit(){
    //     Application.Quit();
    // }
    public float GetSaveFloat(string abc,float dd){
        return PlayerPrefs.GetFloat(abc,dd);
    }
    public int GetSaveInt(string abc,int dd){
        return PlayerPrefs.GetInt(abc,dd);
    }
    public void SetSaveFloat(string abc,float dd){
        PlayerPrefs.SetFloat(abc,dd);
    }
    public void SetSaveInt(string abc,int dd){
        PlayerPrefs.SetInt(abc,dd);
    }
    public void Continue(){
        if(!ESCON){
            //showMouse=true;
            ESC.SetActive(true);
            ESCON=true;
            Time.timeScale = 0f;
            mouse.SetActive(false);
        }
        else {
            ESC.SetActive(false);
            ESCON=false;
            Time.timeScale = timeSpeed;
            //showMouse=false;
            mouse.SetActive(true);
        }
    }
}

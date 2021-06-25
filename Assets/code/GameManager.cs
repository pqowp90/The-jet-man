using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;


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
    public float gameTime{get; private set;}
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
    
    void Awake()
    {
        
        moneySum = PlayerPrefs.GetInt("MONEY",1000);
        if(!menu)
            player = FindObjectOfType<playerMove>().gameObject;
            playerLight = player.GetComponentInChildren<Light2D>();
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
        BEST = PlayerPrefs.GetInt("BEST",0);
        
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
        gameTime+=Time.deltaTime;
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
            progressSlider.value = gameTime/360f;
            playerLight.pointLightOuterRadius = 5.53f + (gameTime/360f)*9f;
            if(gameTime>BEST)
                BEST = (int)gameTime;
            progressSliderBEST.value = BEST/360f;
        }
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
                drone = GameManager.instance.allPoolManager.GetPool(1);
                drone.transform.position = new Vector3(RandomX,RandomY,0f);
                drone.SetActive(true);
            }
            if(Random.Range(0,2)==0&&gameTime>20){
                drone = GameManager.instance.allPoolManager.GetPool(6);
                drone.transform.position = new Vector3(RandomX,RandomY,0f);
                drone.SetActive(true);
            }
            if(Random.Range(0,3)==0&&gameTime>60){
                StartCoroutine(MisailBarssa());
            }
            yield return new WaitForSeconds(spawnDeley);
        }
    }
    private IEnumerator MisailBarssa(){
        yield return new WaitForSeconds(Random.Range(4,8));
        Vector3 save=new Vector3(GameManager.instance.player.transform.position.x,GameManager.instance.player.transform.position.y-4f,0f);
        WWWWW.transform.position = GameManager.instance.player.transform.position;
        for(int i=0;i<3;i++){
            WWWWW.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            WWWWW.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
        drone = GameManager.instance.allPoolManager.GetPool(7);
        drone.transform.position = save;
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

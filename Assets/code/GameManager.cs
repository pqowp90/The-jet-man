using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


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
    private bool ESCON=false;
    [SerializeField]
    private bool menu=false;
    public bool showMouse=false;
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

    void Awake()
    {
        if(!menu)
            player = FindObjectOfType<playerMove>().gameObject;
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

    }
    private IEnumerator SpawnDrone(){
        while(true){
            
            float spawnDeley = 0f;
            spawnDeley = Random.Range(2f,5f);
            float RandomY = Random.Range(spawnGoMin.position.y,spawnGoMax.position.y);
            float RandomX = Random.Range(spawnGoMin.position.x,spawnGoMax.position.x);

            for(int i=0;i<3;i++){
                yield return new WaitForSeconds(0.2f);
                drone = GameManager.instance.allPoolManager.GetPool(1);
                drone.transform.position = new Vector3(RandomX,RandomY,0f);
                drone.SetActive(true);
            }
            yield return new WaitForSeconds(spawnDeley);
        }
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

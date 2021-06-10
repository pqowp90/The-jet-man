using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject focusPoint;
    [SerializeField]
    private Camera playerCamera;
    private GameObject black,mouse;
    private GameObject ESC;
    [SerializeField]
    private GameObject dronePrefab;
    private float timeSpeed=1f;
    public float recoilResistance{get;private set;}
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
    public Vector2 spawnMaxPos{get;private set;}
    public Vector2 spawnMinPos{get;private set;}
    [SerializeField]
    private Transform spawnGoMin;
    [SerializeField]
    private Transform spawnGoMax;
    public float speedenemy;

    void Awake()
    {
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
        spawnMaxPos=new Vector2(playerCamera.transform.position.x+7f,2.36f);
        spawnMinPos=new Vector2(playerCamera.transform.position.x+5f,-2.15f);
        
    }
    void Update()
    {
        spawnMaxPos=new Vector2(playerCamera.transform.position.x+7f,2.36f);
        spawnMinPos=new Vector2(playerCamera.transform.position.x+5f,-2.15f);
        if(!menu&&!showMouse){
            Cursor.visible = false;
        }
        if(Input.GetKeyDown(KeyCode.Escape)&&!menu){
            Continue();
        }
    }
    private IEnumerator SpawnDrone(){
        while(true){
            
            float spawnDeley = 0f;
            spawnDeley = Random.Range(8f,10f);
            float RandomY = Random.Range(spawnGoMin.position.y,spawnGoMax.position.y);
            float RandomX = Random.Range(spawnGoMin.position.x,spawnGoMax.position.x);

            for(int i=0;i<3;i++){
                yield return new WaitForSeconds(0.2f);
                Instantiate(dronePrefab, new Vector2(RandomX,RandomY),Quaternion.identity);
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
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitForSeconds(0.1f);
        playerCamera.GetComponent<playercamera>().hihi = null;
        timeSpeed = 1f;
        Time.timeScale = timeSpeed;
        black.SetActive(false);
        yield return new WaitForSeconds(2f);
        playerCamera.GetComponent<playercamera>().hihi = focusPoint;
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
            showMouse=true;
            ESC.SetActive(true);
            ESCON=true;
            Time.timeScale = 0f;
            mouse.SetActive(false);
        }
        else {
            ESC.SetActive(false);
            ESCON=false;
            Time.timeScale = timeSpeed;
            showMouse=false;
            mouse.SetActive(true);
        }
    }
}

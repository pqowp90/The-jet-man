using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject focusPoint;
    [SerializeField]
    private Camera playerCamera;
    private GameObject black;
    private GameObject ESC;
    private float timeSpeed=1f;
    private bool ESCON=false;
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

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        focusPoint = GameObject.Find("FocusPoint");
        playerCamera = FindObjectOfType<Camera>();
        black = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        ESC = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        StartCoroutine(Opening());
        maxPos=new Vector2(9,9);
        minPos=new Vector2(-9,-9);
    }
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            Continue();
        }
    }
    private IEnumerator LaserBbang(){
        yield return new WaitForSeconds(3f);
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
    }
    // void Quit(){
    //     Application.Quit();
    // }
    public void Continue(){
        if(!ESCON){
            ESC.SetActive(true);
            ESCON=true;
            Time.timeScale = 0f;
        }
        else {ESC.SetActive(false);ESCON=false;Time.timeScale = timeSpeed;}
    }
}

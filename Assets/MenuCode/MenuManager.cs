using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [SerializeField]
    private Camera maincamera;
    [SerializeField]
    private Animator cameraAnimator;
    [SerializeField]
    private Animator gunAnimator;
    public int sceneNum;
    [SerializeField]
    private Animator animator,mirrorballAnimator;
    [SerializeField]
    private ScrollbarCode[] scrollbarCode;
    [SerializeField]
    private float nonotime;
    private bool nonono;
    private Vector3 mousePosed;//마우스포지션 였던것
    [SerializeField]
    private int gunCnt;
    private string gunYes;
    [SerializeField]
    public bool[] gunChk{get;private set;}
    void Start()
    {
        instance = this;

        gunYes = PlayerPrefs.GetString("GunSave","0000000000");
        for(int i=0;i<gunCnt;i++){
            gunChk[i]=(gunYes[i]=='1')?true:false;
        }
    }
    private void BuyGun(int num){
        gunChk[num]=true;
        gunYes="";
        for(int i=0;i<gunCnt;i++){
            if(gunChk[i]==true){
                gunYes+="1";
            }
            else
                gunYes+="0";

        }
        PlayerPrefs.SetString("GunSave",gunYes);
        Debug.Log(gunYes);
    }
    void Update()
    {
        Jamsu();
        if(Input.GetKeyDown(KeyCode.Escape)&&sceneNum!=0){
            Back();
        }
    }
    private void Jamsu(){
        if(mousePosed!=Camera.main.ScreenToWorldPoint(Input.mousePosition)){
            nonotime=0f;
            if(nonono){
                mirrorballAnimator.SetBool("jamsu",false);
                nonono=false;
            }
        }
        else
            if(sceneNum==0) nonotime += Time.deltaTime;
        mousePosed = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(!nonono){
            if(nonotime>15f){
                mirrorballAnimator.SetBool("jamsu",true);
                nonono = true;
            }
        }
    }
    public void Change(){
        switch(sceneNum){
            case 1:

            break;
        }
    }
    public void StartGame(){
        SceneManager.LoadScene("main");
    }
    public void StartClick(){
        FindObjectOfType<Canvas>().enabled = false;
        cameraAnimator.enabled = true;
        maincamera.GetComponent<AudioSource>().enabled = true;
        gunAnimator.SetTrigger("MenuPlayerGo");
    }
    public void StoreClick(){
        maincamera.GetComponent<playercamera>().zoom=1f;
        sceneNum=1;
        UpdateUi();
    }
    public void SettingClick(){
        maincamera.GetComponent<playercamera>().zoom=3f;
        sceneNum=2;
        UpdateUi();
    }
    public void Back(){
        if(sceneNum==2){
            for(int i=0;i<2;i++){
                scrollbarCode[i].Set();
            }
        }
        maincamera.GetComponent<playercamera>().zoom=0f;
        sceneNum=0;
        UpdateUi();
    }
    public void Quit(){
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
    private void UpdateUi(){
        animator.SetInteger("Num",sceneNum);
        animator.SetTrigger("Bbang");
    }
    
}
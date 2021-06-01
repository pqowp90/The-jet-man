using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [SerializeField]
    private BackgroundMusic backgroundMusic;
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
    private Animator playerAnimator;
    [SerializeField]
    private Animator dandandanAnimator;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private float nonotime;
    [SerializeField]
    private playercamera playerCamera;
    private bool nonono;
    private Vector3 mousePosed;//마우스포지션 였던것
    [SerializeField]
    private int gunCnt;
    private string gunYes;
    [SerializeField]
    public int[,] gunChk=new int[3,2];
    public int money;
    void Start()
    {
        instance = this;
        LoadGunSave();
        money = PlayerPrefs.GetInt("MONEY",1000);
        UpdateUI();
    }
    public void LoadGunSave(){
        
        gunYes = PlayerPrefs.GetString("GunSave","0000000000");
        for(int i=0;i<gunCnt;i++){
            gunChk[i,0]=(gunYes[i]=='1')?1:0;
        }
        for(int i=0;i<gunCnt;i++){
            gunChk[i,1]=(int)char.GetNumericValue(gunYes[gunCnt+i]);;
        }
        Debug.Log(gunYes);
    }
    public void BuyGun(int num){
        gunChk[num,0]=1;
        gunYes="";
        for(int i=0;i<gunCnt;i++){
            if(gunChk[i,0]==1){
                gunYes+='1';
            }
            else
                gunYes+='0';

        }
        for(int i=0;i<gunCnt;i++){
            gunYes+=gunChk[i,1].ToString();
        }
        PlayerPrefs.SetString("GunSave",gunYes);
    }
    void Update()
    {
        Cursor.visible = true;
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
            if(nonotime>20f){
                
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
        backgroundMusic.FaidOut();
        FindObjectOfType<Canvas>().enabled = false;
        cameraAnimator.enabled = true;
        maincamera.GetComponent<AudioSource>().enabled = true;
        gunAnimator.SetTrigger("MenuPlayerGo");
    }
    public void StoreClick(){
        LoadGunSave();
        maincamera.GetComponent<playercamera>().zoom=1f;
        sceneNum=1;
        UpdateMenu();
    }
    public void SettingClick(){        
        maincamera.GetComponent<playercamera>().zoom=3f;
        sceneNum=2;
        UpdateMenu();
    }
    public void Back(){
        if(sceneNum==2){
            for(int i=0;i<2;i++){
                scrollbarCode[i].Set();
            }
        }
        maincamera.GetComponent<playercamera>().zoom=0f;
        sceneNum=0;
        UpdateMenu();
    }
    public void Quit(){
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
    private void UpdateMenu(){
        animator.SetInteger("Num",sceneNum);
        animator.SetTrigger("Bbang");
    }
    public void UpdateUI(){
        moneyText.text = string.Format("{0}$",money);
    }
    
}
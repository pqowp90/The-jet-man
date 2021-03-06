using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [SerializeField]
    private GameObject backgroundMusicPrefab;
    private BackgroundMusic backgroundMusic;
    [SerializeField]
    private Camera maincamera;
    [SerializeField]
    private Animator cameraAnimator;
    [SerializeField]
    private Animator gunAnimator;
    public int sceneNum;
    [SerializeField]
    private Animator animator,mirrorballAnimator,storeAnimator;
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
    public AudioSource audioSource;
    private bool nonono;
    private Vector3 mousePosed;//마우스포지션 였던것
    [SerializeField]
    private int gunCnt;
    private string gunYes;
    [SerializeField]
    public int[,] gunChk=new int[6,2];
    public int money;
    [SerializeField]
    private AudioClip[] audioClip;
    public int gunNewSelect=-1;
    [SerializeField]
    public StoreGuns[] gunNewThis;
    [SerializeField]
    private Text selectGun;
    private AudioMixer mixer;
    [SerializeField]
    private SelectThis selectThis;
    void Awake()
    {
        mixer = Resources.Load<AudioMixer>("mixer");
        
        Time.timeScale = 1f;
        Screen.orientation = ScreenOrientation.Portrait;
        if (FindObjectOfType<BackgroundMusic>()==null)
            backgroundMusic = Instantiate(backgroundMusicPrefab).GetComponent<BackgroundMusic>();
        else 
            backgroundMusic = FindObjectOfType<BackgroundMusic>();
        backgroundMusic.GetComponent<AudioSource>().time = 0;
        backgroundMusic.GetComponent<AudioSource>().Play();
        PlayerPrefs.GetInt("Select1",-1);
        PlayerPrefs.GetInt("Select2",-1);
        audioSource = GetComponent<AudioSource>();
        instance = this;
        money = PlayerPrefs.GetInt("MONEY",1000);
        LoadGunSave();
        UpdateUI();
        
    }
    void Start(){
        StartVolumeSet();
    }
    public void Chogihwa(){
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
    private void StartVolumeSet(){
        for(int i=0;i<scrollbarCode.Length;i++){
            mixer.SetFloat(scrollbarCode[i].abc,Mathf.Log10(PlayerPrefs.GetFloat(scrollbarCode[i].abc))*20);
        }
    }
    public void LoadGunSave(){
        
        gunYes = PlayerPrefs.GetString("GunSave","0000000000000000000000000");
        for(int i=0;i<gunCnt;i++){
            gunChk[i,0]=(gunYes[i]=='0')?0:((gunYes[i]=='1')?1:2);
        }
        for(int i=0;i<gunCnt;i++){
            gunChk[i,1]=(int)char.GetNumericValue(gunYes[gunCnt+i]);
        }
        //Debug.Log(gunYes);
    }
    public void multyGo(){
        SceneManager.LoadScene("multy");
    }
    public void cantBuy(){
        audioSource.clip = audioClip[1];
        audioSource.volume = 1f;
        audioSource.time = 0f;
        audioSource.Play();
        
    }
    public void BuySound(){
        audioSource.clip = audioClip[0];
        audioSource.volume = 0.5f;
        audioSource.time = 0.1f;
        audioSource.Play();
    }
    public void BuyGun(){
        
        gunYes="";
        for(int i=0;i<gunCnt;i++){
            if(gunChk[i,0]==1){
                gunYes+='1';
            }else if(gunChk[i,0]==2){
                gunYes+='2';
            }
            else
                gunYes+='0';

        }
        for(int i=0;i<gunCnt;i++){
            gunYes+=gunChk[i,1].ToString();
        }
        PlayerPrefs.SetString("GunSave",gunYes);
        ResetStore();
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
    
    public void SelectGun(bool hi){
        if(gunChk[gunNewSelect,0]==0) {
            GoSound(1);
            return;
        }
        bool oneOrtwo = false;
        GoSound(5);
        int s1,s2;
        s1 = PlayerPrefs.GetInt("Select1",-1);
        s2 = PlayerPrefs.GetInt("Select2",-1);
        storeAnimator.SetBool("Select",false);
        if(hi){
            PlayerPrefs.SetInt("Select1",gunNewSelect);
            oneOrtwo = true;
            if(s2==gunNewSelect){
                PlayerPrefs.SetInt("Select2",-1);//1번을 했는데 2번이랑 같은거면 2번지우고 1번으로
                selectThis.SelectSprite(false,99);
            }
        }
        else{
            if(s1==gunNewSelect)//2번을 했는데 1번에 있으면 취소
                return;
            if(s1!=-1){//1번이 비어있지 않으면 넣어줌
                oneOrtwo = false;
                PlayerPrefs.SetInt("Select2",gunNewSelect);
            }
        }
        selectThis.SelectSprite(oneOrtwo,gunNewSelect);
        ResetStore();
    }
    void ResetStore(){
        for(int i=0;i<gunCnt;i++){
            gunNewThis[i].UpdateButten();
        }
    }
    public void SelectClick(){
        if(gunChk[gunNewSelect,0]==0){
            return;
        }
        storeAnimator.SetBool("Select",true);
        GoSound(7);
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
        if(PlayerPrefs.GetInt("Select1",-1)>=0){
            PlayerPrefs.SetInt("S1UP",gunChk[PlayerPrefs.GetInt("Select1",-1),1]);
            if(PlayerPrefs.GetInt("Select2",-1)!=-1)
                PlayerPrefs.SetInt("S2UP",gunChk[PlayerPrefs.GetInt("Select2",-1),1]);
            backgroundMusic.FaidOut();
            FindObjectOfType<Canvas>().enabled = false;
            FindObjectOfType<Canvas>().gameObject.SetActive(false);
            cameraAnimator.enabled = true;
            maincamera.GetComponent<AudioSource>().enabled = true;
            gunAnimator.SetTrigger("MenuPlayerGo");
        }
        else{
            Debug.Log("총을 최소 하나 이상 선택하세요");
            StartCoroutine(SelectGunNow());
        }
        
    }
    private IEnumerator SelectGunNow(){
        DOTween.To(()=>selectGun.color,colorL=>selectGun.color=colorL,new Color(0.8207547f,0.8207547f,0.8207547f,1f),0.2f);
        yield return new WaitForSeconds(2f); 
        DOTween.To(()=>selectGun.color,colorL=>selectGun.color=colorL,new Color(0.8207547f,0.8207547f,0.8207547f,0f),0.2f);
    }
    public void GoSound(int num){
        audioSource.volume = 0.7f;
        audioSource.time = 0f;
        audioSource.clip = audioClip[num];
        audioSource.Play();
    }
    public void StoreClick(){
        GoSound(7);
        audioSource.time = 0f;
        LoadGunSave();
        maincamera.GetComponent<playercamera>().zoom=1.3f;
        sceneNum=1;
        UpdateMenu();
    }
    public void SettingClick(){      
        GoSound(7);
        audioSource.time = 0f;
        maincamera.GetComponent<playercamera>().zoom=3f;
        sceneNum=2;
        UpdateMenu();
    }
    public void Back(){
        GoSound(7);
        audioSource.time = 0f;
        storeAnimator.SetBool("Select",false);
        if(sceneNum==2){
            for(int i=0;i<3;i++){
                scrollbarCode[i].Set();
            }
        }
        maincamera.GetComponent<playercamera>().zoom=0f;
        sceneNum=0;
        UpdateMenu();
    }
    public void Quit(){
        GoSound(7);
        audioSource.time = 0.15f;
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
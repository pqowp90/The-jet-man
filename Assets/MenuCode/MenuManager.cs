using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Camera maincamera;
    [SerializeField]
    private Animator cameraAnimator;
    [SerializeField]
    private Animator gunAnimator;
    public int sceneNum;
    [SerializeField]
    private Animator animator;
    void Start()
    {

    }
    void Update()
    {

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
        maincamera.GetComponent<playercamera>().zoom=0f;
        sceneNum=0;
        UpdateUi();
    }
    private void UpdateUi(){
        animator.SetInteger("Num",sceneNum);
        animator.SetTrigger("Bbang");
    }
}

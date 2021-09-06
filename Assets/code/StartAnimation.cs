using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StartAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform[] transforms;
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        StartCoroutine(StartAnimationCor());
    }
    private IEnumerator StartAnimationCor(){
        yield return new WaitForSeconds(4.5f);
        transforms[0].DOShakePosition(6f,0.07f,100,90f,false,false);
        yield return new WaitForSeconds(0.5f);
        transforms[1].gameObject.SetActive(true);
        transforms[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(5.5f);
        transforms[3].gameObject.SetActive(true);
        transforms[0].DOShakePosition(2f,0.5f,120,50f,false,true);
        transforms[7].GetComponent<AudioSource>().time = 25f;
        transforms[7].gameObject.SetActive(true);
        transforms[1].gameObject.SetActive(false);
        transforms[2].gameObject.SetActive(false);
        target = transforms[4].gameObject;
        transforms[4].GetComponent<Animator>().SetBool("NoNo",true);
        yield return new WaitForSeconds(2.5f);
        Camera.main.DOOrthoSize(2.7f,0.5f);
        yield return new WaitForSeconds(1f);
        transforms[6].gameObject.SetActive(true);
        Camera.main.DOOrthoSize(2f,0.5f);
        yield return new WaitForSeconds(0.95f);
        for(int i=0;i<15;i++){
            transforms[8].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.0625f);
            transforms[8].gameObject.SetActive(false);
            yield return new WaitForSeconds(0.0625f);
        }
        yield return new WaitForSeconds(0.05f);
        transforms[8].gameObject.SetActive(true);
        transforms[9].gameObject.SetActive(true);
        transforms[7].GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
        
        //DOTween.To(()=>Camera.main.DOOrthoSize.color,colorL=>light2D.color=colorL,new Color(1f,0.4849057f,0.4849057f,1f),0.1f);
    }
    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed; // 카메라가 따라갈 속도
    private Vector3 targetPosition; // 대상의 현재 위치

    void Update()
    {
        if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, transforms[0].position.z);
            transforms[5].position = Vector3.Lerp(transforms[0].position, targetPosition, moveSpeed * Time.deltaTime);            
        }
    }
    public void Skip(){
        SceneManager.LoadScene("Menu");
    }
}

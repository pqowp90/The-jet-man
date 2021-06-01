using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreGuns : MonoBehaviour
{
    [SerializeField]
    private int myGunSet;
    [SerializeField]
    private GameObject select;
    [SerializeField]
    private GameObject buy;
    [SerializeField]
    private GameObject upgread;
    [SerializeField]
    private MenuManager menuManager;
    [SerializeField]
    private GameObject lockImage;
    [SerializeField]
    private int[] gunCost;
    [SerializeField]
    private int max;
    [SerializeField]
    private Text costText;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("UpdateButten",0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
    void UpdateButten(){
        costText.text = ""+gunCost[menuManager.gunChk[myGunSet,1]];
        lockImage.SetActive(menuManager.gunChk[myGunSet,0]==0);
        buy.SetActive(menuManager.gunChk[myGunSet,0]==0);
        upgread.SetActive(menuManager.gunChk[myGunSet,0]==1);
        if(menuManager.gunChk[myGunSet,1]==max){
            upgread.transform.GetChild(0).GetComponent<Text>().text="MAX";
        }
        select.GetComponent<Image>().color = (menuManager.gunChk[myGunSet,0]==0)?new Color(0.6235294f,0.6235294f,0.6235294f,1f):new Color(0.2078431f,0.8039216f,0.2156863f,1f);
        buy.GetComponent<Image>().color = (menuManager.money<gunCost[menuManager.gunChk[myGunSet,1]])?new Color(0.7075472f,0.249644f,0.249644f,1f):new Color(0.2078431f,0.8039216f,0.2156863f,1f);
        upgread.GetComponent<Image>().color = (menuManager.money<gunCost[menuManager.gunChk[myGunSet,1]])?new Color(0.7075472f,0.249644f,0.249644f,1f):new Color(0.2078431f,0.8039216f,0.2156863f,1f);
    }
    public void BuyGun(){
        Debug.Log(menuManager.gunChk[myGunSet,1]);
        if(menuManager.money<gunCost[menuManager.gunChk[myGunSet,1]])
            return;
        if(menuManager.gunChk[myGunSet,1]==max)
            return;
        menuManager.gunChk[myGunSet,1]++;
        menuManager.BuyGun(myGunSet);
        menuManager.money-=gunCost[0];
        PlayerPrefs.SetInt("MONEY",menuManager.money);
        menuManager.UpdateUI();
        UpdateButten();
    }
}

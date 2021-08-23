using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreGuns : MonoBehaviour
{
    [SerializeField]
    private int myGunSet;
    [SerializeField]
    private GameObject buy;
    [SerializeField]
    private MenuManager menuManager;
    [SerializeField]
    private GameObject lockImage;
    [SerializeField]
    private int[] gunCost;
    [SerializeField]
    private int max;
    [SerializeField]
    private Text costText,upText;
    void Start()
    {
        //Invoke("UpdateButten",0.5f);
        UpdateButten();
    }

    public void UpdateButten(){
        

        costText.text = string.Format("{0}",(menuManager.gunChk[myGunSet,0]==2)?0:gunCost[menuManager.gunChk[myGunSet,1]]);

        if(menuManager.gunChk[myGunSet,0]!=0)
            upText.text = string.Format("+{0}",(menuManager.gunChk[myGunSet,0]==2)?(menuManager.gunChk[myGunSet,1]):(menuManager.gunChk[myGunSet,1]-1));

        //lockImage.SetActive(menuManager.gunChk[myGunSet,0]==0);
        
        // buy.SetActive(menuManager.gunChk[myGunSet,0]==0);
        // upgread.SetActive(menuManager.gunChk[myGunSet,0]>0);
        if(menuManager.gunChk[myGunSet,0]>0){
            buy.transform.GetComponentInChildren<Text>().text="upgread";
        }else{
            buy.transform.GetComponentInChildren<Text>().text="buy";
        }

        if(menuManager.gunChk[myGunSet,0]==2){
            buy.transform.GetComponentInChildren<Text>().text="MAX";
        }

        buy.GetComponent<Image>().color = (menuManager.money<gunCost[menuManager.gunChk[myGunSet,1]])?new Color(0.8490566f,0.2763439f,0.2763439f,1f):new Color(0.1058651f,0.5754717f,0.13548f,1f);
        if(menuManager.gunChk[myGunSet,0]==2)
            buy.GetComponent<Image>().color = new Color(0.1058651f,0.5754717f,0.13548f,1f);
        Debug.Log("씨발 왜안됨?"+menuManager.gunChk[myGunSet,0]);
    }
    void OnEnable(){
        menuManager.gunNewSelect = myGunSet;
    }

    public void BuyGun(){
        //Debug.Log(menuManager.gunChk[myGunSet,1]);
        if(menuManager.money<gunCost[menuManager.gunChk[myGunSet,1]]||menuManager.gunChk[myGunSet,0]==2){
            menuManager.cantBuy();
            return;
        }
        if(menuManager.gunChk[myGunSet,1]==max+1){
            menuManager.cantBuy();
            return;
        }
        menuManager.BuySound();
        if(menuManager.gunChk[myGunSet,1]!=max){
            menuManager.gunChk[myGunSet,0]=1;
            menuManager.gunChk[myGunSet,1]++;
        }
        else{
            menuManager.gunChk[myGunSet,0]=2;
        }
        menuManager.money-=gunCost[menuManager.gunChk[myGunSet,1]-1];
        
            
        menuManager.BuyGun();
        PlayerPrefs.SetInt("MONEY",menuManager.money);
        menuManager.UpdateUI();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoolButten : MonoBehaviour
{
    [SerializeField]
    private int firstNum;
    [SerializeField]
    private string abc;
    [SerializeField]
    private bool setBool;
    [SerializeField]
    private BackgroundMove backgroundMove;
    private GameManager gameManager;
    private bool hello;
    [SerializeField]
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        gameManager = GameManager.instance;
        hello = (gameManager.GetSaveInt(abc,firstNum)==0)?false:true;
        UpdateUi();
    }
    public void click(){
        gameManager.SetSaveInt(abc,(setBool)?1:0);
        backgroundMove.UpdateUi();
    }
    void Update()
    {
        if(Input.GetMouseButtonUp(0)){
            UpdateUi();
        }
    }
    void UpdateUi(){
        hello = (gameManager.GetSaveInt(abc,0)==0)?false:true;
        image.color = (hello!=setBool)?new Color(0.7f,0.7f,0.7f,1f):new Color(1f,1f,1f,1f);
    }
}

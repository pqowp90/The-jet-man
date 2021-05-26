using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarCode : MonoBehaviour
{
    [SerializeField]
    private float firstNum;
    [SerializeField]
    private string abc;
    [SerializeField]
    private Text Num;



    private Scrollbar scrollbar;
    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        scrollbar.value = GameManager.instance.GetSaveFloat(abc,firstNum);
    }

    void Update()
    {
        Num.text = string.Format("{0}",(int)(scrollbar.value*100));
    }
    public void Set(){
        GameManager.instance.SetSaveFloat(abc,scrollbar.value);
    }
}

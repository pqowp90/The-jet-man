using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarCode : MonoBehaviour
{
    [SerializeField]
    private Text Num;
    [SerializeField]
    private int saveNum;
    [SerializeField]
    private string[] save;

    private Scrollbar scrollbar;
    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
    }

    void Update()
    {
        Num.text = string.Format("{0}",(int)(scrollbar.value*100));
        //PlayerPrefs.SetFloat(save[saveNum],scrollbar.value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectThis : MonoBehaviour
{
    [SerializeField]
    private Sprite[] guns;
    public Sprite one;
    public Sprite two;
    private Transform zz;
    private SpriteRenderer[] hoho = new SpriteRenderer[2];
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SelectSprite(true,PlayerPrefs.GetInt("Select1",-1));
        SelectSprite(false,PlayerPrefs.GetInt("Select2",-1));
    }

    public void SelectSprite(bool b , int a){
        if(a<0)return;
        if(b) one = guns[a];
        else two = (a==99)?null:guns[a];
        if(!zz){
            zz = FindObjectOfType<FindThis>().transform;
            hoho[0]=zz.GetChild(2).GetComponentInChildren<SpriteRenderer>();
            hoho[1]=zz.GetChild(3).GetComponentInChildren<SpriteRenderer>();
        }
        

        hoho[0].sprite = one;
        hoho[1].sprite = two;
    }
}

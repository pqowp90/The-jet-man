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
    }

    public void SelectSprite(bool b , int a){
        if(b) one = guns[a];
        else two = guns[a];
        if(!zz){
            zz = FindObjectOfType<FindThis>().transform;
            hoho[0]=zz.GetChild(0).GetComponentInChildren<SpriteRenderer>();
            hoho[1]=zz.GetChild(1).GetComponentInChildren<SpriteRenderer>();
        }
        if(a==2){
            hoho[1].sprite = null;
            return;
        }
        hoho[0].sprite = one;
        hoho[1].sprite = two;
    }
}

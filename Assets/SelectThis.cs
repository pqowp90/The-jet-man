using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectThis : MonoBehaviour
{
    [SerializeField]
    private Sprite[] guns;
    public Sprite one;
    public Sprite two;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SelectSprite(bool b , int a){
        if(b) one = guns[a];
        else two = guns[a];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPooler : MonoBehaviour
{
    [SerializeField]
    private int setIndex;
    public int index{get; private set;}

    private void Start(){
        index = setIndex;
    }
    public void Despawn(){
        transform.SetParent(GameManager.instance.allPoolManager.transform);
        gameObject.SetActive(false);
        
    }
}

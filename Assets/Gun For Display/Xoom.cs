using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Xoom : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other){
        if(!other.CompareTag("display"))return;
        transform.DOScale(new Vector3(1.155f,1.155f,1.155f),0.2f);
    }
    void OnTriggerExit2D(Collider2D other){
        if(!other.CompareTag("display"))return;
        transform.DOScale(new Vector3(0.665f,0.665f,0.665f),0.2f);
    }
}

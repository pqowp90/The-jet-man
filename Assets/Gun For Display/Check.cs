using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public bool ohohoh;
    void OnTriggerEnter2D(Collider2D other){
        ohohoh = true;
    }
    void OnTriggerExit2D(Collider2D other){
        ohohoh = false;
    }
}

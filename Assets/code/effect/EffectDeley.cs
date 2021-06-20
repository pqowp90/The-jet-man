using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDeley : MonoBehaviour
{
    [SerializeField]
    private float deley;
    private AllPooler allPooler;
    private void Start(){
        allPooler = GetComponent<AllPooler>();
        Invoke("GoIn",deley);
    }
    private void OnEnable(){
        Invoke("GoIn",deley);
    }
    private void GoIn(){
        allPooler.Despawn();
    }
}

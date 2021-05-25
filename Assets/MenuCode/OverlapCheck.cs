using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapCheck : MonoBehaviour
{
    void Start()
    {
        if(GameObject.Find("BackGroundMusic")!=null)
            Destroy(this);
    }
    void Update()
    {
        
    }
}

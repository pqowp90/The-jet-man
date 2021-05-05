using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCode : MonoBehaviour
{
    [SerializeField]
    playerMove playerMove;
    void Start()
    {
        
    }

    public void BarSsaChong(){
        playerMove.Shoting(1);
    }
}

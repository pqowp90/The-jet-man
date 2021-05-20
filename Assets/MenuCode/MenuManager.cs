using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Camera maincamera;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void StoreClick(){
        maincamera.GetComponent<playercamera>().zoom=1f;
    }
}

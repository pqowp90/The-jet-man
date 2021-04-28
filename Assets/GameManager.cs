using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get{
            if(_instance == null){
                _instance = FindObjectOfType<GameManager>();
                if(_instance == null){
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();                    
                }                
            }
            return _instance;
        }
    }
    public Vector2 maxPos{get;private set;}
    public Vector2 minPos{get;private set;}

    void Start()
    {
        maxPos=new Vector2(9,9);
        minPos=new Vector2(-9,-9);
    }
    void Update()
    {
        
    }
}

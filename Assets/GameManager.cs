using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject black;
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
        black = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        StartCoroutine(Opening());
        maxPos=new Vector2(9,9);
        minPos=new Vector2(-9,-9);
    }
    void Update()
    {
        
    }
    private IEnumerator Opening(){
        yield return new WaitForSeconds(0.95f);
        Time.timeScale = 0.15f;
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1.7f);
        black.SetActive(true);
        Time.timeScale = 0.15f;

    }
}

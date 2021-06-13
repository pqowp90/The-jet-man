using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private float startPos,length,temp,dist;
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private SpriteRenderer spriteRenderer1,spriteRenderer2;
    [SerializeField]
    private Sprite[] sprite;
    [SerializeField]
    private bool hello=false;
    [SerializeField]
    private GameObject leftWall;
    
    void Start()
    {
        spriteRenderer1.sprite = sprite[GameManager.instance.GetSaveInt("Background",0)];
        spriteRenderer2.sprite = sprite[GameManager.instance.GetSaveInt("Background",0)];
        startPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        
        if(hello)
            return;
        temp = cam.transform.position.y;
        transform.position = new Vector3(transform.position.x, startPos, transform.position.z);

        if(temp >  startPos + length) {startPos += length;leftWall.SetActive(true);}
        else if(temp <  startPos - length) startPos -= length;
    }
    public void UpdateUi(){
        spriteRenderer1.sprite = sprite[GameManager.instance.GetSaveInt("Background",0)];
        spriteRenderer2.sprite = sprite[GameManager.instance.GetSaveInt("Background",0)];
    }
}

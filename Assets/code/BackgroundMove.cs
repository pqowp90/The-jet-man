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
    
    void Start()
    {
        spriteRenderer1.sprite = sprite[GameManager.instance.GetSaveInt("Background",0)];
        spriteRenderer2.sprite = sprite[GameManager.instance.GetSaveInt("Background",0)];
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        
        if(hello)
            return;
        temp = cam.transform.position.x;
        transform.position = new Vector3(startPos, transform.position.y, transform.position.z);

        if(temp >  startPos + length) startPos += length;
        else if(temp <  startPos - length) startPos -= length;
    }
    public void UpdateUi(){
        spriteRenderer1.sprite = sprite[GameManager.instance.GetSaveInt("Background",0)];
        spriteRenderer2.sprite = sprite[GameManager.instance.GetSaveInt("Background",0)];
    }
}

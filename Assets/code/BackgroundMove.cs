using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private float startPos,length,temp,dist;
    [SerializeField]
    private GameObject cam;
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        temp = cam.transform.position.x;
        transform.position = new Vector3(startPos, transform.position.y, transform.position.z);

        if(temp >  startPos + length) startPos += length;
        else if(temp <  startPos - length) startPos -= length;

    }
}

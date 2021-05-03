using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMove : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    void Update()
    {
        if(player.transform.position.x>transform.position.x)
            transform.position = new Vector3(player.transform.position.x,transform.position.y,0f);
    }
}

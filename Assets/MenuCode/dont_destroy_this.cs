using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dont_destroy_this : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}

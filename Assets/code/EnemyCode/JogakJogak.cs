using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogakJogak : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprite;
    private GameObject jogak;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite[Random.Range(0,3)];
    }

}

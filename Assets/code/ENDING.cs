using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ENDING : MonoBehaviour
{
    [SerializeField]
    private Text text;
    private int M;
    private int MMM=0;
    // Start is called before the first frame update
    void Start()
    {
        M = (int)GameManager.instance.gameTime;
        DOTween.To(()=>MMM,MMM1=>MMM=MMM1,M,0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = string.Format("{0}M",MMM);
    }
}

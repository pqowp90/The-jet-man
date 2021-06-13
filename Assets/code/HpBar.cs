using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour

{
    public Slider slider;
    public Vector3 offset;
    void Start(){
        slider.gameObject.SetActive(false);
    }
    public void sethealth(float health, float maxhealth){
        slider.gameObject.SetActive(health<maxhealth);
        slider.maxValue = maxhealth;
        slider.value = health;
        //CancelInvoke();
        //Invoke("nono",5);
        if (health<=0)
            slider.gameObject.SetActive(false);
    }
    void nono(){
        slider.gameObject.SetActive(false);
    }
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}

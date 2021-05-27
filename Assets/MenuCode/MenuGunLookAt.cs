using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGunLookAt : MonoBehaviour
{
    private Vector3 oPosition,target;
    private float rotateDegree,headRotate,wheelInput;
    [SerializeField]
     private Animator gunAnimator;
    public int GunSet=0;
    void Start()
    {
        
    }

    void Update()
    {
        //-----------------------------------------------------------------------------------------
        oPosition = transform.position;
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rotateDegree = Mathf.Atan2(target.y - oPosition.y, target.x - oPosition.x)*Mathf.Rad2Deg;
        headRotate = Mathf.Atan2(target.y - oPosition.y, Mathf.Abs(target.x - oPosition.x))*Mathf.Rad2Deg;
        //spriteRenderer.flipX=(rotateDegree<90&&rotateDegree>-90)?true:false;
        transform.rotation=new Quaternion(0f,(rotateDegree<90&&rotateDegree>-90)?0f:180f,0f,0f);
        transform.GetChild(0).transform.rotation = Quaternion.Euler (0f, (rotateDegree<90&&rotateDegree>-90)?0f:180f,headRotate/3f);
        transform.GetChild(1).transform.rotation = Quaternion.Euler (0f, (rotateDegree<90&&rotateDegree>-90)?0f:180f,headRotate);//HeadRotation
        //-----------------------------------------------------------------------------------------
        wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if(wheelInput>0){
                if(GunSet<1)
                    GunSet++;
                gunAnimator.SetInteger("GunSet",GunSet);
            }
            else if(wheelInput<0){
                if(GunSet>0)
                    GunSet--;
                gunAnimator.SetInteger("GunSet",GunSet);
            }
        
        gunAnimator.SetInteger("GunSet",GunSet);
    }
}

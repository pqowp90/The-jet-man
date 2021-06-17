using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone1 : EnemyMove
{
    private Vector3 diff;
    private float rotationZ;
    private Transform myGun;
    void Start()
    {
        myGun = transform.GetChild(0);
    }
    protected override void Move()
    {
        base.Move();
        if(myGun==null){
            myGun = transform.GetChild(0);
        }
        diff = GameManager.instance.player.transform.position - transform.position;
        diff.Normalize();
        rotationZ = Mathf.Atan2(diff.y,diff.x)*Mathf.Rad2Deg;
        myGun.rotation = Quaternion.Euler(myGun.rotation.x,myGun.rotation.y,rotationZ);
    }
    public IEnumerator FireGun(){
        while(true){
            yield return new WaitForSeconds(1f);
            
        }
    }
}

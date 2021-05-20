using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPulling : MonoBehaviour
{
    [SerializeField]
    private GameObject poolingObjectPrefab;
    [SerializeField]
    private Queue<BulletMove> pooliongObjectQueue = new Queue<BulletMove>();
    
    public static ObjectPulling instance;
    private void Awake()
    {
        instance = this;
        Instialize(10);
    }
    private void Update(){
        //Debug.Log(instance.pooliongObjectQueue.Count);
    }
    private BulletMove CreatNewObject(){
        var newObj = Instantiate(poolingObjectPrefab,transform).GetComponent<BulletMove>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }
    private void Instialize(int count){
        for(int i=0;i<count;i++){
            pooliongObjectQueue.Enqueue(CreatNewObject());
        }
    }
    public static BulletMove GetObject(){
        if(instance.pooliongObjectQueue.Count > 0){
            var obj =instance.pooliongObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            obj.GetComponent<BulletMove>().StartDeley();
            return obj;
        }
        else{
            var newObj = instance.CreatNewObject();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            newObj.GetComponent<BulletMove>().StartDeley();
            return newObj;
        }
    }
    public static void ReturnObject(BulletMove bullet){
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(instance.transform);
        instance.pooliongObjectQueue.Enqueue(bullet);
    }
    

}

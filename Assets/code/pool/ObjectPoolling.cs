using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class ObjectPoolling : MonoBehaviour
{
    [SerializeField]
    private GameObject poolingObjectPrefab;
    [SerializeField]
    private Queue<BulletMove> pooliongObjectQueue = new Queue<BulletMove>();
    private bool isMulty;
    
    public static ObjectPoolling instance;
    private void Awake()
    {
        isMulty = SceneManager.GetActiveScene().buildIndex==4;
        instance = this;
        //Instialize(10);
    }
    private void Update(){
        //Debug.Log(instance.pooliongObjectQueue.Count);
    }
    private BulletMove CreatNewObject(){
        
        var newObj = (isMulty)?PhotonNetwork.Instantiate("BulletPrefeb",transform.position,Quaternion.identity).GetComponent<BulletMove>()
            :Instantiate(poolingObjectPrefab,transform).GetComponent<BulletMove>();
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
            obj.GetComponent<BulletMove>().StartDeley();
            return obj;
        }
        else{
            var newObj = instance.CreatNewObject();
            newObj.transform.SetParent(null);
            newObj.GetComponent<BulletMove>().StartDeley();
            return newObj;
        }
    }
    public static void ReturnObject(BulletMove bullet){
        bullet.transform.SetParent(instance.transform);
        bullet.gameObject.SetActive(false);
        instance.pooliongObjectQueue.Enqueue(bullet);
    }
    

}

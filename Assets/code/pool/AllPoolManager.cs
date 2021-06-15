using UnityEngine;
public class AllPoolManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefeb=new GameObject[10];
    private GameObject hellow=null;
    private AllPooler allPooler;
    //------------------------------------------------------------//
    public GameObject GetPool(int findIndex){
        hellow = null;
        for(int i=0;i<transform.childCount;i++){
            allPooler = transform.GetChild(i).GetComponent<AllPooler>();
            if(allPooler.index == findIndex){
                hellow = allPooler.gameObject;
                break;
            }
        }
        if(hellow==null){
            hellow = Instantiate(prefeb[findIndex]);
            hellow.SetActive(false);
        }
        else{
            hellow.transform.SetParent(null);
        }
        return hellow;
    }
    
}

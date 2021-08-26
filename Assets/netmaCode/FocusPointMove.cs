using UnityEngine;

public class FocusPointMove : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(transform.position.x,Camera.main.transform.position.y+5f,transform.position.z);
    }
}

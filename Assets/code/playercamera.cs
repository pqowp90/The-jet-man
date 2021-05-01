using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercamera : MonoBehaviour
{
    private float SHAKEtimeremaining, shakepower, shakefadetime, shakerotation;
    public float smoothTimeX, smoothTimeY, rotationmultiplier = 15f;
    public float zoom, speed, fstzoom;
    private Vector2 velocity;
    public GameObject player;
    public GameObject hihi;
    public bool bound;
    private Vector3 target;
    public Vector2 minPos, maxPos;
    private Vector2 targerPosition;
    void Start()
    {
        Application.targetFrameRate = 60;
        fstzoom = GetComponent<Camera>().orthographicSize;
    }

    void FixedUpdate()
    {
        
        if (hihi != null)
        {
            target = (player.transform.position + hihi.transform.position) / 2;
        }
		
        else target = player.transform.position;
        float posX = Mathf.SmoothDamp(transform.position.x, target.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, target.y, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
        if (bound)
        {
            transform.position = new Vector3(transform.position.x,
                Mathf.Clamp(transform.position.y, minPos.y, maxPos.y),transform.position.z
            );
        }
    }
    void Update()
    {
        if(bound)
            if(transform.position.x>3.5f){
                bound=false;
                hihi = GameObject.Find("startLaser");
                StartCoroutine(StartLaser());
            }
        if (SHAKEtimeremaining > 0)
        {
            SHAKEtimeremaining -= Time.deltaTime;
            float xAmount = Random.Range(-1f, 1f) * shakepower;
            float yAmount = Random.Range(-1f, 1f) * shakepower;

            transform.position += new Vector3(xAmount, yAmount, 0f);
            shakepower = Mathf.MoveTowards(shakepower, 0f, shakefadetime * Time.deltaTime);
            shakerotation = Mathf.MoveTowards(shakerotation, 0f, shakefadetime * Time.deltaTime * rotationmultiplier);
        }
        //transform.rotation = Quaternion.Euler(0f,0f,shakerotation * Random.Range(-1f,1f));
        if (zoom != 0)
        {
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, zoom, speed);
        }
        else GetComponent<Camera>().orthographicSize = fstzoom;

    }
    private IEnumerator StartLaser(){
        yield return new WaitForSeconds(0.1f);
        hihi.GetComponent<Animator>().SetTrigger("barrsa");
    }
    public void startshake(float length, float power)
    {
        SHAKEtimeremaining = length;
        shakepower = power;

        shakefadetime = power / length;

        shakerotation = power * rotationmultiplier;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;
    public GameObject DisconnectPanel;
    public GameObject RespawnPanel;
    public GameObject scoreText;
    int[] score = new int[2];

    private void Awake(){
        Screen.orientation = ScreenOrientation.Landscape;
        //Screen.SetResolution();
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;

    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings();
    
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        PhotonNetwork.JoinOrCreateRoom("Room",new RoomOptions{MaxPlayers = 3},null);
    }
    public override void OnJoinedRoom()
    {
        DisconnectPanel.SetActive(false);
        Spawn();
    }
    void Update(){
        
        if(Input.GetKeyDown(KeyCode.Escape)&&PhotonNetwork.IsConnected){
            PhotonNetwork.Disconnect();
        }
    }
    public void Spawn(){
        PhotonNetwork.Instantiate("playerPrefab",Vector3.zero+new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0f),Quaternion.identity);
        RespawnPanel.SetActive(false);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        
        DisconnectPanel.SetActive(true);
        RespawnPanel.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
    public void Robi(){
        PhotonNetwork.Disconnect();
    }
    public void WinLose(bool hi){
        Time.timeScale = 0.1f;
        RespawnPanel.SetActive(true);
        RespawnPanel.transform.GetChild(0).GetComponent<Text>().text = string.Format("You {0}!",(hi)?"Win":"Lose");
        Invoke("Robi",0.3f);
    }

}

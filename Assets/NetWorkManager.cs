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
        PhotonNetwork.Instantiate("playerPrefab",Vector3.zero,Quaternion.identity);
        RespawnPanel.SetActive(false);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        
        DisconnectPanel.SetActive(true);
        RespawnPanel.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frameㄴ
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title"); //로비에 들어오면 타이틀 메뉴를 켠다.
        Debug.Log("Joined Lobby");
    }

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnCreateRoomFailed(short returnCode, string message) //방만들기 실패시 호출됨
    {
        errorText.text = "Room Createion Failed: " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("asdf");
        MenuManager.Instance.OpenMenu("room");  //룸 메뉴를 연다.
        roomNameText.text = PhotonNetwork.CurrentRoom.Name; //들어간 방 이름
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(); //방 떠나기 : 포톤 네트워크 기능이다.
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title"); //방 떠나기 성공시 타이틀 메뉴 호출
    }
}

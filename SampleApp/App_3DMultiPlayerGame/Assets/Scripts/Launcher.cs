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

    // Update is called once per frame��
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
        MenuManager.Instance.OpenMenu("title"); //�κ� ������ Ÿ��Ʋ �޴��� �Ҵ�.
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

    public override void OnCreateRoomFailed(short returnCode, string message) //�游��� ���н� ȣ���
    {
        errorText.text = "Room Createion Failed: " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("asdf");
        MenuManager.Instance.OpenMenu("room");  //�� �޴��� ����.
        roomNameText.text = PhotonNetwork.CurrentRoom.Name; //�� �� �̸�
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(); //�� ������ : ���� ��Ʈ��ũ ����̴�.
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title"); //�� ������ ������ Ÿ��Ʋ �޴� ȣ��
    }
}

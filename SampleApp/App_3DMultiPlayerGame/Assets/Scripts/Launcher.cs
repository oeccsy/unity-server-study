using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;
    // Start is called before the first frame update

    public static Launcher Instance; //�� ��ũ��Ʈ�� �޼���� ����ϱ� ���� ����

    void Awake()
    {
        Instance = this;
    }
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

        PhotonNetwork.AutomaticallySyncScene = true; //�ڵ����� ��� ������� scene�� ���� �����ش�.
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title"); //�κ� ������ Ÿ��Ʋ �޴��� �Ҵ�.
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
        //���� ��� �̸��� �������� ���ڸ� �ٿ��� �����ش�.
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

        Player[] players = PhotonNetwork.PlayerList;
        for (int i =0; i<players.Count(); i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
            //�濡 ���� �濡 �ִ� ��� ��ϸ�ŭ �̸�ǥ�� �ߵ��� �Ѵ�.
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient); //���常 ���ӽ��� ��ư�� ���� �� �ִ�.
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1); //build�� scene��ȣ 1�� ���Ѵ�.
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
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

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name); //info.Name �̸��� ���� ������ ����
        MenuManager.Instance.OpenMenu("loading"); //�ε�â ����
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)//�氹����ŭ �ݺ�
        {
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
            //instantiate�� prefab�� roomListContent��ġ�� ������ְ� �� �������� i��° �븮��Ʈ�� �ȴ�. 
        }
    }
}

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

    public static Launcher Instance; //이 스크립트를 메서드로 사용하기 위해 선언

    void Awake()
    {
        Instance = this;
    }
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

        PhotonNetwork.AutomaticallySyncScene = true; //자동으로 모든 사람들의 scene을 통일 시켜준다.
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title"); //로비에 들어오면 타이틀 메뉴를 켠다.
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
        //들어온 사람 이름을 랜덤으로 숫자를 붙여서 정해준다.
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

        Player[] players = PhotonNetwork.PlayerList;
        for (int i =0; i<players.Count(); i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
            //방에 들어가면 방에 있는 사람 목록만큼 이름표가 뜨도록 한다.
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient); //방장만 게임시작 버튼을 누를 수 있다.
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1); //build의 scene번호 1을 뜻한다.
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
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

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name); //info.Name 이름을 가진 방으로 접속
        MenuManager.Instance.OpenMenu("loading"); //로딩창 열기
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)//방갯수만큼 반복
        {
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
            //instantiate로 prefab을 roomListContent위치에 만들어주고 그 프리펩은 i번째 룸리스트가 된다. 
        }
    }
}

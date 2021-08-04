# [Photon Pun2 �� �̿��� ��Ƽ���� ����]

��Ƽ������ ���� ������ �� �ֵ��� ���ִ� �����ӿ�ũ�̴�.

## [NetworkManager]

�� ������Ʈ�� NetworkManager ��� ��ũ��Ʈ�� ���� ������ �����ϴ� ����� ������ ���´�.

��� ����) ��ư�� NetworkManager�� �����Ͽ� �ش� ��ư�� ������ ���ϴ� ����� �����ϵ��� �Ѵ�.

## [NetworkManager ��ũ��Ʈ]

```C#
using Photon.Pun;
using Photon.Realtime;
```
* ���� �ΰ����� using �ؾ� �Ѵ�.  

`public class NetworkManager : MonoBehaviourPunCallbacks`

* �Ϲ����� Ŭ������ �ٸ��� MonoBehaviour�� ��ӹ��� �ʰ� MonoBehaviourPunCallbacks�� ��ӹ޴´�.  


`void Awake() => Screen.SetResolution(960, 540, false);`
* (PC����) �����ϱ� ���� ȭ�� ũ�� ����


`void Update() => StatusText.text = PhotonNetwork.NetworkClientState.ToString();`
* ���� ������¸� Text�� ǥ���ϴ� �ڵ�
* ������¸� Update()���� �����Ͽ� �ǽð����� ������¸� Ȯ���� �� �ֵ��� �Ѵ�.

### [���� ����]

```C#
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        print("�������ӿϷ�");
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
    }
```
* ������ �����Ѵ�.
* Connect() �޼��尡 ȣ���� �Ϸ�Ǹ� OnConnectToMaster() �޼��带 �ݹ��Ͽ� ���������� �Ϸ�Ǿ����� ����޴´�.

```C#
    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause) => print("�������");
```
* ������ ������ �����Ѵ�.
* ���� ���� �� OnDisconnected�Լ��� �ݹ��Ͽ� ���������� �������� ����޴´�.

### [�κ�/�� ����]

```C#
    //�κ� ����
    public void JoinLobby() => PhotonNetwork.JoinLobby();

    //�游��� : �� �̸�, �ִ� �÷��̾� ���� �Ű������� �޴´�.
    public void CreateRoom() => PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 });

    //�� ���� : �� �̸��� �Ű������� �޴´�.
    public void JoinRoom() => PhotonNetwork.JoinRoom(roomInput.text);
    
    //roomInput.text�� �ش��ϴ� ���� ���� ��� ����, ������� �����
    public void JoinOrCreateRoom() => PhotonNetwork.JoinOrCreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 }, null);
    
    //���� �� ����
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    //�� ������
    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    //�ݹ� �Լ���
    public override void OnJoinedLobby() => print("�κ����ӿϷ�");
    public override void OnCreatedRoom() => print("�游���Ϸ�");
    public override void OnJoinedRoom() => print("�������Ϸ�");
    public override void OnCreateRoomFailed(short returnCode, string message) => print("�游������");
    public override void OnJoinRoomFailed(short returnCode, string message) => print("����������");
    public override void OnJoinRandomFailed(short returnCode, string message) => print("�淣����������");
```
* ���� ����⳪ ������ ConnectedToMaster �Ǵ� JoinedLobby �Ǿ������� �����ϴ�.



### [PV.RPC] [ä�ù� ����]

#### [PV.RPC]
`PV.RPC(string methodName, RPCtarget target, params object[] parameters);`  
`PV.RPC("ChatRPC", RpcTarget.All, "<color=yellow>" + newPlayer.NickName + "���� �����ϼ̽��ϴ�.</color>");`
* �ݵ�� Room�� �־�� ȣ�� ������ �Լ��̴�.
* �濡 �ִ� �÷��̾�� �޼��带 ȣ���ϵ��� �����Ѵ�.

#### [ä�ù� ����]
PV.RPC�� ��� ���ø� �����ϱ� ���� ä�ù� ������ �����Ѵ�.

ä�ù� UI
![Image](../SampleApp/App_Photon_1/Picture/ChattingRoomUI.png)
* ScrollView ���ο� �������� Text�� ��ġ�Ѵ�.
* Input Field �� Button�� ��ġ�Ͽ� �Է°� ���ۿ� �̿��ϵ��� �Ѵ�.

ä��â�� ���� �����Ҷ����� �����ϰ� ����ֵ��� �Ѵ�.  
�濡 �÷��̾ ����/���� �ϴ� ���, ���ο� �濡 �����ϰ� �Ǵ� ��� �� ������ �����Ѵ�.

```C#
    public override void OnJoinedRoom()     //�濡 ���������� ȣ��ȴ�.
    {
        //ȭ����ȯ
        RoomPanel.SetActive(true);
        LobbyPanel.SetActive(false);

        //�� ����, ä��â ����
        RoomRenewal();
        ChatInput.text = "";
        for (int i = 0; i < ChatText.Length; i++) ChatText[i].text = "";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)      //�濡 �÷��̾ ���������� ȣ��ȴ�.
    {
        RoomRenewal();
        PV.RPC("ChatRPC", RpcTarget.All, "<color=yellow>" + newPlayer.NickName + "���� ����</color>");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)       //�濡 �÷��̾ ���������� ȣ��ȴ�.
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "���� �����ϼ̽��ϴ�</color>");
    }

    void RoomRenewal()  //���� �����ϴ� �Լ� : �������� ����� �����Ѵ�.
    {
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "�� / " + PhotonNetwork.CurrentRoom.MaxPlayers + "�ִ�";
    }
 ```




### [����ũ]
* https://www.youtube.com/watch?v=mPCNTi3Booo&list=PL3KKSXoBRRW3YE4UMnRH762vOhSHLdnpK&index=1&t=2s
* https://www.youtube.com/watch?v=a6MquH2NPRE&list=PL3KKSXoBRRW3YE4UMnRH762vOhSHLdnpK&index=2
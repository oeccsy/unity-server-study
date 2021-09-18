using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro; 
public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text text;
    Player player; //���� ����Ÿ���� Player

    public void SetUp(Player _player)
    {
        player = _player;
        text.text = _player.NickName; //�÷��̾� �̸��� �޾Ƽ� ��쵵�� ��
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) //�÷��̾ ���� ������ �� ȣ���Ѵ�.
    {
        if(player == otherPlayer) // ���� ohterPlayer�� �����ΰ��
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
   
}

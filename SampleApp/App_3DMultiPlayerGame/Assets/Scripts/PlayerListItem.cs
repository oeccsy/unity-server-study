using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro; 
public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text text;
    Player player; //포톤 리얼타임의 Player

    public void SetUp(Player _player)
    {
        player = _player;
        text.text = _player.NickName; //플레이어 이름을 받아서 띄우도록 함
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) //플레이어가 방을 떠났을 때 호출한다.
    {
        if(player == otherPlayer) // 나간 ohterPlayer가 본인인경우
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
   
}

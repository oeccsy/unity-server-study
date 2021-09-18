using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    RoomInfo info; //포톤 리얼타임의 방정보 기능

    public void SetUp(RoomInfo _info) //방정보를 받아온다.
    {
        info = _info;
        text.text = _info.Name;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info); //런처 스크립트로 메서드로 JoinRoom 실행
    }
}

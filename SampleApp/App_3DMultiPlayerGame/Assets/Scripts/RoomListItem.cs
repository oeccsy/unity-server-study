using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    RoomInfo info; //���� ����Ÿ���� ������ ���

    public void SetUp(RoomInfo _info) //�������� �޾ƿ´�.
    {
        info = _info;
        text.text = _info.Name;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info); //��ó ��ũ��Ʈ�� �޼���� JoinRoom ����
    }
}

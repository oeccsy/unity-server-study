using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO; //path 사용을 위함
public class PlayerManager : MonoBehaviour
{
    PhotonView PV; //포톤의 포톤뷰

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if(PV.IsMine) //본인의 포톤 네트워크라면
        {
            CreateController(); //플레이어 컨트롤러를 붙여준다.
        }
    }

    void CreateController()
    {
        Debug.Log("Create Controller");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
    }
}

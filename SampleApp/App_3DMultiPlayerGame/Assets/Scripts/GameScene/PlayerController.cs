using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using static GameController;//게임 컨트롤러 사용

public class PlayerController : MonoBehaviour
{
    public bool isBoss;
    PhotonView PV;
     PlayerManager playerManager;
     public static PlayerController playerController;
    void Awake()
    {
        isBoss = false;//초기 술래끄기
        //playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
        //플레이어 매니저 정의 PlayerManager에서 생성한 인스턴스를 찾게된다.  
        playerController = GetComponent<PlayerController>();
        //플레이어 컨트롤러를 가지도록 해주자
        GC.Players.Add(this);
        //게임컨트롤러 목록에 이놈들을 추가
        Debug.Log("Add complete");
        //추가완료
    }

    [PunRPC]
    void SetBoss(bool _isBoss)
    {
        //술래를 정해주는 RPC
        isBoss = _isBoss;
        Debug.Log("Boss " + isBoss);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO; //Path 사용

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance; //RoomManager를 메서드로 사용하기 위함

    void Awake()
    {
        if(Instance) //다른 룸메니저의 존재를 확인한다.
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
        // 활성화되면 씬 매니저의 OnSceneLoaded에 체인을 건다.
        // 씬이 바뀔때마다 작동됨
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // 비활성화되면 씬 매니저의 체인을 지운다.
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode load)
    {
        if(scene.buildIndex == 1) //scene의 buildIndex가 1이면 (게임씬이면)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
            //포톤 Prefabs에 있는 플레이어 매니저를 해당 position, rotation으로 생성
            //Path.Combine("PhotonPrefabs", "PlayerManager") 는 Resources폴더 하위에 있는 프리팹 경로

        }
    }
}

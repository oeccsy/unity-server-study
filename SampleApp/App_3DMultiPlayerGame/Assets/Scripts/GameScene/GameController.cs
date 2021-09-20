using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameController : MonoBehaviour
{
    public int whichPlayerIsBoss;
    public static GameController GC;
    public List<PlayerController> Players = new List<PlayerController>();

    void Awake()
    {
        GC = this;
    }
    
    IEnumerator ExampleCoroutine() //지연시간 후 술래 설정
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(3);
        Debug.Log("Finish Coroutine at timestamp : " + Time.time);

        if(PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Start Pick Boss");

            PickBoss();
        }
    }
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    void PickBoss()
    {
        List<PlayerController> PlayerList = new List<PlayerController>(Players);

        whichPlayerIsBoss = Random.Range(0, PlayerList.Count);

        Debug.Log("We have " + PlayerList.Count);

        Debug.Log("Boss Number is " + whichPlayerIsBoss);

        //PlayerList[whichPlayerIsBoss].GetComponent<PhotonView>().RPC("SetBoss", RpcTarget.All, true);
    }
}

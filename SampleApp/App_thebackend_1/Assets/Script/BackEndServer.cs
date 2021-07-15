using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using BackEnd.Tcp;
using UnityEngine.UI;
public class BackEndServer : MonoBehaviour
{
    public List<MatchInfo> matchInfos { get; private set; } = new List<MatchInfo>();
    public class MatchInfo
    {
        public MatchType matchType;

    }
    public Text text1;
    public string myNickName { get; private set; } = string.Empty;

    // 디버깅용
    private string FAIL_CONNECT_MATCHSERVER = "매치 서버 접속 실패 : {0}";
    ErrorInfo errorInfo;

    public bool isConnectMatchServer = false;

    private void Awake()
    {
        Backend.InitializeAsync(true, callback =>
        {
            if (callback.IsSuccess())
            {
                //초기화 성공
                Debug.Log("성공");
                text1.text = "Initialize";
            }
            else
            {
                //초기화 실패
                Debug.Log("실패");
            }
        });
    }
    // Update is called once per frame

    public void G_Login()
    {
        BackendReturnObject bro = Backend.BMember.GuestLogin("게스트 로그인으로 로그인함");
        if (bro.IsSuccess())
        {
            Debug.Log("게스트 로그인에 성공했습니다");
            text1.text = "로그인";
        }

    }
    public void JoinMatchServer()
    {
        if (isConnectMatchServer)
        {
            return;
        }

        isConnectMatchServer = true;
        if (!Backend.Match.JoinMatchMakingServer(out errorInfo))
        {
            var errorLog = string.Format(FAIL_CONNECT_MATCHSERVER, errorInfo.ToString());
            Debug.Log(errorLog);
        }
        else
        {
            Debug.Log("매치서버 접속 성공");
            text1.text = "MatchMakingServer";
        }

    }

    public void CreateRoom()
    {
        Backend.Match.CreateMatchRoom();
        text1.text = "Room";
    }

    public void Match()
    {
        Backend.Match.RequestMatchMaking(MatchType.MMR, MatchModeType.Melee, "a");
    }

    private void ProcessMatchMakingResponse(MatchMakingResponseEventArgs args)
    {
        switch (args.ErrInfo)
        {
            case ErrorCode.Success:
                // 매칭 성공했을 때
                Debug.Log("매칭 성공!");
                ProcessMatchSuccess(args);
                break;
            case ErrorCode.Match_InProgress:
                // 매칭 신청 성공했을 때 or 매칭 중일 때 매칭 신청을 시도했을 때

                // 매칭 신청 성공했을 때
                if (args.Reason == string.Empty)
                {
                    Debug.Log("매칭중!");
                }
                break;
            case ErrorCode.Match_MatchMakingCanceled:
                // 매칭 신청이 취소되었을 때
                Debug.Log("매칭 취소됨");
                break;
            case ErrorCode.Match_InvalidMatchType:
                // 매치 타입을 잘못 전송했을 때
                Debug.Log("매치 타입 잘못 전송됨");
                break;
            case ErrorCode.Match_InvalidModeType:
                Debug.Log("매치 모드 잘못 전송됨");
                break;
            case ErrorCode.InvalidOperation:
                // 잘못된 요청을 전송했을 때
                Debug.Log("요청 잘못됨");
                break;
            case ErrorCode.Match_Making_InvalidRoom:
                // 잘못된 요청을 전송했을 때
                Debug.Log("요청 잘못됨");
                break;
            case ErrorCode.Exception:
                // 매칭 되고, 서버에서 방 생성할 때 에러 발생 시 exception이 리턴됨
                // 이 경우 다시 매칭 신청해야 됨
                Debug.Log("에러 발생, 다시 매칭하세요");
                break;
        }


    }

    // 매칭 성공했을 때
    // 인게임 서버로 접속해야 한다.
    private void ProcessMatchSuccess(MatchMakingResponseEventArgs args)
    {
        ErrorInfo errorInfo;

        if (!Backend.Match.JoinGameServer(args.RoomInfo.m_inGameServerEndPoint.m_address, args.RoomInfo.m_inGameServerEndPoint.m_port, false, out errorInfo))
        {
            Debug.Log("게임 입장");
        }
        // 인자값에서 인게임 룸토큰을 저장해두어야 한다.
        // 인게임 서버에서 룸에 접속할 때 필요
        // 1분 내에 모든 유저가 룸에 접속하지 않으면 해당 룸은 파기된다.
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            G_Login();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            JoinMatchServer();
            
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateRoom();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Match();
            Debug.Log("매칭중. .");
            text1.text = "매칭중";
        }
    }



}

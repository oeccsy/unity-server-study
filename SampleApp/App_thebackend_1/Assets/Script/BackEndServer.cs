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

    // ������
    private string FAIL_CONNECT_MATCHSERVER = "��ġ ���� ���� ���� : {0}";
    ErrorInfo errorInfo;

    public bool isConnectMatchServer = false;

    private void Awake()
    {
        Backend.InitializeAsync(true, callback =>
        {
            if (callback.IsSuccess())
            {
                //�ʱ�ȭ ����
                Debug.Log("����");
                text1.text = "Initialize";
            }
            else
            {
                //�ʱ�ȭ ����
                Debug.Log("����");
            }
        });
    }
    // Update is called once per frame

    public void G_Login()
    {
        BackendReturnObject bro = Backend.BMember.GuestLogin("�Խ�Ʈ �α������� �α�����");
        if (bro.IsSuccess())
        {
            Debug.Log("�Խ�Ʈ �α��ο� �����߽��ϴ�");
            text1.text = "�α���";
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
            Debug.Log("��ġ���� ���� ����");
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
                // ��Ī �������� ��
                Debug.Log("��Ī ����!");
                ProcessMatchSuccess(args);
                break;
            case ErrorCode.Match_InProgress:
                // ��Ī ��û �������� �� or ��Ī ���� �� ��Ī ��û�� �õ����� ��

                // ��Ī ��û �������� ��
                if (args.Reason == string.Empty)
                {
                    Debug.Log("��Ī��!");
                }
                break;
            case ErrorCode.Match_MatchMakingCanceled:
                // ��Ī ��û�� ��ҵǾ��� ��
                Debug.Log("��Ī ��ҵ�");
                break;
            case ErrorCode.Match_InvalidMatchType:
                // ��ġ Ÿ���� �߸� �������� ��
                Debug.Log("��ġ Ÿ�� �߸� ���۵�");
                break;
            case ErrorCode.Match_InvalidModeType:
                Debug.Log("��ġ ��� �߸� ���۵�");
                break;
            case ErrorCode.InvalidOperation:
                // �߸��� ��û�� �������� ��
                Debug.Log("��û �߸���");
                break;
            case ErrorCode.Match_Making_InvalidRoom:
                // �߸��� ��û�� �������� ��
                Debug.Log("��û �߸���");
                break;
            case ErrorCode.Exception:
                // ��Ī �ǰ�, �������� �� ������ �� ���� �߻� �� exception�� ���ϵ�
                // �� ��� �ٽ� ��Ī ��û�ؾ� ��
                Debug.Log("���� �߻�, �ٽ� ��Ī�ϼ���");
                break;
        }


    }

    // ��Ī �������� ��
    // �ΰ��� ������ �����ؾ� �Ѵ�.
    private void ProcessMatchSuccess(MatchMakingResponseEventArgs args)
    {
        ErrorInfo errorInfo;

        if (!Backend.Match.JoinGameServer(args.RoomInfo.m_inGameServerEndPoint.m_address, args.RoomInfo.m_inGameServerEndPoint.m_port, false, out errorInfo))
        {
            Debug.Log("���� ����");
        }
        // ���ڰ����� �ΰ��� ����ū�� �����صξ�� �Ѵ�.
        // �ΰ��� �������� �뿡 ������ �� �ʿ�
        // 1�� ���� ��� ������ �뿡 �������� ������ �ش� ���� �ı�ȴ�.
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
            Debug.Log("��Ī��. .");
            text1.text = "��Ī��";
        }
    }



}

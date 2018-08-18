using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{

   //private const string ip = "127.0.0.1";
    private const string ip = "172.20.10.5";
    private const int port = 30000;
    private bool _useNat = false;

    public GameObject player;

    void OnGUI()
    {
        //// 현재 사용자의 네트워크에 접속 여부 판단
        //if (Network.peerType == NetworkPeerType.Disconnected)
        //{
        //    // 게임 서버 생성 버튼
        //    if (GUI.Button(new Rect(20, 20, 200, 25), "Start Server"))
        //    {
        //        // 게임 서버 생성 : InitalizeSever(접속자수, 포트번호 , NAT사용여부)
        //        // Nat :  network address translation :: 사설 ip(공유기같은)-> 공인 ip로 변환
        //        Network.InitializeServer(20, port, _useNat);
        //    }

        //    // 게임에 접속하는 버튼
        //    if (GUI.Button(new Rect(20, 50, 200, 25), "Connect to Server"))
        //    {
        //        // 게임 서버 접속 : Connect(접속ip, 접속포트번호)
        //        Network.Connect(ip, port);
        //    }
        //}
        //else
        //{
        //    // 서버 일때 메세지 출력
        //    if (Network.peerType == NetworkPeerType.Server)
        //    {
        //        GUI.Label(new Rect(20, 20, 200, 25), "Initialization Server...");
        //        GUI.Label(new Rect(20, 50, 200, 25), "Clinet Count = " + Network.connections.Length.ToString());
        //    }
        //    // 클라이언트로 접속했을 때의 메세지 출력
        //    if (Network.peerType == NetworkPeerType.Client)
        //    {
        //        GUI.Label(new Rect(20, 20, 200, 25), "Connected to Server");
        //    }
        //}
    }

    void Start()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            Network.Connect(ip, port);
        }
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            Network.InitializeServer(20, port, _useNat);
        }
    }

    // 게임 서버로 구동시키고 서버 초기화가 정상적으로 완료됐을 때 호출 됨
    void OnServerInitialized()
    {
        StartCoroutine(this.CreatePlayer());
    }

    // 클라이언트로 게임 서버에 접속 했을 대 호출 됨.
    void OnConnectedToServer()
    {
        StartCoroutine(this.CreatePlayer());
    }

    IEnumerator CreatePlayer()
    {
        Vector3 pos = new Vector3(0,0,0);
      
         //   pos = new Vector3(10, 15, 0);
        
        
        

        // 네트워크 상에 플레이어를 동적 생성
        Network.Instantiate(player, pos, Quaternion.identity, 0);
        yield return null;
    }

    void OnPlayerDisconnected(NetworkPlayer netPlayer)
    {
        Network.RemoveRPCs(netPlayer);
        Network.DestroyPlayerObjects(netPlayer);
    }
}

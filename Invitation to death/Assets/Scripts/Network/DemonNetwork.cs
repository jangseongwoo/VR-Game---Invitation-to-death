using UnityEngine;
using System.Collections;

public class DemonNetwork : MonoBehaviour {

    private float mPlayerRotSpeed = 150.0f;
    private NetworkView mNetworkView;
    private Transform transform;


    public enum AnimState
    {
        idle, trace, attack, die, gothit
    }
    public AnimState animState = AnimState.idle;
    public AnimationClip[] animClips;

    private Transform tr;
    private NetworkView _networkView;
    private CharacterController _controller;
    private Animation _animation;

    public int devilHp;
    // 위치 정보 송수신 변수 선언
    private Vector3 currPos = Vector3.zero;
    private Quaternion currRot = Quaternion.identity;

    public GameObject bullet;
    public Transform firePos;

    private bool isDie = false;
   // private int hp = 100;
    private float respawnTime = 3.0f;
    int anim = 0;


    // Use this for initialization
    void Start()
    {
        transform = GetComponent<Transform>();
        mNetworkView = GetComponent<NetworkView>();
        mNetworkView.observed = this;
        devilHp = GetComponent<DevilCtrl>().devilHp;
        anim = (int)GetComponent<DevilCtrl>().monsterState;
        //if (mNetworkView.isMine)
        currPos = transform.position;
        //else
        //    this.gameObject.transform.parent = GameObject.Find("CoalTrain").transform;

     
    }


    // Update is called once per frame
    void Update()
    {
        if (Network.peerType == NetworkPeerType.Server)
        {

        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, currPos, Time.deltaTime * 10.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, currRot, Time.deltaTime * 10.0f);
            _animation.CrossFade(animClips[(int)anim].name, 0.2f);
        }
       // _animation = (int)anim;

    }




    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            if (Network.peerType == NetworkPeerType.Server)
            {
                Vector3 pos = transform.position;
                Quaternion rot = transform.rotation;
                int _animState = (int)animState;

                stream.Serialize(ref pos);
                stream.Serialize(ref rot);
                stream.Serialize(ref devilHp);
                stream.Serialize(ref _animState);
            }

        }
        else
        {
            if (Network.peerType != NetworkPeerType.Server)
            {
                Vector3 revPos = Vector3.zero;
                Quaternion revRot = Quaternion.identity;
               // int _animState = 0;

                stream.Serialize(ref revPos);
                stream.Serialize(ref revRot);
                stream.Serialize(ref devilHp);
                stream.Serialize(ref anim);

                currPos = revPos;
                currRot = revRot;
                animState = (AnimState)anim;
            }
        }
    }
}

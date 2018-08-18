using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour
{
    private float mPlayerRotSpeed = 150.0f;
    private NetworkView mNetworkView;
    private Transform transform;


    public enum AnimState
    {
        idle = 0,
        runForward,
        runBackward,
        runRight,
        runLeft
    }
    public AnimState animState = AnimState.idle;
    public AnimationClip[] animClips;

    private Transform tr;
    private NetworkView _networkView;
    private CharacterController _controller;
    private Animation _animation;

    // 위치 정보 송수신 변수 선언
    private Vector3 currPos = Vector3.zero;
    private Quaternion currRot = Quaternion.identity;

    public GameObject bullet;
    public Transform firePos;

    private bool isDie = false;
    private int hp = 100;
    private float respawnTime = 3.0f;


    // Use this for initialization
    void Start()
    {
        transform = GetComponent<Transform>();
        mNetworkView = GetComponent<NetworkView>();
        mNetworkView.observed = this;

        //if (mNetworkView.isMine)
        //this.gameObject.transform.parent = GameObject.Find("RotX").transform;
        //else
        //    this.gameObject.transform.parent = GameObject.Find("CoalTrain").transform;

       // transform.localPosition = Vector3.zero;
     //   transform.localRotation =  GameObject.Find("Capsule-prev").transform.rotation;
    }


    // Update is called once per frame
    void Update()
    {
        if (mNetworkView.isMine)
        {


            //if (Input.GetMouseButton(0))
            //{
            //    Fire();
            //    _networkView.RPC("Fire", RPCMode.Others);
            //}

            if (Input.GetMouseButtonDown(0))
            {
                //총알 발사.
                Fire();
                mNetworkView.RPC("Fire", RPCMode.Others);
                //  audio.clip = bulletSound;
                //  audio.Play();

            }
            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
                mNetworkView.RPC("Fire", RPCMode.Others);
            }
            transform.Rotate(Vector3.left * Time.deltaTime * mPlayerRotSpeed * Input.GetAxis("Mouse Y"));
          //  transform.Rotate(Vector3.up * Time.deltaTime * mPlayerRotSpeed * Input.GetAxis("Mouse X"));
        }
        else
        {
            // transform.position = currPos;
            transform.rotation = Quaternion.Slerp(transform.rotation, currRot, Time.deltaTime * 10.0f);
        }


    }




    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            //  Vector3 pos = transform.position;
            Quaternion rot = transform.rotation;
            //   int _animState = (int)animState;

            //   stream.Serialize(ref pos);
            stream.Serialize(ref rot);
            //   stream.Serialize(ref _animState);

        }
        else
        {
            //   Vector3 revPos = Vector3.zero;
            Quaternion revRot = Quaternion.identity;
            //  int _animState = 0;

            //    stream.Serialize(ref revPos);
            stream.Serialize(ref revRot);
            //  stream.Serialize(ref _animState);

            //     currPos = revPos;
            currRot = revRot;
            //   animState = (AnimState)_animState;
        }
    }



    [RPC]
    void Fire()
    {
        StartCoroutine(this.CreateBullet());
    }
    IEnumerator CreateBullet()
    {
        GameObject.Instantiate(bullet, firePos.position, firePos.rotation);
        yield return null;
    }
}

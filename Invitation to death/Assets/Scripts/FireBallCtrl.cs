using UnityEngine;
using System.Collections;

public class FireBallCtrl : MonoBehaviour {

    //총알의 파괴력.
    public int damage = 20;

    //총알의 생명 시간.
    private float LIFETime = 2.0f;


    //테스트.
    private Transform bossTr;
    private Transform playerTr;

    void Awake()
    {
        //총알을 Forward 방향으로 힘을 가함.
        //GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1500.0f);
        
        /*
        bossTr = GameObject.FindWithTag("BOSS").GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        GetComponent<Rigidbody>().AddRelativeForce(
            //(
            //(new Vector3(playerTr.position.x, playerTr.position.y, playerTr.position.z) - transform.position)).normalized * 1500.0f
            (playerTr.position - transform.position).normalized * 1500.0f
            //(playerTr.position - GameObject.Find("FirePos").transform.position).normalized * 1500.0f

            );
        */
        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 1) * 100000.0f * Time.deltaTime);

        //일정 시간 지난 후 제거.
        Destroy(gameObject, LIFETime);
    }
    void Update()
    {
        bossTr = GameObject.FindWithTag("BOSS").GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }
}

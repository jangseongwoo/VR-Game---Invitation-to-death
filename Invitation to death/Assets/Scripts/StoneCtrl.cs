using UnityEngine;
using System.Collections;

public class StoneCtrl : MonoBehaviour {

    public int mStoneHP = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {

        //충돌한 게임오브젝트의 태그값 비교.
        if (coll.GetComponent<Collider>().tag == "BULLET")
        {
            //혈흔 효과 코루틴 함수 호출.
            //StartCoroutine(this.CreateBloodEffect(coll.transform.position));

            //맞은 총알의 Damage를 추출해 몬스터 hp 차감.
            mStoneHP -= coll.gameObject.GetComponent<BulletFire>().damage;
            if (mStoneHP <= 0)
            {
                //MonsterDie();
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1) * 2000);
                GameObject.FindGameObjectWithTag("WALL").GetComponent<Collider>().enabled = false;
            }

            //충돌한 게임 오브젝트 삭제.
            Destroy(coll.gameObject);


            //IsHit Trigger를 발생시키면 Any State에서 gothit로 전이됨.
            //_animator.SetTrigger("IsHit");
        }
    }
}

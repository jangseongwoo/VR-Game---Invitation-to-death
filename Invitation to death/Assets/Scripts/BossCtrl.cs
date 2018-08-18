using UnityEngine;
using System.Collections;

public class BossCtrl : MonoBehaviour {
    //혈흔 효과 프리팹.
    public GameObject bloodEffect;
    //보스 생명 변수.
    public int bossHp = 400;
    //공격 사정거리.
    public float attackDist = 100.0f;
    //보스의 사망 여부.
    public bool isBossDie = false;

    private int explosionCheck = 0;


    //GuiManager에 접근하기 위한 변수.
    private GuiManager guiManager;

    //Attack 전용 
    public AudioClip fireSound;
    //fireBall 프리팹 할당.
    public GameObject fireBall;
    public GameObject explosion;
    //총알 발사 위치.
    public Transform firePos;
    public Transform explosionPos;

    //보스의 상태 정보가 있는 Enumerable 변수 선언.
    public enum BossState { spawn, idle, attack1, attack2, attack2_2, die };
    //몬스터의 현재 상태 정보를 저장할 Enum변수.
    public BossState bossState = BossState.spawn;

    //속도 향상을 위해 각종 컴포넌트를 변수에 할당.
    private Transform bossTr;
    private Transform playerTr;

    //애니메이터 컴포넌트에 접근하기 위한 변수 선언.
    private Animator _bossAnimator;


    void Awake()
    {
        //일정한 간격으로 몬스터의 행동 상태를 체크하는 코루틴 함수 실행.
        StartCoroutine(this.CheckBossState());
        //몬스터의 상태에 따라 동작하는 루틴을 실행하는 코루틴 함수 실행.
        StartCoroutine(this.BossAction());
    }

    void Start()
    {
        //몬스터의 Transform 할당.
        bossTr = this.gameObject.GetComponent<Transform>();
        //추적 대상인 Player의 Transform 할당.
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //Animator 컴포넌트 할당.
        _bossAnimator = this.gameObject.GetComponent<Animator>();

        //GuiManager 게임 오브젝트의 GuiManager 스크립트를 할당.
        guiManager = GameObject.Find("GUIManager").GetComponent<GuiManager>();
    }

    void MoveUpdate()
    {
        transform.rotation = Quaternion.LookRotation(
    new Vector3(
    playerTr.position.x, bossTr.position.y, playerTr.position.z)
                - transform.position);

        //bossTr.Translate(Vector3.forward * 1 * Time.deltaTime);

        //if (bossTr.position.y < playerTr.position.y)
        //    bossTr.Translate(Vector3.up * 1 * Time.deltaTime);
        //else if (playerTr.position.y < bossTr.position.y)
        //    bossTr.Translate(-Vector3.up * 1 * Time.deltaTime);
    }

    /*
     * 일정한 간격으로 몬스터의 할동 상태를 체크하고 monsterState값 변경.
     */

    IEnumerator CheckBossState()
    {
        while (!isBossDie)
        {

            //Fire();

            yield return new WaitForSeconds(0.2f);
            //몬스터의 플레이어 사이의 거리 측정.
            float dist = Vector3.Distance(playerTr.position, bossTr.position);

            //Debug.Log("거리 : " + dist.ToString());

            //Debug.Log(_bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack2-2").ToString());

            if (dist <= 100.0f) //공격 범위 이내로 들어왔는지 확인.
            {
                bossState = BossState.attack2;
            }
        }
    }

    /*
     * 몬스터 상태값에 따라 적절한 동작을 수행하는 함수.
     */

    IEnumerator BossAction()
    {
        while (!isBossDie)
        {
            switch (bossState)
            {
                case BossState.idle:
                    //추적 중지.

                    //Animator의 IsAttack 변수를 false로 설정.
                    //_bossAnimator.SetBool("IsAttack", false);

                    //Animator의 IsTrace 변수를 false로 설정.
                    //_bossAnimator.SetBool("IsTrace", false);

                    break;

                //공격 상태.
                case BossState.attack1:
                    //추적 중지.

                    //IsAttack을 true로 설정해 attack State로 전이.
                    _bossAnimator.SetTrigger("attack1");
                    //_bossAnimator.SetBool("attack12", true);
                    Fire();
                    break;

                case BossState.attack2:
                    //추적 중지.

                    //IsAttack을 true로 설정해 attack State로 전이.
                    _bossAnimator.SetTrigger("attack2");
                    bossState = BossState.attack2_2;

                    if (_bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack2-1"))
                        Fire();

                    //Fire();
                    break;

                case BossState.attack2_2:

                    if (_bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack2-2") && explosionCheck == 0)
                    {
                        FireAtPlayerPosition();
                        explosionCheck++;
                    }
                    if (_bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack2-2") == false)
                        explosionCheck = 0;
                    //bossState = BossState.idle;
                    break;

            }

            yield return null;
        }
    }

    void Fire()
    {
        //총알 생성 루틴은 코루틴을 활용.
        //StartCoroutine(this.CreateBullet());
        GameObject.Instantiate(fireBall, firePos.position, firePos.rotation);
    }

    void FireAtPlayerPosition()
    {
        GameObject.Instantiate(explosion, explosionPos.position, explosionPos.rotation);
        guiManager.DispScore(-30);
    }

    //총알 생성 코루틴 함수.
    IEnumerator CreateBullet()
    {
        GameObject.Instantiate(fireBall, firePos.position, firePos.rotation);
        yield return null;
    }



    void OnTriggerEnter(Collider coll)
    {

        //충돌한 게임오브젝트의 태그값 비교.
        if (coll.GetComponent<Collider>().tag == "BULLET")
        {
            //혈흔 효과 코루틴 함수 호출.
            StartCoroutine(this.CreateBossBloodEffect(coll.transform.position));

            //맞은 총알의 Damage를 추출해 몬스터 hp 차감.
            bossHp -= coll.gameObject.GetComponent<BulletFire>().damage;
            if (bossHp <= 0)
            {
                BossDie();
            }

            //충돌한 게임 오브젝트 삭제.
            Destroy(coll.gameObject);

            //IsHit Trigger를 발생시키면 Any State에서 gothit로 전이됨.
            //_animator.SetTrigger("IsHit");
        }
    }

    //몬스터 사망 시 처리 루틴.
    void BossDie()
    {
        //모든 코루틴을 정지시킨다.
        StopAllCoroutines();

        isBossDie = true;

        bossState = BossState.die;

        _bossAnimator.SetTrigger("isDie");

        //몬스터에 추가된 Collider를 비활성화.
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;



        //일단 죽으면 사라지도록 처리.

        Destroy(gameObject, 4.0f);


        //데미지 입히는 양 팔의 콜라이더 비활성화 코드 같음.
        //foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        //{
        //    coll.enabled = false;
        //}

        //GameUI의 스코어 누적과 스코어 표시 함수 호출.
        //gameUI.DispScore(50);

        //몬스터 오브젝트 풀로 환원시키는 코루틴 함수 호출.
        //StartCoroutine(this.PushObjectPool());

    }

    IEnumerator CreateBossBloodEffect(Vector3 pos)
    {
        //혈흔 효과 발생.
        GameObject _blood1 = (GameObject)Instantiate(bloodEffect, pos, Quaternion.identity);
        Destroy(_blood1, 2.0f);

        yield return null;
    }

    void Update()
    {
        MoveUpdate();

        //_bossAnimator.SetTrigger("attack1");
    }
}

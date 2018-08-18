using UnityEngine;
using System.Collections;

public class CapsuleCtrl : MonoBehaviour
{
    public float fixedVelocity;
    private float fixedAccelSpeed = 2500;

    // 사운드 클립
    public AudioClip up;
    public AudioClip downFast;

    //단위 벡터.
    private Vector3 standardX = new Vector3(1, 0, 0);
    private Vector3 standardY = new Vector3(0, 1, 0);
    private Vector3 standardZ = new Vector3(0, 0, 1);

    //캡슐의 위치를 저장할 변수.
    private Transform tr;
    //delay (0.1초)전의 위치를 저장할 변수.
    private Vector3 prevTr;

    //이전 회전각도(스크립트로 구한)값을 저장할 변수.
    private float prevPitch;
    private float prevYaw;
    private float prevRoll;

    //360도 회전 레일에서 균형 잡는 것 반전 구현 위한 변수.
    public bool isReverse = false;

    //z축 기준 양의 방향으로 가고있는지 음의 방향으로 가고있는지 판단하기 위한 변수.
    public bool isForwardDirection;


    //두 포지션 사이의 거리를 저장할 변수.
    private float dist;
    private float distForYAxis;

    //균형을 잡기 위한 양 날개.
    private Transform leftWing;
    private Transform rightWing;

    //지속적인 Addforce bool 변수.
    public bool addForce;
    public float addForceSpeed = 2200.0f;

    //이동 방향과 객차의 forward 방향 벡터 사이의 각도를 저장할 변수.
    private float betweenAngle;
    //히브 값(y축 포지션 값만을 통한 속도 값))을 저장할 변수.
    public float heaveVelocity;
    //떨어지는 지를 체크하여 히브 값을 계산할지 판단하기 위한 불값.
    private bool isFalling = false;



    //
    public float changeValueX, changeValueY, changeValueZ;
    //이전의 x,y,z Rotation값을 저장할 변수.
    public float prevX, prevY, prevZ;

    public float delay = 0.2f;
    private float nextSave = 0.0f;

    //객차 액셀, 브레이크 속도.
    private float speed = 2200.0f;
    public bool rotatingRailLeft = false;
    public bool rotatingRailRight = false;

    //객차 속도
    public float velocity;

    //액셀, 브레이크 입력 값 받을 변수.
    public float throttle;

    //지속적으로 가해줄 힘의 세기
    public float AutoAccelSpeed = 800.0f;
    public float AutoBackAccelSpeed = 0.0f;

    //GuiManager에 접근하기 위한 변수.
    private GuiManager guiManager;

    //슬로우 모션
    public bool isSlowMotion = false;

    //gun
    public GameObject gun;

    //보스
    public GameObject Boss;

    void Start()
    {

    }

    void Awake()
    {
        tr = GetComponent<Transform>();
        rightWing = GameObject.FindGameObjectWithTag("RIGHTWING").transform;
        leftWing = GameObject.FindGameObjectWithTag("LEFTWING").transform;

        //GuiManager 게임 오브젝트의 GuiManager 스크립트를 할당.
        guiManager = GameObject.Find("GUIManager").GetComponent<GuiManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (false == isSlowMotion)
        {
            Time.timeScale = 1f;
        }
        if (true == isSlowMotion)
        {
            Time.timeScale = 0.3f;
        }

        //속도 조절 구간.
        //tr.GetComponent<Rigidbody>().AddForce(tr.transform.up * AutoAccelSpeed * Time.deltaTime);
        //tr.GetComponent<Rigidbody>().AddForce(tr.transform.up * -AutoBackAccelSpeed * Time.deltaTime);

        if (velocity < fixedVelocity - 5)
            fixedAccelSpeed = 1500;// * Time.deltaTime; //원래 5000
        else if (fixedVelocity + 5 < velocity)
            fixedAccelSpeed = -1500;// * Time.deltaTime;

        tr.GetComponent<Rigidbody>().AddForce(tr.transform.up * fixedAccelSpeed * Time.deltaTime);
        
        //값을 반전시키면 액셀과 브레이크 변경
        throttle = -Input.GetAxis("Vertical");

        if (0 < velocity || 0 < throttle)
            GetComponent<Rigidbody>().AddForce(transform.up * throttle * 2200 * Time.deltaTime);

        if (Input.GetKey("space"))
        {
            //로컬좌표기준 힘을 가함.
            GetComponent<Rigidbody>().AddForce(transform.up * speed * Time.deltaTime);
            //rigidbody.AddRelativeForce(Vector3.forward * speed);

            //전역좌표기준 힘을 가함.
            //rigidbody.AddForce (Vector3.forward * speed);
        }
        else if (Input.GetKey("b"))
        {
            //로컬좌표기준 힘을 가함.
            GetComponent<Rigidbody>().AddForce(transform.up * (-speed) * Time.deltaTime);
            //rigidbody.AddRelativeForce(Vector3.forward * speed);

            //전역좌표기준 힘을 가함.
            //rigidbody.AddForce (Vector3.forward * speed);
        }
        else if (Input.GetKey("a"))
        {
            tr.Rotate(new Vector3(0, 50.0f, 0) * Time.deltaTime);
        }
        else if (Input.GetKey("d"))
        {
            tr.Rotate(new Vector3(0, -50.0f, 0) * Time.deltaTime);
        }

        //위치 변경 테스트
        if (Input.GetKeyDown("1"))
        {
            if (0 < velocity)
                GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

            this.transform.position = GameObject.Find("Respawn1").GetComponent<Transform>().position;
            this.transform.rotation = GameObject.Find("Respawn1").GetComponent<Transform>().rotation;

            Time.timeScale = 1.0f;
        }
        if (Input.GetKeyDown("2"))
        {
            if (0 < velocity)
                GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

            this.transform.position = GameObject.Find("Respawn2").GetComponent<Transform>().position;
            this.transform.rotation = GameObject.Find("Respawn2").GetComponent<Transform>().rotation;

            Time.timeScale = 1.0f;
        }
        if (Input.GetKeyDown("3"))
        {
            if (0 < velocity)
                GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

            this.transform.position = GameObject.Find("Respawn3").GetComponent<Transform>().position;
            this.transform.rotation = GameObject.Find("Respawn3").GetComponent<Transform>().rotation;

            Time.timeScale = 1.0f;
        }
        if (Input.GetKeyDown("4"))
        {
            if (0 < velocity)
                GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

            this.transform.position = GameObject.Find("Respawn4").GetComponent<Transform>().position;
            this.transform.rotation = GameObject.Find("Respawn4").GetComponent<Transform>().rotation;

            Time.timeScale = 1.0f;
        }
        if (Input.GetKeyDown("5"))
        {
            if (0 < velocity)
                GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

            this.transform.position = GameObject.Find("Respawn5").GetComponent<Transform>().position;
            this.transform.rotation = GameObject.Find("Respawn5").GetComponent<Transform>().rotation;

            Time.timeScale = 1.0f;
        }


        //지속적인 힘 가하는 코드.
        if (true == addForce)
            GetComponent<Rigidbody>().AddForce(transform.up * addForceSpeed * Time.deltaTime); //시간의 개념 도입 위해 Time.deltaTime 곱해줌.


        //90도 이상이면 뒤쪽으로 이동 중인 것.
        float forwardBack = Vector3.Angle(standardZ, GameObject.FindWithTag("Player").transform.up);
        //Debug.Log("forwardBack" + forwardBack);

        if (90.0f < forwardBack)
            isForwardDirection = false;
        else if (forwardBack < 90.0f)
            isForwardDirection = true;


        /*
        //각도 변화 계산하여 대입.(스크립트로 구한 각도)
        changeValueX = (-Vector3.Angle(standardY, GameObject.FindWithTag("Player").transform.up) + 90.0f) - prevPitch;

        //z축 기준 양의 방향으로 이동 중이 아니면 양/음의 값을 반전
        if (true == isForwardDirection)
        {
            if (40 <= changeValueY || changeValueY <= -40)
                changeValueY = 0;
            else
                changeValueY = (-Vector3.Angle(standardX, -GameObject.FindWithTag("Player").transform.forward) + 90.0f) - prevYaw;

            if (40 <= changeValueZ || changeValueZ <= -40)
                changeValueZ = 0;
            else
                changeValueZ = (Vector3.Angle(standardZ, GameObject.FindWithTag("Player").transform.right) - 90.0f) - prevRoll;
        }
        else if(false == isForwardDirection)
        {
            if (40 <= changeValueY || changeValueY <= -40)
                changeValueY = 0;
            else
                changeValueY = -(-Vector3.Angle(standardX, -GameObject.FindWithTag("Player").transform.forward) + 90.0f) - prevYaw;

            if (40 <= changeValueZ || changeValueZ <= -40)
                changeValueZ = 0;
            else
                changeValueZ = -(Vector3.Angle(standardZ, GameObject.FindWithTag("Player").transform.right) - 90.0f) - prevRoll;
        }
        */


        //각도 변화 계산하여 대입.
        //if (rotatingRailLeft == false)
        {
            //급격한 각도 변화 제한.
            if (30 <= tr.eulerAngles.x - prevX || tr.eulerAngles.x - prevX <= -30)
                ;
            else
                changeValueX = tr.eulerAngles.x - prevX;

            if (30 <= tr.eulerAngles.y - prevY || tr.eulerAngles.y - prevY <= -30)
                ;
            else
                changeValueY = tr.eulerAngles.y - prevY;

            if (30 <= tr.eulerAngles.z - prevZ || tr.eulerAngles.z - prevZ <= -30)
                ;
            else
                changeValueZ = tr.eulerAngles.z - prevZ;
        }



        //float dist = Vector3.Distance(tr.position,prevTr);
        //Debug.Log(dist / delay);


        //속도 얻는 코드.
        //이동하는 방향벡터와 객차의 forward 방향 벡터 사이의 각도를 구함
        betweenAngle = Vector3.Angle((tr.position - prevTr), transform.up);
        //Debug.Log("각도 : " + betweenAngle);

        //Debug.Log("tr = " + tr.position.ToString() + " prevTr = " + prevTr.ToString());

        //사이 각도가 90도 이상이면 후진 중이므로.
        if (90 < betweenAngle)
            velocity = -(float)(dist / delay * 3.6);
        //사이 각도가 90도 이하이면 전진 중이므로. 
        else if (betweenAngle < 90)
            velocity = (float)(dist / delay * 3.6);

        Debug.Log(velocity + "Km/h");

        //현재와 이전의 y 포지션 값 사이의 차의 값이 음수라면 하강 중이라는 뜻.
        if (isFalling)
            heaveVelocity = (float)(distForYAxis / delay * 3.6);
        else
            heaveVelocity = 0;

        // Debug.Log("히브 값 : " + heaveVelocity + "km/h");
        if ((dist / delay * 3.6) >= 30)
        {
            if (GetComponent<AudioSource>().isPlaying == false)
            {
                GetComponent<AudioSource>().clip = downFast;
                GetComponent<AudioSource>().volume = 0.3f;
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if (GetComponent<AudioSource>().isPlaying == false)
            {
                GetComponent<AudioSource>().clip = up;
                GetComponent<AudioSource>().volume = 0.3f;
                GetComponent<AudioSource>().Play();
            }
        }

        if (rotatingRailLeft == false && rotatingRailRight == false)
        {
            //만약 왼쪽으로 기울었다면
            if (0.1f <= (rightWing.position.y - leftWing.position.y))
            {
                //if (true == isReverse)
                //    tr.Rotate(new Vector3(0, 0.6f, 0));
                //else if (false == isReverse)
                //    tr.Rotate(new Vector3(0, -0.6f, 0));

                if (true == isReverse)
                    tr.Rotate(new Vector3(0, 30.0f, 0) * Time.deltaTime);
                else if (false == isReverse)
                    tr.Rotate(new Vector3(0, -30.0f, 0) * Time.deltaTime);

            }
            ////오른쪽으로 기울었다면
            else if (0.1f <= (leftWing.position.y - rightWing.position.y))
            {
                //if (true == isReverse)
                //    tr.Rotate(new Vector3(0, -0.6f, 0));
                //else if (false == isReverse)
                //    tr.Rotate(new Vector3(0, 0.6f, 0));

                if (true == isReverse)
                    tr.Rotate(new Vector3(0, -30.0f, 0) * Time.deltaTime);
                else if (false == isReverse)
                    tr.Rotate(new Vector3(0, 30.0f, 0) * Time.deltaTime);
            }
        }
        if (rotatingRailLeft == true)
        {
            //만약 왼쪽으로 기울었다면 (15도 기울었을 때 날개 사이의 y포지션 값 차이는 0.32378)
            if (0.373f <= (rightWing.position.y - leftWing.position.y))
            {
                //tr.Rotate(new Vector3(0, -0.4f, 0));
                tr.Rotate(new Vector3(0, -20.0f, 0) * Time.deltaTime);
            }
            ////오른쪽으로 기울었다면
            else if (0.373f >= (rightWing.position.y - leftWing.position.y))
            {
                //tr.Rotate(new Vector3(0, 0.4f, 0));
                tr.Rotate(new Vector3(0, 20.0f, 0) * Time.deltaTime);
            }

        }
        else if (rotatingRailRight == true)
        {
            //만약 왼쪽으로 기울었다면
            if (0.373f <= (leftWing.position.y - rightWing.position.y))
            {
                //tr.Rotate(new Vector3(0, 0.4f, 0));
                tr.Rotate(new Vector3(0, 20.0f, 0) * Time.deltaTime);
            }
            //오른쪽으로 기울었다면
            else if (0.373f >= (leftWing.position.y - rightWing.position.y))
            {
                //tr.Rotate(new Vector3(0, -0.4f, 0));
                tr.Rotate(new Vector3(0, -20.0f, 0) * Time.deltaTime);
            }
        }

    }

    //Trigger 구간 충돌 체크.
    void OnTriggerEnter(Collider coll)
    {
        //if (coll.gameObject.tag == "End")
        //{
        //    Application.LoadLevel("StartScene");
        //}

        //if (coll.gameObject.tag == "ADDFORCE")
        //{
        //    tr.GetComponent<Rigidbody>().AddForce(tr.transform.up * 80000.0f * Time.deltaTime);//350);
        //    //Destroy(coll.gameObject);
        //}


        //if (coll.gameObject.tag == "LEFTROTATE")
        //{
        //    if (rotatingRailLeft == true)
        //        rotatingRailLeft = false;
        //    else
        //        rotatingRailLeft = true;
        //}
        //else if (coll.gameObject.tag == "RIGHTROTATE")
        //{
        //    if (rotatingRailRight == true)
        //        rotatingRailRight = false;
        //    else
        //        rotatingRailRight = true;
        //}
        //if (coll.gameObject.tag == "ADDFORCE_START")
        //{
        //    addForce = true;
        //}
        //else if (coll.gameObject.tag == "ADDFORCE_END")
        //{
        //    addForce = false;
        //}

        //if (coll.gameObject.tag == "REVERSE")
        //{
        //    if (isReverse == true)
        //        isReverse = false;
        //    else if (isReverse == false)
        //        isReverse = true;
        //}

        //if(coll.gameObject.tag == "FALLING")
        //{
        //    isFalling = !isFalling;
        //}

        //if(coll.gameObject.tag == "DEVILPUNCH")
        //{
        //    guiManager.DispScore(-5);
        //}

        //if (coll.gameObject.tag == "SLOW")
        //{


        //    isSlowMotion = !isSlowMotion;
        //}


        //if(coll.gameObject.tag == "RESPAWN1")
        //{
        //    if (0 < velocity)
        //        GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

        //    this.transform.position = GameObject.Find("Respawn1").GetComponent<Transform>().position;
        //    this.transform.rotation = GameObject.Find("Respawn1").GetComponent<Transform>().rotation;

        //    guiManager.DispScore(-10);
        //}

        //if (coll.gameObject.tag == "RESPAWN2")
        //{
        //    if (0 < velocity)
        //        GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

        //    this.transform.position = GameObject.Find("Respawn2").GetComponent<Transform>().position;
        //    this.transform.rotation = GameObject.Find("Respawn2").GetComponent<Transform>().rotation;

        //    guiManager.DispScore(-10);
        //}

        //if (coll.gameObject.tag == "RESPAWN3")
        //{
        //    if (0 < velocity)
        //        GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

        //    this.transform.position = GameObject.Find("Respawn3").GetComponent<Transform>().position;
        //    this.transform.rotation = GameObject.Find("Respawn3").GetComponent<Transform>().rotation;

        //    guiManager.DispScore(-10);
        //}

        //if (coll.gameObject.tag == "RESPAWN4")
        //{
        //    if (0 < velocity)
        //        GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

        //    this.transform.position = GameObject.Find("Respawn4").GetComponent<Transform>().position;
        //    this.transform.rotation = GameObject.Find("Respawn4").GetComponent<Transform>().rotation;

        //    guiManager.DispScore(-10);
        //}

        //if (coll.gameObject.tag == "RESPAWN5")
        //{
        //    if (0 < velocity)
        //        GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

        //    this.transform.position = GameObject.Find("Respawn5").GetComponent<Transform>().position;
        //    this.transform.rotation = GameObject.Find("Respawn5").GetComponent<Transform>().rotation;

        //    guiManager.DispScore(-10);
        //}
        if (coll.gameObject.tag == "GUN")
        {
            Debug.Log("Gun");
            GameObject.Find("Male").GetComponent<Animator>().SetBool("IsTakeGun", true);
            GameObject.Find("Ak47Body").SetActive(true);
           
        }
        if (coll.gameObject.tag == "TORCH")
        {
            GameObject.Find("Male").GetComponent<Animator>().SetBool("IsTakeGun", false);
            GameObject.Find("Male").GetComponent<Animator>().SetBool("IsPutInGun", true);
            GameObject.Find("Male").GetComponent<Animator>().SetBool("IsTakeTorch", true);
        }

        switch (coll.gameObject.tag)
        {
           // case "GUN": Debug.Log("Gun"); break;

            case "End":  Application.LoadLevel("StartScene");   break;

            case "ADDFORCE":    tr.GetComponent<Rigidbody>().AddForce(tr.transform.up * 80000.0f * Time.deltaTime); break;

            case "LEFTROTATE":
                {
                    if (rotatingRailLeft == true)
                        rotatingRailLeft = false;
                    else
                        rotatingRailLeft = true;
                }
                break;

            case "RIGHTROTATE":
                {
                    if (rotatingRailRight == true)
                        rotatingRailRight = false;
                    else
                        rotatingRailRight = true;
                }
                break;

            case "ADDFORCE_START":  addForce = true;    break;
            case "ADDFORCE_END":    addForce = false;   break;

            case "REVERSE":
                {
                    if (isReverse == true)
                        isReverse = false;
                    else if (isReverse == false)
                        isReverse = true;
                }
                break;

            case "FALLING": isFalling = !isFalling; break;
            case "DEVILPUNCH":  guiManager.DispScore(-5);   break;
            case "SLOW": isSlowMotion = true; break;
            case "FAST": isSlowMotion = false; break;

            case "RESPAWN1":
                {
                    if (0 < velocity)
                        GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

                    this.transform.position = GameObject.Find("Respawn1").GetComponent<Transform>().position;
                    this.transform.rotation = GameObject.Find("Respawn1").GetComponent<Transform>().rotation;

                    Time.timeScale = 1f;

                    guiManager.DispScore(-10);
                }
                break;

            case "RESPAWN2":
                {
                    if (0 < velocity)
                        GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

                    this.transform.position = GameObject.Find("Respawn2").GetComponent<Transform>().position;
                    this.transform.rotation = GameObject.Find("Respawn2").GetComponent<Transform>().rotation;

                    Time.timeScale = 1f;

                    guiManager.DispScore(-10);
                }
                break;

            case "RESPAWN3":
                {
                    if (0 < velocity)
                        GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

                    this.transform.position = GameObject.Find("Respawn3").GetComponent<Transform>().position;
                    this.transform.rotation = GameObject.Find("Respawn3").GetComponent<Transform>().rotation;

                    Time.timeScale = 1f;

                    guiManager.DispScore(-10);
                }
                break;

            case "RESPAWN4":
                {
                    if (0 < velocity)
                        GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

                    this.transform.position = GameObject.Find("Respawn4").GetComponent<Transform>().position;
                    this.transform.rotation = GameObject.Find("Respawn4").GetComponent<Transform>().rotation;

                    Time.timeScale = 1f;

                    guiManager.DispScore(-10);
                }
                break;

            case "RESPAWN5":
                {
                    if (0 < velocity)
                        GetComponent<Rigidbody>().AddForce(transform.up * -1.0f * 30000 * Time.deltaTime);

                    this.transform.position = GameObject.Find("Respawn5").GetComponent<Transform>().position;
                    this.transform.rotation = GameObject.Find("Respawn5").GetComponent<Transform>().rotation;

                    Time.timeScale = 1f;

                    guiManager.DispScore(-10);
                }
                break;

            case "SCORE50": guiManager.DispScore(50);   break;

            case "BACK-400":    AutoBackAccelSpeed = -400;  break;
            case "BACK-200":    AutoBackAccelSpeed = -200;  break;
            case "BACK0":       AutoBackAccelSpeed = 0;     break;
            case "BACK200":     AutoBackAccelSpeed = 200;   break;
            case "BACK400":     AutoBackAccelSpeed = 400;   break;
            case "BACK600":     AutoBackAccelSpeed = 600;   break;
            case "BACK800":     AutoBackAccelSpeed = 800;   break;
            case "BACK1000":    AutoBackAccelSpeed = 1000;  break;
            case "BACK2000": AutoBackAccelSpeed = 2000; break;


            case "FIXEDSPEED10": fixedVelocity = 10; break;
            case "FIXEDSPEED20": fixedVelocity = 20; break;
            case "FIXEDSPEED30": fixedVelocity = 30; break;
            case "FIXEDSPEED40": fixedVelocity = 40; break;
            case "FIXEDSPEED50": fixedVelocity = 50; break;
            case "FIXEDSPEED60": fixedVelocity = 60; break;
            case "FIXEDSPEED70": fixedVelocity = 70; break;
            case "FIXEDSPEED80": fixedVelocity = 80; break;
            case "FIXEDSPEED90": fixedVelocity = 90; break;
            case "FIXEDSPEED100": fixedVelocity = 100; break;

            case "BOSSSPAWN": Boss.SetActive(true); break;

            case "GAMEOVER": Application.LoadLevel("GameOverScene"); break;

        }


    }

    void Quit()
    {
        Application.Quit();
    }

    void LateUpdate()
    {
        //delay만큼 시간이 흐를 때 마다 변화된 각도 계산, 출력.
        if (Time.time > nextSave)
        {
            //delay 초 전의 위치와의 거리 측정.
            dist = Vector3.Distance(tr.position, prevTr);
            distForYAxis = tr.position.y - prevTr.y;

            nextSave = Time.time + delay;

            //이전 각도 구하는 코드(스크립트로 구하는).
            prevPitch = -Vector3.Angle(standardY, GameObject.FindWithTag("Player").transform.up) + 90.0f;

            //앞 방향으로 가고있다면
            if (true == isForwardDirection)
            {
                prevRoll = -Vector3.Angle(standardX, -GameObject.FindWithTag("Player").transform.forward) + 90.0f;
                prevYaw = Vector3.Angle(standardZ, GameObject.FindWithTag("Player").transform.right) - 90.0f;
            }
            else
            {
                prevRoll = -(-Vector3.Angle(standardX, -GameObject.FindWithTag("Player").transform.forward) + 90.0f);
                prevYaw = -(Vector3.Angle(standardZ, GameObject.FindWithTag("Player").transform.right) - 90.0f);
            }


            //이전 각도 구하는 코드.
            prevX = tr.eulerAngles.x;
            prevY = tr.eulerAngles.y;
            prevZ = tr.eulerAngles.z;

            //이전 위치 구하는 코드.
            prevTr.x = tr.position.x;
            prevTr.y = tr.position.y;
            prevTr.z = tr.position.z;
        }
    }
}
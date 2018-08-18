using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuiManager : MonoBehaviour {
    public GameObject ovrCamera;
    public GameObject mainCamera;

    private float lastSecond;
    private float nowSecond;

    //Text UI 항목 연결을 위한 변수.
    public Text txtScore;
    //누적점수를 기록하기 위한 변수.
    private int totScore = 0;

    void Awake()
    {
        ovrCamera = GameObject.FindGameObjectWithTag("OVRCAMERA");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Start()
    {
        DispScore(0);
    }

    void Update()
    {

        //Debug.Log("TimeScale : " + Time.timeScale);
        //Time.timeScale 값이 0일 때 일시정지, 1일 때 정상 게임 재생.
        if (Input.GetKeyDown("q"))
        {
            if (0 == Time.timeScale)
                Time.timeScale = 1;
            else if (1 == Time.timeScale)
                Time.timeScale = 0;
        }
    }
    public void DispScore(int score)
    {
        totScore += score;
        txtScore.text = "score : " + totScore.ToString();
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 80, 100, 25), "Restart"))
        {
            lastSecond = Time.time;
            Application.LoadLevel("ExportTestScene2");
        }
        //nowSecond = float.Parse(Time.time.ToString());
        //nowSecond -= lastSecond;

        nowSecond += Time.deltaTime;


        //GUI.Box(new Rect(20, 50, 200, 25), nowSecond.ToString());
        //GUI.Box(new Rect(20, 50, 200, 25), Time.time.ToString());
        
    }

}

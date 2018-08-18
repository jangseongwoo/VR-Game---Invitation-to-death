using UnityEngine;
using System.Collections;

public class testsc : MonoBehaviour {
    //액셀, 브레이크 입력 값 받을 변수.
    public float throttle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        throttle = Input.GetAxis("Vertical");
	}
}

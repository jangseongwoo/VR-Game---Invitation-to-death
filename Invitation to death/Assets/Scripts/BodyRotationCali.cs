using UnityEngine;
using System.Collections;

public class BodyRotationCali : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.localRotation = GameObject.Find("Capsule-prev").transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

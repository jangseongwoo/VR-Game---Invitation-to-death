using UnityEngine;
using System.Collections;

public class SphereCtrl : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = GameObject.FindGameObjectWithTag("OVR").transform.rotation;
	}
}

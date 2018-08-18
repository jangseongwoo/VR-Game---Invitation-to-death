using UnityEngine;
using System.Collections;

public class MakeParentPlayer : MonoBehaviour
{
    private Vector3 v = new Vector3(0,-1,0);// new Vector3(0.0f, -1.2f, 0.0f);
    
	// Use this for initialization
	void Start () {
        this.gameObject.transform.parent = GameObject.Find("RotX").transform;
        transform.localPosition = Vector3.zero;
       // transform.localRotation = GameObject.Find("CoalTrain").transform.rotation;
       // transform.localRotation = Quaternion.identity;
        transform.Rotate(Vector3.up, 98);
        transform.Translate(v);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

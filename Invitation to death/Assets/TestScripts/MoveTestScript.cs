using UnityEngine;
using System.Collections;

public class MoveTestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("p"))
            this.transform.position = new Vector3(0, 0, 0);
	}
}

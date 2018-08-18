using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButton(0))
            Application.LoadLevel("ExportTestScene2");

        if(Input.GetButtonDown("Fire1"))
            Application.LoadLevel("ExportTestScene2");
	}

    void Quit()
    {
        Application.Quit();
    }
}

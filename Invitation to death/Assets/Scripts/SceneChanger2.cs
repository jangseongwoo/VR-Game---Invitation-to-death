using UnityEngine;
using System.Collections;

public class SceneChanger2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
            Application.LoadLevel("StartScene");
    }

    void Quit()
    {
        Application.Quit();
    }
}

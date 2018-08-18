using UnityEngine;
using System.Collections;

public class CrossCtrl : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        if (0 < Input.GetAxis("Horizontal"))
        {
            GetComponent<NetworkView>().RPC("checkRightTag", RPCMode.AllBuffered);
            //networkView.RPC("checkRightTag", RPCMode.AllBuffered);
            //GameObject.FindGameObjectWithTag("LEFTCROSS").collider.enabled = false;
            //GameObject.FindGameObjectWithTag("RIGHTCROSS").collider.enabled = true;
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<NetworkView>().RPC("checkLeftTag", RPCMode.AllBuffered);
            //networkView.RPC("checkLeftTag", RPCMode.AllBuffered);
            //GameObject.FindGameObjectWithTag("LEFTCROSS").collider.enabled = true;
            //GameObject.FindGameObjectWithTag("RIGHTCROSS").collider.enabled = false;
        }
        //Debug.Log(Input.GetAxis("Horizontal").ToString());
        else
        {
            this.GetComponent<Collider>().enabled = true;
        }
        
    }
    // Network 갈림길 미션 동기화를 위한 함수
    [RPC]
    void checkRightTag()
    {
        if (this.tag == "LEFTCROSS")
        {
            this.GetComponent<Collider>().enabled = false;
        }
        else if (this.tag == "RIGHTCROSS")
        {
            this.GetComponent<Collider>().enabled = true;
        }
    }
    [RPC]
    void checkLeftTag()
    {
        if (this.tag == "LEFTCROSS")
        {
            this.GetComponent<Collider>().enabled = true;
        }
        else if (this.tag == "RIGHTCROSS")
        {
            this.GetComponent<Collider>().enabled = false;
        }
    }
}


using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DesktopControls : NetworkBehaviour {

	// Use this for initialization
	void Start(){
		
		if (!Application.isMobilePlatform || NetworkServer.active) {
			GetComponent<Canvas> ().enabled = true;
		} else {
			GetComponent<Canvas> ().enabled = false;

		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}

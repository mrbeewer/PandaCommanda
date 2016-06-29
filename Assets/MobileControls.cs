using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MobileControls : NetworkBehaviour {



	// Use this for initialization
	void Start () {
		if (Application.isMobilePlatform || !NetworkServer.active) {
			GetComponent<Canvas> ().enabled = true;
			GameObject.Find ("TeamSelect").GetComponent<Canvas> ().enabled = false;
		} else {
			GetComponent<Canvas> ().enabled = false;

		}
			
	}



}

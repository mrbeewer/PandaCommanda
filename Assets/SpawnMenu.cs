using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnMenu : MonoBehaviour {

	public GameObject menu;

	// Use this for initialization
	void Start () {
		if (NetworkServer.active) {
			GameObject temp = Instantiate (menu) as GameObject;
			NetworkServer.Spawn (temp);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

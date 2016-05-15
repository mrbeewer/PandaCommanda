using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;


public class TeamsInfo : NetworkBehaviour {
	public int TeamSizes = 1;
	public List<GameObject> TeamA;
	public List<GameObject> TeamB;

	// Use this for initialization
	void Start () {
		TeamA = new List<GameObject> ();
		TeamB = new List<GameObject> ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

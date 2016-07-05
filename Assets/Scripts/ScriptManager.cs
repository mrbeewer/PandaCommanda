using UnityEngine;
using System.Collections;

public class ScriptManager : MonoBehaviour {

	public GameObject[] ScriptExecutionOrder;

	// Use this for initialization
	void Start () {
		foreach (var script in ScriptExecutionOrder) {
			script.SetActive (true);
		}
	}

}

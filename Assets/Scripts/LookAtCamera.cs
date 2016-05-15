using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LookAtCamera : MonoBehaviour {
	ShipController shipcontrol;

	// Use this for initialization
	void Start () {
		shipcontrol = GetComponentInParent<ShipController> ();
	}
	// Update is called once per frame
	void Update () {
		//transform.LookAt (Camera.main.gameObject.transform);
		GetComponent<Text> ().text = shipcontrol.Health.ToString();
	}
}

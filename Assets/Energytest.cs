using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Energytest : NetworkBehaviour {

	EnergyControls controls;


	bool enableReplinish = false;
	bool isRed = false;
	// Use this for initialization
	void Start () {
		GameObject.Find ("Red").GetComponent<Button> ().onClick.AddListener(ToggleReplenishRed);
		GameObject.Find ("Blue").GetComponent<Button> ().onClick.AddListener (ToggleReplenishBlue);
	}   
	
	// Update is called once per frame
	void Update () {

		if (isLocalPlayer) {

			if (enableReplinish){
				CmdDoStuff (isRed);
				enableReplinish = false;
			}


		}

	}



	public void ToggleReplenishRed(){
		enableReplinish = true;
		isRed = true;
	}

	public void ToggleReplenishBlue(){
		enableReplinish = true;
		isRed = false;
	}

	[Command]
	public void CmdDoStuff(bool isRed){
		if (controls == null) {
			controls = FindObjectOfType<EnergyControls> ();

		}

		if (isRed) {
			controls.RedEnergy += 100;
			if (controls.RedEnergy > controls.RedEnergyMax) {
				controls.RedEnergy = controls.RedEnergyMax;
			}
		} else {
			controls.BlueEnergy += 100;
			if (controls.BlueEnergy > controls.BlueEnergyMax) {
				controls.BlueEnergy = controls.BlueEnergyMax;
			}
		}
	}

	[Command]
	public void CmdDestroySuply(GameObject supply){
		Destroy (supply);
	}

}

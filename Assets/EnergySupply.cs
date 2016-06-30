using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnergySupply : NetworkBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void Click(){
		//if PC player return
		if (NetworkServer.active) {
			return;
		}
		//replenish the currently selected color either red or blue
		if (FindObjectOfType<BlockChanger>().ChangeToRed) {
			FindObjectOfType<Energytest> ().ToggleReplenishRed ();
		} else {
			FindObjectOfType<Energytest> ().ToggleReplenishBlue ();
		}		
		//destroy once completed
		FindObjectOfType<Energytest> ().CmdDestroySuply (gameObject);
	}

}

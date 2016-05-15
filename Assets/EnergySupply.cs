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
		if (NetworkServer.active) {
			return;
		}
			
		if (FindObjectOfType<BlockChanger>().ChangeToRed) {
			FindObjectOfType<Energytest> ().ToggleReplenishRed ();
		} else {
			FindObjectOfType<Energytest> ().ToggleReplenishBlue ();
		}		

		FindObjectOfType<Energytest> ().CmdDestroySuply (gameObject);
	}


}

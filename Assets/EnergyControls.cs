using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnergyControls : NetworkBehaviour {


	RectTransform RedBar;
	RectTransform BlueBar;

	public int RedEnergyMax = 1000;
	public int BlueEnergyMax = 1000;


	float initRedSize;
	float initBlueSize; 

	[SyncVar (hook="CalcEnergyBarRed")]
	public int RedEnergy;
	[SyncVar (hook="CalcEnergyBarBlue")]
	public int BlueEnergy;

	public int EnergyReplenishRate = 1; // replenish every x seconds
	public int EnergyReplenishAmount = 10;

	// Use this for initialization
	void Start () {
		BlueBar = GameObject.Find ("BlueEnergy").GetComponent<RectTransform> ();
		initBlueSize = BlueBar.localScale.x;

		RedEnergy = RedEnergyMax;
		BlueEnergy = BlueEnergyMax;
		RedBar = GameObject.Find ("RedEnergy").GetComponent<RectTransform> ();

		initRedSize = RedBar.localScale.x;


		RedEnergy = RedEnergyMax;
		BlueEnergy = BlueEnergyMax;

		if (NetworkServer.active) {
			InvokeRepeating ("Replenish", 0, EnergyReplenishRate);
		}
	}
		

	void Replenish(){				
		RedEnergy += EnergyReplenishAmount;
		BlueEnergy += EnergyReplenishAmount;

		if (RedEnergy > RedEnergyMax) {
			RedEnergy = RedEnergyMax;
		}

		if (BlueEnergy >BlueEnergyMax) {
			BlueEnergy = BlueEnergyMax;
		}

	}

	void CalcEnergyBarBlue(int currentEnergyLevel){

		try {
			float percentage = (currentEnergyLevel * 100) / BlueEnergyMax;
			float NewSize = (percentage * initBlueSize) / 100;
			Vector3 oldSize = BlueBar.localScale;
			BlueBar.localScale = new Vector3 (NewSize,oldSize.y,0);

		} catch (System.Exception ex) {
			print (ex.ToString());
		}

	}

	void CalcEnergyBarRed(int currentEnergyLevel){

		try {
			float percentage = (currentEnergyLevel * 100) / RedEnergyMax;
			float NewSize = (percentage * initRedSize) / 100;
			Vector3 oldSize = RedBar.localScale;
			RedBar.localScale = new Vector3 (NewSize,oldSize.y,0);

		} catch (System.Exception ex) {
			print (ex.ToString());
		}

	}


	public bool DepletEnergy(bool isRed, int amountToDeplet){

		if (isRed) {
			if (RedEnergy <= 0 || RedEnergy - amountToDeplet < 0) {
				return false;
			}
			RedEnergy -= amountToDeplet;
		} else {
			if (BlueEnergy <= 0 || BlueEnergy - amountToDeplet < 0) {
				return false;
			}
			BlueEnergy -= amountToDeplet;
		}

		return true;
	}

}

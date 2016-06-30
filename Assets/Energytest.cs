using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
/// <summary>
/// MUST BE ON PLAYER CONTROLED GAMEOBJECT.
/// Provides controls for energy supply and energycontrols for mobile
/// </summary>
public class Energytest : NetworkBehaviour {

	EnergyControls energyControls;


	bool enableReplinish = false;
	bool isRed = false;

	bool enableChangeWeapon = false;
	ChangeWeaponsControl changeWeaponControls;
	bool isBullet = true;
	// Use this for initialization
	void Start () {
		if (!isServer) {
			//Adds functions to button when the player is spawned
			GameObject.Find ("Red").GetComponent<Button> ().onClick.AddListener(ToggleReplenishRed);
			GameObject.Find ("Blue").GetComponent<Button> ().onClick.AddListener (ToggleReplenishBlue);

			GameObject.Find ("ChngBullet").GetComponent<Button> ().onClick.AddListener (ToggleToBullet);
			GameObject.Find ("ChngRocket").GetComponent<Button> ().onClick.AddListener (ToggleToRocket);
		}

	}   
	
	// Update is called once per frame
	void Update () {

		if (isLocalPlayer) {

			if (enableReplinish){
				CmdAddEnergy (isRed);
				enableReplinish = false;
			}

			if (enableChangeWeapon) {
				CmdChangeWeapon (isBullet);
				enableChangeWeapon = false;
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

	/// <summary>
	/// Adds energy to EnergyControls based to if the (mobile)user has red or bue selected
	/// </summary>
	/// <param name="isRed">If set to <c>true</c> is red.</param>
	[Command]
	public void CmdAddEnergy(bool isRed){
		if (energyControls == null) {
			energyControls = FindObjectOfType<EnergyControls> ();

		}

		if (isRed) {
			energyControls.RedEnergy += 100;
			if (energyControls.RedEnergy > energyControls.RedEnergyMax) {
				energyControls.RedEnergy = energyControls.RedEnergyMax;
			}
		} else {
			energyControls.BlueEnergy += 100;
			if (energyControls.BlueEnergy > energyControls.BlueEnergyMax) {
				energyControls.BlueEnergy = energyControls.BlueEnergyMax;
			}
		}
	}

	/// <summary>
	/// Used by the energy supply. Destroys the object provided.
	/// </summary>
	/// <param name="supply">Supply.</param>
	[Command]
	public void CmdDestroySuply(GameObject supply){
		Destroy (supply);
	}






	//HACK everything below here is a HACK

	public void ToggleToBullet(){
		
		enableChangeWeapon = true;
		isBullet = true;


		if (changeWeaponControls == null) {
			changeWeaponControls = FindObjectOfType<ChangeWeaponsControl> ();
		}

		foreach (var item in FindObjectsOfType<ShipWeaponControl>()) {
			item.Projectile = Resources.Load ("Projectiles/Bullet") as GameObject;
		}

	}

	public void ToggleToRocket(){
		enableChangeWeapon = true;
		isBullet = false;

		foreach (var item in FindObjectsOfType<ShipWeaponControl>()) {
			item.Projectile = Resources.Load ("Projectiles/Rocket") as GameObject;
		}
	}


	[Command]
	public void CmdChangeWeapon(bool value){
		if (changeWeaponControls == null) {
			changeWeaponControls = FindObjectOfType<ChangeWeaponsControl> ();
		}
			

		if (value) {
			foreach (var item in FindObjectsOfType<ShipWeaponControl>()) {
				item.Projectile = Resources.Load ("Projectiles/Bullet") as GameObject;
			}	
		} else {
			foreach (var item in FindObjectsOfType<ShipWeaponControl>()) {
				item.Projectile = Resources.Load ("Projectiles/Rocket") as GameObject;
			}
		}

		/*
		foreach (var item in FindObjectsOfType<ShipWeaponControl>()) {
			if (!isServer && !item.isLocalPlayer) {
				localPlayer = item;
				localPlayer.Projectile = GetWeapon ();
			} else if (isServer && item.isLocalPlayer) {
					localPlayer = item;
					localPlayer.Projectile = GetWeapon ();
				}

		} */
	}


	GameObject GetWeapon(){
		/*
		switch (FindObjectOfType<ChangeWeaponsControl>().EnumValue) {
			case 0:

			return Resources.Load ("Projectiles/Bullet") as GameObject;
			break;

			case 1:
			return Resources.Load ("Projectiles/Rocket") as GameObject;

			break;

			default:
			return null;
		}*/

		return null;
	}

}

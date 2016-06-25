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


	[Command]
	public void CmdChangeWeapon(int enumValue){
		ShipWeaponControl localPlayer = null;

		foreach (var item in FindObjectsOfType<ShipWeaponControl>()) {
			if (item.isLocalPlayer) {
				localPlayer = item;
			}
		}



		switch (enumValue) {
			case 0:
				localPlayer.Projectile = Resources.Load ("Projectiles/Bullet") as GameObject;
			break;

			case 1:
				localPlayer.Projectile = Resources.Load ("Projectiles/Rocket") as GameObject;
			break;
				

			default:
			break;
		}

	}

	enum WeaponType{
		Bullet, // 0
		Rocket // 1
	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class ShipWeaponControl : NetworkBehaviour {
	public GameObject projectile;

	public GameObject Projectile {
		get {
			return projectile;
		}
		set {

			if (value.GetComponent<Rocket>()) {
				ShootFreq = 3;
			} else {
				ShootFreq = 20;
			}

			projectile = value;
		}
	}

	bool canShoot = true;
	float ShootFreq = 20;
	float freqCounter;

	[SyncVar]
	public bool isRed = false;

	public bool IsRed {
		get {
			return isRed;
		}
		set {

			isRed = value;
		}
	}



	#region test
	ChangeWeaponsControl chngweapon;
	#endregion
	// Use this for initialization
	void Start () {
		freqCounter = 30 / ShootFreq;


	}
	
	// Update is called once per frame
	void Update () {

		//Controls for player
		if (isLocalPlayer && isServer) {

			//Fire1 is space on desktop and a button on mobile
			if (CrossPlatformInputManager.GetButtonDown("Fire1")) {
				CmdShoot ();
			}

			if (Input.GetKeyDown(KeyCode.F)) {
				ToggleBulletCollor ();
			}


		}
	}

	void ToggleBulletCollor(){
		IsRed = IsRed ? false : true;
	}

	/// <summary>
	/// Sends command to Server to create bullet on server 
	/// then with RpcShoot creates bullet on each of the servers
	/// </summary>
	/// <param name="speedToFireBullet">Speed to fire bullet.</param>
	[Command]
	void CmdShoot(){
		if (canShoot && FindObjectOfType<EnergyControls>().DepletEnergy(IsRed, projectile.GetComponent<Projectile>().EnergyNeeded)) {
			GameObject temp = Instantiate (projectile) as GameObject;
			temp.transform.position = gameObject.transform.FindChild ("Gun").position;
			temp.transform.rotation = gameObject.transform.FindChild ("Gun").rotation;

			temp.GetComponent<Projectile> ().isRed = isRed;
			RpcShoot (isRed);
			StartCoroutine (ShootTimer ());

		}
	}

	/// <summary>
	/// called from the server and creates bullet on each client.
	/// </summary>
	/// <param name="speedToFireBullet">Speed to fire bullet.</param>
	[ClientRpc]
	void RpcShoot(bool _isred){

		if (isServer) {
			return;
		}
			GameObject temp = Instantiate (projectile) as GameObject;
			temp.transform.position = gameObject.transform.FindChild ("Gun").position;
			temp.transform.rotation = gameObject.transform.FindChild ("Gun").rotation;
			temp.GetComponent<Projectile> ().isRed = _isred;

			//temp.GetComponent<Rigidbody> ().velocity = temp.transform.up * speedToFireBullet;

	}

	IEnumerator ShootTimer(){
		canShoot = false;
		yield return new WaitForSeconds (1f / ShootFreq);
		canShoot = true;
	}


}

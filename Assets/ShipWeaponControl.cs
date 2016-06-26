using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class ShipWeaponControl : NetworkBehaviour {
	public float BulletSpeed = 30;
	public GameObject Bullet;
	bool canShoot = true;
	public float ShootFreq = 30; // per second
	float freqCounter;

	bool isRed = false;

	public bool IsRed {
		get {
			return isRed;
		}
		set {

			isRed = value;
		}
	}

	// Use this for initialization
	void Start () {
		freqCounter = 30 / ShootFreq;
	}
	
	// Update is called once per frame
	void Update () {

		//Controls for player
		if (isLocalPlayer) {

			//Fire1 is space on desktop and a button on mobile
			if (CrossPlatformInputManager.GetButtonDown("Fire1")) {
				CmdShoot (BulletSpeed);
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
	void CmdShoot(float speedToFireBullet){
		if (canShoot && FindObjectOfType<EnergyControls>().DepletEnergy(IsRed, Bullet.GetComponent<Bullet>().EnergyNeeded)) {
			GameObject temp = Instantiate (Bullet) as GameObject;
			temp.transform.position = gameObject.transform.FindChild ("Gun").position;
			temp.transform.rotation = gameObject.transform.FindChild ("Gun").rotation;

			temp.GetComponent<Bullet> ().isRed = isRed;
			RpcShoot (BulletSpeed, isRed);
			StartCoroutine (ShootTimer ());

		}
	}

	/// <summary>
	/// called from the server and creates bullet on each client.
	/// </summary>
	/// <param name="speedToFireBullet">Speed to fire bullet.</param>
	[ClientRpc]
	void RpcShoot(float speedToFireBullet, bool _isred){

		if (isServer) {
			return;
		}
			GameObject temp = Instantiate (Bullet) as GameObject;
			temp.transform.position = gameObject.transform.FindChild ("Gun").position;
			temp.transform.rotation = gameObject.transform.FindChild ("Gun").rotation;
			temp.GetComponent<Bullet> ().isRed = _isred;

			//temp.GetComponent<Rigidbody> ().velocity = temp.transform.up * speedToFireBullet;

	}

	IEnumerator ShootTimer(){
		canShoot = false;
		print (ShootFreq);
		yield return new WaitForSeconds (1f / ShootFreq);
		canShoot = true;
	}




}

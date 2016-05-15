using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
	Vector3 startposition;
	public bool SpawnedOnThisClient = false;
	public int Damage = 10;
	[SyncVar]
	public bool isRed = false;
	public int EnergyNeeded = 10;
	float bulletSpeed = 10;
	// Use this for initialization
	void Start () {
		startposition = transform.position;
		GetComponent<Rigidbody> ().velocity = transform.up * bulletSpeed;

		CmdChangeColor ();
	}
	
	// Update is called once per frame
	void Update () {

		//once bullet is far enough away destroy it
		if (Vector3.Distance(startposition, transform.position) > 60) {
			Destroy (gameObject);
		}

	}

	void OnCollisionEnter(Collision col){
			
		if (NetworkServer.active) {
			if (col.gameObject.GetComponent<Damagable> ()) {
				if (col.gameObject.GetComponent<Damagable> ().isRed == isRed && col.gameObject.GetComponent<Damagable> ().colorname != "Black") {
					col.gameObject.GetComponent<Damagable> ().TakeDamage (Damage);
				} 
			}
		}
			Destroy (gameObject);


	}


	void CmdChangeColor(){
		GetComponent<Renderer> ().material.color = isRed ? Color.red : Color.blue;
	}

		
}

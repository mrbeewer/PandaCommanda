using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : Projectile {


	void Start () {
		projectileSpeed = 20;
		ShootFreq = 20;
		startposition = transform.position;
		GetComponent<Rigidbody> ().velocity = transform.up * projectileSpeed;

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

		if (!col.gameObject.GetComponent<ShipController>()) {
			Destroy (gameObject);
		}

		//Destroy (gameObject);
	}
		
		
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Rocket : Projectile {



	void Start () {
		projectileSpeed = 10;
		Damage = 100;


		if (NetworkServer.active) {
			//startposition = transform.position;
		}

		GetComponent<Rigidbody> ().velocity = transform.up * projectileSpeed;

		ChangeColor ();
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
				Destroy (gameObject);
			}
		}

		if (!col.gameObject.GetComponent<ShipController>()) {
			Destroy (gameObject);
		}

		//HACK
		//Destroy (gameObject);


	}

	void ChangeColor(){
		GetComponent<Renderer> ().material.color = isRed ? Color.red : Color.blue;
	}
}

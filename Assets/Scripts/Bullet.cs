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
		
		
		
}

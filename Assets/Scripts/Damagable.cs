using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Damagable : NetworkBehaviour {
	public bool isRed;
	public int Health;

	[SyncVar]
	public string colorname;

	public void TakeDamage(int damage){
		if (Health != null) {
			Health -= damage;
		} else {
			throw new UnityException ("Health for " + gameObject.name + " was not instantiated");
		}
	}
}

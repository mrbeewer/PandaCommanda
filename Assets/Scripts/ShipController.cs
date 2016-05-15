using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class ShipController : NetworkBehaviour {

	int MaxHealth = 1000;

	[SyncVar (hook="CalcHealthBar")]
	public int Health;

	float Hori;
	public float PlayerSpeed = 10;

	float HealthBarInitSize;
	RectTransform HealthBar;
	GameObject Canvas;

	// Use this for initialization
	void Start () {

		Health = MaxHealth;

		//Puts player below the map
		transform.position = GameObject.Find ("StartPos").transform.position;

		//sets healthbar vairables needed for CalcHealthBar hook
		Canvas = transform.FindChild ("Canvas").gameObject;
		HealthBar = Canvas.transform.FindChild ("Health").GetComponent<RectTransform> ();
		HealthBarInitSize = HealthBar.localScale.x;
	}

	void Update(){
		
		Hori = CrossPlatformInputManager.GetAxis ("Horizontal");

		if (isLocalPlayer) {
			transform.position += transform.right * PlayerSpeed * Time.deltaTime * Hori;
		}
	}

		

	/// <summary>
	/// Hook for the health syncvar. When health is changed 
	/// it changes the size of the health bar
	/// </summary>
	/// <param name="_health">Health.</param>
	void CalcHealthBar(int _health){

		try {
			float percentage = (_health * 100) / MaxHealth;
			float NewSize = (percentage * HealthBarInitSize) / 100;
			Vector3 oldSize = HealthBar.localScale;
			HealthBar.localScale = new Vector3 (NewSize,oldSize.y,0);

			if (_health <= 0) {
				transform.position = GameObject.Find ("StartPos").transform.position;
			}

		} catch (System.Exception ex) {
			print (ex.ToString());
		}

	}
}

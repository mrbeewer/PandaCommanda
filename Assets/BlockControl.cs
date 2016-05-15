﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BlockControl : Damagable {

	public Material Dmg1;
	public Material Dmg2;

	public GameObject EnergySupply;

	public Color color;
	float speed = 4f;
	// Use this for initialization
	void Start () {
		Health = 50;
		ChangeColor ();
	}
	
	// Update is called once per frame
	void Update () {
		MoveForaward ();
		if (Health <= 0) {
			Destroy (gameObject);
		} else if(Health < 40 && Health > 20){
				GetComponent<Renderer> ().material = Dmg1;
		} else if (Health < 20) {
			GetComponent<Renderer> ().material = Dmg2;
		}
		ChangeColor ();
	}

	void MoveForaward(){
		transform.position += -transform.forward * speed * Time.deltaTime;
	}

	public void ChangeColor(){
		switch (colorname) {
			case "Blue":
				GetComponent<Renderer> ().material.color = Color.blue;
				if (NetworkServer.active) {
					isRed = false;

				}
			break;

			case "Red":
				GetComponent<Renderer> ().material.color = Color.red;
				if (NetworkServer.active) {
					isRed = true;
				}
			break;

			case "Black":
				GetComponent<Renderer> ().material.color = Color.black;

			break;
			default:
			break;
		}
	}

	void OnDestroy(){
		if (!NetworkServer.active) {
			return;
		}
		int chance = Random.Range (0, 100);

		if (chance < 10) {
			GameObject temp = Instantiate (EnergySupply) as GameObject;
			temp.transform.position = gameObject.transform.position;

			NetworkServer.Spawn (temp);
		}
	}

	public void TouchChangeColorRed(){
		if (colorname == "Black") {
			if (FindObjectOfType<BlockChanger>().ChangeToRed) {
				colorname = "Red";
				GetComponent<Renderer> ().material.color = Color.red;
				isRed = true;
				ClientScene.localPlayers [0].gameObject.GetComponent<BlockChanger> ().CmdTouchChangeColorRed (netId);
			} else {
				colorname = "Blue";
				GetComponent<Renderer> ().material.color = Color.blue;
				isRed = false;
				ClientScene.localPlayers [0].gameObject.GetComponent<BlockChanger> ().CmdTouchChangeColorBlue (netId);
			}

		}
	}




}
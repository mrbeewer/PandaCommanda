using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BlockChanger : NetworkBehaviour {

	public bool changeToRed = false;
	Text CurrentColorText;
	public bool ChangeToRed {
		get {
			return changeToRed;
		}
		set {
			changeToRed = value;
		}
	}

	void Start(){
		if (!NetworkServer.active) {
			GameObject.Find ("ChangeToBlue").GetComponent<Button> ().onClick.AddListener (ToggleChangeToBlue);
			GameObject.Find ("ChangeToRed").GetComponent<Button> ().onClick.AddListener (ToggleChangeToRed);
			CurrentColorText = GameObject.Find ("CurrentColor").GetComponent<Text> ();
			CurrentColorText.text = "Blue";
		}

	}

	public void ToggleChangeToRed(){
		ChangeToRed = true;
		CurrentColorText.text = "Red";

	}

	public void ToggleChangeToBlue(){
		ChangeToRed = false;
		CurrentColorText.text = "Blue";

	}

	[Command]
	public void CmdTouchChangeColorRed(NetworkInstanceId id){
		foreach (var item in FindObjectsOfType<NetworkIdentity>()) {
			if (item.netId == id) {
				if (item.GetComponent<BlockControl>().colorname == "Black") {
					item.GetComponent<BlockControl>().colorname = "Red";
					item.GetComponent<BlockControl> ().isRed = true;
					item.gameObject.GetComponent<Renderer> ().material.color = Color.red;
				}
			}
		}
	}





	[Command]
	public void CmdTouchChangeColorBlue(NetworkInstanceId id){
		foreach (var item in FindObjectsOfType<NetworkIdentity>()) {
			if (item.netId == id) {
				if (item.GetComponent<BlockControl>().colorname == "Black") {
					item.GetComponent<BlockControl>().colorname = "Blue";
					item.GetComponent<BlockControl> ().isRed = false;
					item.gameObject.GetComponent<Renderer> ().material.color = Color.blue;
				}
			}
		}
	}
}

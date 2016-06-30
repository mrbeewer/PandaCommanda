using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Used by mobile user
/// </summary>
public class BlockChanger : NetworkBehaviour {

	public bool changeToRed = false;
	//Text CurrentColorText;
	Image CurrentColorBar;
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
			//Adds functions to buttons when Player is spwaned
			GameObject.Find ("ChangeToBlue").GetComponent<Button> ().onClick.AddListener (ToggleChangeToBlue);
			GameObject.Find ("ChangeToRed").GetComponent<Button> ().onClick.AddListener (ToggleChangeToRed);
			/*
			CurrentColorText = GameObject.Find ("CurrentColor").GetComponent<Text> ();
			CurrentColorText.text = "Blue"; */

			CurrentColorBar = GameObject.Find ("CurrentColorMobile").GetComponent<Image> ();
			CurrentColorBar.color = Color.blue;
		}

	}
	/// <summary>
	/// changes the currently selected color for mobile player to red
	/// </summary>
	public void ToggleChangeToRed(){
		ChangeToRed = true;
		//CurrentColorText.text = "Red";
		CurrentColorBar.color = Color.red;

	}
	/// <summary>
	/// Changed the currently selected color for mobile player to blue
	/// </summary>
	public void ToggleChangeToBlue(){
		ChangeToRed = false;
		//CurrentColorText.text = "Blue";
		CurrentColorBar.color = Color.blue;

	}

	/// <summary>
	/// Finds the given NetID of a block on Server and changes the color to red
	/// </summary>
	/// <param name="id">Identifier.</param>
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




	/// <summary>
	/// Finds the given NetID of a block on Server and changes the color to blue
	/// </summary>
	/// <param name="id">Identifier.</param>
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

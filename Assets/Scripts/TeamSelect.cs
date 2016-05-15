using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TeamSelect : NetworkBehaviour {

	GameObject player;
	public Transform TeamASpawn;
	public Transform TeamBSpawn;
	// Use this for initialization
	void Start () {
		if (!NetworkServer.active) {
			GetComponent<Canvas> ().enabled = false;
		}
		TeamASpawn = NetworkManager.singleton.startPositions [1];
		TeamBSpawn = NetworkManager.singleton.startPositions [0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Lets user choose which side the play wants to be on.
	/// Currently nothing stops them from being on the same team. once they have selected 
	/// They have no more choices. 
	/// 
	/// Working on fixing that
	/// </summary>
	/// <param name="_isTeamA">If set to <c>true</c> is team a.</param>
	public void SelectTeam(bool _isTeamA){

		player = ClientScene.localPlayers [0].gameObject;

		if (_isTeamA) {
			player.transform.position = TeamASpawn.transform.position;
			player.transform.rotation = TeamASpawn.transform.rotation;
		} else {
			player.transform.position = TeamBSpawn.transform.position;
			player.transform.rotation = TeamBSpawn.transform.rotation;
			Camera.main.transform.Rotate (Camera.main.transform.up, 180);

		}

		GetComponent<Canvas> ().enabled = false;

	}




}

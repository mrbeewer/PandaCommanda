using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour {

	public static LevelControl singleton;

	public Text LevelText;
	public int currentLevel;
	public GameObject BlockSpawnerObject;

	private bool continueLevel = false;


	void Awake() {
		if (singleton == null)
			singleton = this;
		else if (singleton != this)
			Destroy(gameObject); 
	}
		
	public void onStartClick () {
		if (NetworkServer.active) {
			InitiateLevel();
		}
	}

	void InitiateLevel() {
		HUDControl.singleton.level++;
		Debug.Log ("Start Level " + HUDControl.singleton.level);
		if (HUDControl.singleton.level < 6) {
			StartCoroutine (ShowLevelText (2, "Level " + HUDControl.singleton.level));
			BlockSpawner.singleton.NewLevelStarted ();
		} else {
			StartCoroutine (ShowLevelText (10, "GAME OVER"));
		}
	}
	
	// Update is called once per frame
	void Update () {

	}


	IEnumerator ShowLevelText(int time, string text){
		LevelText.gameObject.SetActive (true);
		LevelText.text = text;
		yield return new WaitForSeconds (time);
		LevelText.gameObject.SetActive (false);
	}

	public IEnumerator WaitThenStartNextLevel(int time){
		yield return new WaitForSeconds (time);
		InitiateLevel ();
	}

}

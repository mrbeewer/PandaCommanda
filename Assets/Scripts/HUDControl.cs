using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class HUDControl : NetworkBehaviour {

	public static HUDControl singleton;

	[SyncVar (hook = "OnScoreChange")]
	public int score = 0;
	[SyncVar (hook = "OnLevelChange")]
	public int level = 0;

	public Text ScoreText;
	public Text LevelText;

	void Awake() {
		if (singleton == null)
			singleton = this;
		else if (singleton != this)
			Destroy(gameObject); 
	}

	// Use this for initialization
	void Start () {
		ScoreText = GameObject.Find ("HUD/Score").GetComponent<Text> ();
		LevelText = GameObject.Find ("HUD/Level").GetComponent<Text> ();
	}
	

	void OnScoreChange (int score) {
		ScoreText.text = "Score: " + score;
	}

	void OnLevelChange (int level) {
		LevelText.text = "Level: " + level;
	}

	public void IncreaseScore(GameObject item) {

		if (item.CompareTag ("BasicBlock"))
			score += 100;
		else
			Debug.Log ("Score update sent from -- " + item.tag);

	}

	public void UpdateLevel(int currentLevel) {
		level = currentLevel;
	}
}

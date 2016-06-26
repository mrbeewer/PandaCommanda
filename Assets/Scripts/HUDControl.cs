using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDControl : MonoBehaviour {

	public static HUDControl singleton;


	public int score = 0;
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
	
	// Update is called once per frame
	void Update () {
		ScoreText.text = "Score: " + score;
		LevelText.text = "Level: " + level;
	}

	public void IncreaseScore(GameObject item) {

		if (item.CompareTag ("BasicBlock"))
			score += 100;
		else
			Debug.Log ("Score update sent from -- " + item.tag);

	}
}

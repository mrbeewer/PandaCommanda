using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BlockSpawner : NetworkBehaviour {

	public static BlockSpawner singleton;

	public Transform[] spawnPoints;
	public GameObject Block;


	public int currentBlockCount = 0;
	public int maxBlocksPerLevel = 20;


	public int randomCounter = 0;
	public int randomOpt0 = 0;
	public int randomOpt1 = 0;
	public int randomOpt2 = 0;


	private bool continueLevel = false;

	void Awake() {
		if (singleton == null)
			singleton = this;
		else if (singleton != this)
			Destroy(gameObject); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewLevelStarted() {
		continueLevel = true;
		currentBlockCount = 0;
		maxBlocksPerLevel = HUDControl.singleton.level * 20;
		StartCoroutine (InvokeRepeatingWithStop (4, 1));
	}


	void StartSpawn(){
		if (currentBlockCount < maxBlocksPerLevel) {
			int[] array = { 0, 1, 2, 3, 4, 5 };
			int[] arrayReturn = Shuffle (array);
			for (int i = 0; i < Random.Range(1, HUDControl.singleton.level); i++) {
				SpawnBlockRandomColorWLocation (arrayReturn [i]);
			}
		}
		else {
			continueLevel = false;
			StartCoroutine (LevelControl.singleton.WaitThenStartNextLevel (10)); 
		}
			
	}

	public GameObject SpawnBlockRandomColorWLocation(int indexOfSpawnPoint) {
		currentBlockCount++;
		GameObject temp = Instantiate (Block) as GameObject;
		BlockControl cont = temp.GetComponent<BlockControl> ();
		temp.transform.position = spawnPoints [indexOfSpawnPoint].position;
		cont.colorname = PickRandomColor ();
		NetworkServer.Spawn (temp);
		return temp;
	}


	/// <summary>
	/// Spawns block with a random color at a random spawnpoint
	/// </summary>
	/// <returns>The block random color.</returns>
	/// <param name="speed">Speed.</param>
	public GameObject SpawnBlockRandomColor(float speed){
		currentBlockCount++;
		GameObject temp = Instantiate (Block) as GameObject;
		BlockControl cont = temp.GetComponent<BlockControl> ();
		temp.transform.position = PickRandomSpawnPoint ();
		cont.colorname = PickRandomColor ();
		NetworkServer.Spawn (temp);
		return temp;
	}


	public void SpawnBlocksRandomColor(int numberOfBlocksToSpawn, float speed){
		for (int i = 0; i < numberOfBlocksToSpawn; i++) {
			StartCoroutine (SpawnSeperationTimer (Random.Range(0, 2), speed ));
		}
	}

	public void SpawnBlockAtSpawnPoint(int indexOfSpawnPoint, float speed){
		SpawnBlockRandomColor (speed).transform.position = spawnPoints [indexOfSpawnPoint].position;
	}

	public void SpawnBlockRandomPoint() {
		
	}


	string PickRandomColor(){
		Color color;
		int[] array = { 0, 1, 2 };
		int blackVal = HUDControl.singleton.level * 10;
		int colorVal = 100 - blackVal;
		int[] weights = { colorVal / 2, colorVal / 2, blackVal };

//		switch (Random.Range(0,3)) {
//			case 0:
//			return "Blue";
//			case 1:
//			return "Red";
//			default:
//			return "Black"; 
//		}

		switch (WeightRandom(array, weights)) {
			case 0:
				return "Blue";
				break;
			case 1:
				return "Red";
				break;
			case 2:
				return "Black";
				break;
			default:
				return "Black";
				break;
		}
	}



	Vector3 PickRandomSpawnPoint(){
		return spawnPoints [Random .Range(0, spawnPoints.Length)].position;
	}

	IEnumerator SpawnSeperationTimer(int time, float speed){

		yield return new WaitForSeconds (time);
		SpawnBlockRandomColor (speed);

	}

	IEnumerator InvokeRepeatingWithStop(float time, float repeatRate) {
		yield return new WaitForSeconds (time);
		do {
			StartSpawn();
			yield return new WaitForSeconds(repeatRate);
		} while (continueLevel);
	}



	static T[] Shuffle<T>(T[] array)
	{
		int n = array.Length;
		for (int i = 0; i < n; i++)
		{
			// NextDouble returns a random number between 0 and 1.
			// ... It is equivalent to Math.random() in Java.
			int r = i + (int)(Random.value * (n - i));
			T t = array[r];
			array[r] = array[i];
			array[i] = t;
		}

		return array;
	}

	static int WeightRandom(int[] array, int[] weights) {
		//array = { 0, 1, 2 };
		//weights = { 34, 33, 33 };
		int num_choices = weights.Length;
		int choice = 0;

		int sum_of_weight = 0;
		for(int i=0; i < num_choices; i++) {
			sum_of_weight += weights[i];
		}
		int rnd = Random.Range(0, sum_of_weight);
		for(int i=0; i < num_choices; i++) {
			if (rnd < weights [i]) {
				choice = i;
				break;
			}
			rnd -= weights[i];
		}

		return array[choice];

	}


}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BlockSpawner : NetworkBehaviour {

	public Transform[] spawnPoints;
	public GameObject Block;
	// Use this for initialization
	void Start () {
		if (NetworkServer.active) {
			InvokeRepeating ("StartSpawn", 4, 1);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void StartSpawn(){
		SpawnBlockRandomColor (10);
	}

	/// <summary>
	/// Spawns block with a random color at a random spawnpoint
	/// </summary>
	/// <returns>The block random color.</returns>
	/// <param name="speed">Speed.</param>
	public GameObject SpawnBlockRandomColor(float speed){
		
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


	string PickRandomColor(){
		Color color;

		switch (Random.Range(0,3)) {
			case 0:
			return "Blue";
			case 1:
			return "Red";
				
			default:
			return "Black";
		}


	}

	Vector3 PickRandomSpawnPoint(){
		return spawnPoints [Random .Range(0, spawnPoints.Length)].position;
	}

	IEnumerator SpawnSeperationTimer(int time, float speed){

		yield return new WaitForSeconds (time);
		SpawnBlockRandomColor (speed);

	}
}

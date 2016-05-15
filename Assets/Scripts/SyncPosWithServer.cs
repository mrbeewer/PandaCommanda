using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncPosWithServer : NetworkBehaviour {

	[SyncVar]
	Vector3 syncPos;
	Vector3 lastPos;
	float learpSpeed = 5;
	float movementSendRateThreshold = .05f;
	[SyncVar]
	Quaternion syncRotation;

	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer) {
			transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime * learpSpeed);
			transform.rotation = syncRotation;

		} else {
			if (Vector3.Distance(transform.position, lastPos) >= movementSendRateThreshold) {
				CmdSendPosRosToSerever (transform.position, transform.rotation);
				lastPos = transform.position;
			}
		}
	}



	[Command]
	void CmdSendPosRosToSerever(Vector3 pos, Quaternion rotation){
		syncPos = pos;
		syncRotation = rotation;

	}


	bool HasRotationChanged(){
		return transform.rotation != syncRotation;
	}
}

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;




//WONT WORK, MUST CHECK IF CHILD ISLOCALPLAYER



public class CameraMove : NetworkBehaviour {

	// Use this for initialization
	void Start () {

		if (gameObject.GetComponent<PlayerScript> ().isLocalPlayer == false) {
			return;
		}
		transform.position = new Vector3(0, 3, 0);
		transform.rotation.Set (30, 30, 30, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (gameObject.GetComponent<PlayerScript> ().isLocalPlayer == false) {
			return;
		}
		if (Input.GetKey("up")) {
			Vector3 a = transform.position;
			a.z += 0.1f;
			transform.position = a;
		}

		if (Input.GetKey ("down")) {
			Vector3 a = transform.localPosition;
			a.z -= 0.1f;
			transform.position = a;
		}

		if (Input.GetKey("left")) {
			Vector3 a = transform.position;
			a.x -= 0.1f;
			transform.position = a;
		}

		if (Input.GetKey ("right")) {
			Vector3 a = transform.localPosition;
			a.x += 0.1f;
			transform.position = a;
		}
			
	}
}

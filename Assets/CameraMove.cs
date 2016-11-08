using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey("up")) {
			Vector3 a = transform.position;
			a.z += 0.0000000000000000001f;
			transform.Translate (a);

		}

		if (Input.GetKey ("down")) {
			Vector3 a = transform.localPosition;
			a.z -= 0.0000000000000000001f;
			transform.Translate (a);
		}
			
	}
}

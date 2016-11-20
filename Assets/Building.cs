using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : Unit {

	List<Unit> canCreate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		


		if (Camera.main == null) {
			return;
		}
		if (Input.GetMouseButton (0)) {

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (gameObject.GetComponent<Collider> ().Raycast (ray, out hit, Mathf.Infinity)) {
				print ("We have clicked on the city!");
				Instantiate (GManager.main.tankDefinition/*, gameObject.transform*/);
			}
		}
	}
}

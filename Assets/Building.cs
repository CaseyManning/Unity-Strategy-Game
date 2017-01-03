using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Building : Unit {

	public static Dictionary<string, GameObject> createableUnits;

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		
		base.Update ();

		if (Camera.main == null) {
			return;
		}
		if (PlayerScript.players.ContainsKey (team)) {
			//If the building is selected, display the create unit buttons
			if (PlayerScript.players [team].selected.Contains (this) && createableUnits.Count > 0) {
				print ("I am ready to create a unit!");
				foreach (string s in createableUnits.Keys) {
					if (Input.GetKeyUp (s)) {
						Instantiate (createableUnits [s]);
					}
				}
			}
		}
	}
}

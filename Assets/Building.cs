using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Building : Unit {


	public static Dictionary<string, GameObject> createableUnits;

	// Use this for initialization
	public void Start () {
		base.Start ();
	}
	
	public void Update () {
		base.Update ();
		if (PlayerScript.players.ContainsKey (team)) {
			if (PlayerScript.players [team].selected.Contains (this) && createableUnits.Count > 0) {
				print ("I am ready to create a unit!");
				foreach (string s in createableUnits.Keys) {
					if (Input.GetKeyUp (s)) {
						GameObject g = createableUnits [s];
						print (g);
						g.GetComponent<Unit> ().team = team;
						Instantiate (g);
					}
				}
			}
		}
	}
}

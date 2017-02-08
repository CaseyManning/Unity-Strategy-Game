using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Building : Unit {

	public string[] strings;
	public GameObject[] gameObjects;
	public Dictionary<string, GameObject> unitsSpawned = new Dictionary<string, GameObject>();

	// Use this for initialization
	public void Start () {
		for (int i = 0; i < strings.Length; i++) {
			unitsSpawned.Add(strings[i], gameObjects[i]);
		}
		base.Start ();
	}
	
	public void Update () {

		if (PlayerScript.players == null) {
			return;
		}

		base.Update ();
		if (PlayerScript.players.ContainsKey (team)) {
			if (PlayerScript.players [team].selected.Contains (this) && unitsSpawned.Count > 0) {
				print ("I am ready to create a unit!");
				foreach (string s in unitsSpawned.Keys) {
					if (Input.GetKeyUp (s) && PlayerScript.players[team].resources >= unitsSpawned[s].GetComponent<Unit>().cost) {
						PlayerScript.players [team].resources -= unitsSpawned [s].GetComponent<Unit> ().cost;
						print ("test");
						GameObject g = Instantiate (unitsSpawned [s]);
						g.GetComponent<Unit> ().team = team;
						g.transform.position = transform.position;

					}
				}
			}
		}
	}
}

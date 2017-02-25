using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Building : Unit {

	public string[] strings;
	public GameObject[] gameObjects;
	public Dictionary<string, GameObject> unitsSpawned = new Dictionary<string, GameObject>();

	// Use this for initialization
	public void Start () {
		for (int i = 0; i < strings.Length; i++) {
			unitsSpawned.Add(strings[i], gameObjects[i]);
			ClientScene.RegisterPrefab (gameObjects [i]);
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
				foreach (string s in unitsSpawned.Keys) {
					if (Input.GetKeyUp (s) && PlayerScript.players[team].resources >= unitsSpawned[s].GetComponent<Unit>().cost) {
						PlayerScript.players [team].resources -= unitsSpawned [s].GetComponent<Unit> ().cost;
						print ("Creating a Unit");
//						GameObject g = Instantiate (unitsSpawned [s]);
//						if (team == 1) {
//							Shader[] shaders = g.transform.GetComponentsInChildren<Shader> ();
//							foreach(Shader sh in shaders) {
//								if (sh.name == "colour_units") {
//									//sh.gameObject = GManager.colors[1]
//								}
//							}
//						}

//						print ("Setting Authority of " + g.ToString ());
						print("Team is " + team);
						print ("S is " + s);
						PlayerScript.players [team].CmdSpawnUnit (unitsSpawned [s].name, PlayerScript.players[team].GetComponent<NetworkIdentity>(), transform.position, team);
//						g.transform.position = transform.position;
//						g.GetComponent<Unit> ().team = team;
						//PlayerScript.players[team].CmdSetAuthority(g.GetComponent<NetworkIdentity>(), PlayerScript.players[team].GetComponent<NetworkIdentity>());

					}
				}
			}
		}
	}
}
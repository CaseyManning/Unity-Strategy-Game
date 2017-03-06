using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerScript : NetworkBehaviour {

	public float resources = 0;
	public List<Unit> selected;
	public int team;
	public static Dictionary<int, PlayerScript> players;
	public List<Unit> units;


	// Use this for initialization
	void Start () {
		if (players == null) {
			players = new Dictionary<int, PlayerScript> ();
			units = new List<Unit> ();
		}
		team = players.Count;
		players.Add (team, this);
		print ("Player " + team + " is starting!");
		//this.GetComponent<NetworkIdentity> ().AssignClientAuthority (this.connectionToClient);
		printPlayers();
	}

	public static void printPlayers() {
//		Debug.LogError ("PRINTING THE HASHMAP OF " + players.Keys.Count + " PLAYERS");
//		for (int i = 0; i < players.Keys.Count; i++) {
//			Debug.LogError (i + ", " + players [i]);
//		}
	}

	void Update () {

		if (!(isLocalPlayer)) {
			gameObject.GetComponent<Camera> ().enabled = false;
			return;
		} else {
			gameObject.GetComponent<Camera> ().enabled = true;
		}
//		if(selected.Count > 0) {
//			if (selected [0].gameObject.GetComponent<Building> () != null) {
//				for (int i = 0; i < selected [0].gameObject.GetComponent<Building> ().unitsSpawned.Count; i++) {
//					//GManager.main.unitButtons.whenPressed(do the thing with selected[0].gameObject.GetComponent<Building> ().unitsSpawned[i]);
//					GManager.main.unitButtons[i].transform.FindChild("Text").GetComponent<Text>().text = 
//						selected [0].gameObject.GetComponent<Building> ().unitsSpawned.Values[i].name;
//				}
//			}
//		}
		resources += Time.deltaTime;
		units.Clear ();
		GameObject[] allunits = GameObject.FindGameObjectsWithTag ("Unit");
		foreach(GameObject u in allunits) {
			if(u.GetComponent<Unit>().team == team) {
//				u.GetComponent<NetworkIdentity> ().AssignClientAuthority (this.connectionToClient);
				units.Add (u.GetComponent<Unit>());
			}
		}
//		GManager gm = getGameManager ();
		GManager.main.UIresourceText.GetComponent<Text>().text = "Resources: " + (int)resources;
	}

//	void LateUpdate () {
//		GameObject[] cameras = GameObject.FindObjectsOfType<GameObject>();
//		foreach (GameObject c in cameras) {
//			if (c.tag.Contains ("Camera")) {
//				if (c.GetComponent<PlayerScript> ().team != team) {
//					c.tag = "Camera";
//				} else {
//					c.tag = "MainCamera";
//				}
//			}
//		}
//		GameObject[] players = GameObject.FindGameObjectsWithTag ("MainCamera");
//		foreach(GameObject i in players) {
//			i.GetComponent<Camera>().enabled = false;
//			if (i.GetComponent<NetworkIdentity>() != null) {
//				Destroy (i.GetComponent<NetworkTransform> ());
//				Destroy (i.GetComponent<NetworkIdentity> ());
//			}
//		}
//		gameObject.GetComponent<Camera>().enabled = true;
//	}

	public void addSelectedUnit(Unit add) {
		selected.Add (add);
	}

	public void deselectAll() {
		while (selected.Count > 0) {
			selected.RemoveAt (0);
		}
	}

	public static PlayerScript getLocalPlayer() {
		foreach(PlayerScript s in players.Values) {
			if (s.isLocalPlayer) {
				return s;
			}
		}
		return null;
	}

	[Command]
	public void CmdSpawnUnit(string unit, NetworkIdentity playerID, Vector3 position, int unitTeam) {
		print ("Spawning a unit!");
		Debug.Log ("Running NetworkServer.Spawn, trying to spawn " + unit);
		GameObject go = GManager.main.units [unit];
		GameObject g = Instantiate (go);
		g.GetComponent<Unit>().team = unitTeam;
		Debug.LogError ("Spawned unit on team " + g.GetComponent<Unit>().team);
		if (g.GetComponent<NetworkIdentity> ().localPlayerAuthority) {
			Debug.LogError  ("Spawning with client authority!");
		} else {
			Debug.LogError  ("Spawning without client authority!");
		}
		NetworkServer.Spawn (g);
		NetworkIdentity net = g.GetComponent<NetworkIdentity> ();
		if (playerID.isServer) {
			net.AssignClientAuthority (playerID.connectionToClient);
		} else {
			net.AssignClientAuthority (playerID.connectionToServer);
		}
		g.transform.position = position;
	}

//	[Command]
//	public void CmdSetAuthority(NetworkIdentity netId, NetworkIdentity playerID) {
//		Debug.LogError("Running NetworkServer.SetAuthority, trying to spawn " + netId.gameObject.ToString());
//		netId.AssignClientAuthority (playerID.connectionToClient);
//	}
//
	[Command]
	public void CmdSetAuthority(NetworkIdentity netId, NetworkIdentity playerID) {
		Debug.LogError("Running NetworkServer.SetAuthority on " + netId.gameObject.ToString());
		if (playerID.isServer) {
			netId.AssignClientAuthority (playerID.connectionToClient);
		} else {
			netId.AssignClientAuthority (playerID.connectionToServer);
		}
	}


	public GManager getGameManager() {
		GameObject gameManager = null;
		GameObject[] gm = gameObject.scene.GetRootGameObjects();
		foreach(GameObject g in gm) {
			if (g.name == "GameManager") {
				gameManager = g;
			}
		}
		return gameManager.GetComponent<GManager> ();
	}
}

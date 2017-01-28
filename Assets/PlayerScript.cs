using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour {

	public int resources = 0;
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
		//this.GetComponent<NetworkIdentity> ().AssignClientAuthority (this.connectionToClient);

	}
		
	void Update () {
//		foreach(PlayerScript player in players.Values) {
//			print ("-----");
//			print ("Players are " + player.team);
//		}

		//print (players.ContainsKey(0));

		units.Clear ();
			GameObject[] allunits = GameObject.FindGameObjectsWithTag ("Unit");
			foreach(GameObject u in allunits) {
			if(u.GetComponent<Unit>().team == team) {
				u.GetComponent<NetworkIdentity> ().AssignClientAuthority (this.connectionToClient);
				units.Add (u.GetComponent<Unit>());
			}
		}
	}

	void LateUpdate () {
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
		GameObject[] players = GameObject.FindGameObjectsWithTag ("MainCamera");
		foreach(GameObject i in players) {
			print ("Wheeee");
			i.GetComponent<Camera>().enabled = false;
			if (i.GetComponent<NetworkIdentity>() != null) {
				Destroy (i.GetComponent<NetworkTransform> ());
				Destroy (i.GetComponent<NetworkIdentity> ());
			}
		}
		gameObject.GetComponent<Camera>().enabled = true;

	}

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
}

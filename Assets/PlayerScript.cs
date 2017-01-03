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




	}
		
	void Update () {
		units.Clear ();
			GameObject[] allunits = GameObject.FindGameObjectsWithTag ("Unit");
			foreach(GameObject u in allunits) {
			if(u.GetComponent<Unit>().team == team) {
				u.GetComponent<NetworkIdentity> ().AssignClientAuthority (this.connectionToClient);
				units.Add (u.GetComponent<Unit>());
			}
		}
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

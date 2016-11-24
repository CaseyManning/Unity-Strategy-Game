using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour {

	public int resources = 0;
	public List<Unit> selected;
	public int team;
	public static Dictionary<int, PlayerScript> players;


	// Use this for initialization
	void Start () {
		if (players == null) {
			players = new Dictionary<int, PlayerScript> ();
		}
		team = players.Count;
		players.Add (team, this);




	}
		
	void Update () {
			GameObject[] units = GameObject.FindGameObjectsWithTag ("Unit");
			foreach(GameObject u in units) {
			if(u.GetComponent<Unit>().team == team) {
				u.GetComponent<NetworkIdentity> ().AssignClientAuthority (this.connectionToClient);
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
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GManager : MonoBehaviour {

	public Material team1;
	public Material team2;
	public Dictionary<int, Material> colors;
	public GameObject factoryDefinition;
	public GameObject[] UnitDefinitions;
	public Dictionary<string, GameObject> units = new Dictionary<string, GameObject> ();
	public GameObject UIresourceText;
	public static GManager main;
	public GameObject[] unitButtons;
	public GameObject createUnitText;
	public GameObject moveText;
	public bool showHints = true;

	void Start () {
		colors = new Dictionary<int, Material>();

		if (main == null) {
			main = this;
		}

		foreach(GameObject g in UnitDefinitions) {
			ClientScene.RegisterPrefab(g);
			units.Add (g.name, g);
		}
			

		print ("Length is " + units.Keys.Count);
		colors.Add (0, team1);
		colors.Add (1, team2);
//		print ("Am I the server? : " + gameObject.GetComponent<NetworkIdentity> ().isServer);
		print ("Unit keys are " + units.Keys);
		foreach (string key in units.Keys) {
			print (key.ToString ());
		}
	}

//	void Update () {
//
//		if (PlayerScript.players == null) {
//			return;
//		}
//
//		foreach (PlayerScript player in PlayerScript.players.Values) {
//			if (player.units.Count == 0) {
//				//end the game
//				//kill the player
//			}
//		}
//	}


	void Update () {
		if (showHints) {
			if (PlayerScript.getLocalPlayer ().selected.Count > 0 && PlayerScript.getLocalPlayer ().selected [0].GetComponent<Building> () != null && PlayerScript.getLocalPlayer ().selected [0].GetComponent<Building> ().unitsSpawned.Count > 0) {
				//display the text telling you how to create a unit
				createUnitText.SetActive (true);
			} else {
				createUnitText.SetActive (false);
			}

			if (PlayerScript.getLocalPlayer ().selected.Count > 0 && PlayerScript.getLocalPlayer ().selected [0].GetComponent<Unit> ().speed > 0) {
				//display the text telling you how to create a unit
				print("Activating Movement text");
				moveText.SetActive (true);
			} else {
				moveText.SetActive (false);
			}
		}
	}
}

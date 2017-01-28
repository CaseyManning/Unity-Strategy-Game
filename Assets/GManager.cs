using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GManager : MonoBehaviour {

	public static GameObject tankDefinition;
	public GameObject tDefinition;
	public Material team1;
	public Material team2;
	public static Dictionary<int, Material> colors;
	public static GameObject factoryDefinition;

	public static GManager main;
	// Use this for initialization

	void Start () {
		colors = new Dictionary<int, Material>();
		GManager.tankDefinition = tDefinition;
		
		if (main == null) {
			main = this;
			GManager.tankDefinition = tDefinition;
		}
		
		colors.Add (0, team1);
		colors.Add (1, team2);
	}
	
	// Update is called once per frame
	void Update () {
		foreach (PlayerScript player in PlayerScript.players.Values) {
			if (player.units.Count == 0) {
				//end the game
				//kill the player
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GManager : MonoBehaviour {

	public Material team1;
	public Material team2;
	public GameObject tankDefinition = null;
	public static Dictionary<int, Material> colors;

	public static GManager main;
	// Use this for initialization
	void Start () {
		
		if (main == null) {
			main = this;
		}
		
		colors.Add (0, team1);
		colors.Add (1, team2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

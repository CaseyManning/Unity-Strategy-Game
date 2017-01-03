using UnityEngine;
using System.Collections;

public class Factory : Building {

	// Use this for initialization
	void Start () {
		base.Start ();
		createableUnits.Add ("q", GManager.tankDefinition);
	}

	void Update() {
		base.Update ();
	}
}

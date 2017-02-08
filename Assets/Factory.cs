//using UnityEngine;
//using System.Collections.Generic;
//
//public class Factory : Building {
//
//	// Use this for initialization
//	public void Start () {
//		base.Start ();
//		createableUnits = new Dictionary<string, GameObject> ();
//		//GameObject tank = (GameObject) Resources.Load("Assets/Resources/Tank");
//		GameObject tank = GManager.main.UnitDefinitions[0];
//		createableUnits.Add ("q", tank);
//		print ("It's a tank! ");
//		print(tank);
//	}
//
//	public void Update() {
//		base.Update ();
//	}
//}

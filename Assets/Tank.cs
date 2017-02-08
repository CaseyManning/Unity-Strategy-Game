using UnityEngine;
using System.Collections;

public class Tank : Unit {

	// Use this for initialization
	void Start () {
		base.Start ();
		health = 20;
		creationTime = 5;
		speed = 2;
		attackDamage = 1;
		attackSpeed = 1;
		range = 2;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	static void getTank () {
		GameObject g = new GameObject ();
		g.AddComponent<Tank> ();
		//set material and shape
	}
}

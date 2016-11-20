using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GManager : MonoBehaviour {


	public GameObject tankDefinition = null;

	public static GManager main;
	// Use this for initialization
	void Start () {
		
		if (main == null)
			main = this;
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

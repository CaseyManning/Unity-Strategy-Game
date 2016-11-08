using UnityEngine;
using System.Collections;

public class CityScript : MonoBehaviour {

	public bool isSelected = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (gameObject.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) {
				print ("We have clicked on the city!");
				isSelected = true;
			}
		}
	}
}

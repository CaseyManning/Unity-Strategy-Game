using UnityEngine;
using System.Collections;

public class TerrainClickHandler : MonoBehaviour {

	public GameObject goTerrain;
	public GameObject target;
	//public GameObject city;

	// Use this for initialization
	void Start () {

	}

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			if (Camera.main == null) {
				return;
			}
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (goTerrain.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) {
				print ("We have clicked on the terrain!");
				GameObject t = new GameObject ();
				t.tag = "target";
				t.transform.position = hit.point;
				Instantiate(t, hit.point, new Quaternion(), target.transform);
				//city.GetComponent<CityScript> ().isSelected = false;
			}
		}
	}

}

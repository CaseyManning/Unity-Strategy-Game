using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CityScript : MonoBehaviour {

	public bool isSelected = false;
	public GameObject tank;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;

			if (Camera.main == null) {
				return;
			}
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (gameObject.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) {
				print ("We have clicked on the city!");
				isSelected = true;
			}
		}

		if (Input.GetKeyDown("q")) {
			print ("Creating another tank at " + gameObject.transform.position.ToString());
			Vector3 position = gameObject.transform.position;
			position.z -= 20;
			//Instantiate(tank, gameObject.transform.position, new Quaternion(), tank.transform);
		}
	}

	public void makeTank() {
		
	}
}

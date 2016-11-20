using UnityEngine;
using System.Collections;

public class TankMove : MonoBehaviour {

	NavMeshAgent agent;
	public SelectionManager selectionmanager;
	public bool isSelected = true;
	public int player = 0;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		//selectionmanager = GameObject.FindGameObjectWithTag ("SelectionManager").GetComponent<SelectionManager>();
	}

	// Update is called once per frame
	void Update () {

		GameObject[] targets = GameObject.FindGameObjectsWithTag ("target");
		if (targets.Length > 0 && isSelected) {
			agent.SetDestination (targets [0].transform.position);
			Destroy (targets [0]);
			isSelected = false;
		}


		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (gameObject.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) {
				print ("We have clicked on the vehicle!");

				isSelected = true;
				foreach (GameObject i in GameObject.FindGameObjectsWithTag("Unit")) {
					i.GetComponent<TankMove> ().isSelected = false;
				}
				foreach (GameObject i in GameObject.FindGameObjectsWithTag("target")) {
					Destroy (i);
				}
				isSelected = true;
			}
		}
	}
}
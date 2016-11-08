using UnityEngine;
using System.Collections;

public class TankMove : MonoBehaviour {

	NavMeshAgent agent;
	public SelectionManager selectionmanager;
	public bool isSelected = true;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}

	// Update is called once per frame
	void Update () {

		GameObject[] targets = GameObject.FindGameObjectsWithTag ("target");
		if (targets.Length > 0) {
			agent.SetDestination (targets [0].transform.position);
			Destroy (targets [0]);
			isSelected = false;
		}
	}
}

using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	int health;
	int cost;
	int creationTime;
	double speed;
	int attackDamage;
	float attackSpeed;
	double range;
	float cooldown;
	GameObject currentTarget;
	Vector3 currentMoveTarget;
	NavMeshAgent navAgent;
	public int team;
	
	// Use this for initialization
	void Start () {
		navAgent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!(PlayerScript.players [team].isLocalPlayer)) {
			//print ("Terminating Update");
			//return;
			//maybe?
		}
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("target");
		if (targets.Length > 0) {
			currentTarget = targets[0];
		}
		if (currentTarget != null) {


			if (currentTarget.GetComponent<Unit> () != null) {
				if (Vector3.Distance (currentTarget.transform.position, gameObject.transform.position) <= range) {
					//attack (currentTarget);
				}
			}

			if(PlayerScript.players [team].selected.Contains(this)) {
				navAgent.destination = currentTarget.transform.position;
				Destroy(targets[0]);
			}
		}
			
		if (Input.GetMouseButtonDown (0) && PlayerScript.players [team].isLocalPlayer) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (gameObject.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) {
				print ("We have clicked on the vehicle!");
				if (!Input.GetKey (KeyCode.LeftShift)) {
					PlayerScript.players [team].deselectAll ();
				}
				PlayerScript.players [team].addSelectedUnit (this);
				foreach (GameObject i in GameObject.FindGameObjectsWithTag("target")) {
					Destroy (i);
				}

			}
		}
	}

	void attack(Unit target) {
		
			if (cooldown <= 0) {
				target.health -= attackDamage;
				cooldown = attackSpeed;
			} else {
				cooldown -= Time.deltaTime;
			}
	}


}
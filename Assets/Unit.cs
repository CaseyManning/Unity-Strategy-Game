using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	int health = 20;
	int cost;
	int creationTime;
	double speed;
	int attackDamage = 1;
	float attackSpeed = 1;
	double range = 2;
	float attackCooldown = 20;
	public GameObject currentAttackTarget;
	Vector3 currentMoveTarget;
	NavMeshAgent navAgent;
	public int team;
	public GameObject selection;
	public bool debug = false;
	int count = 0;
	public bool teamexist = false;
	public bool isTestEnemy = false;

	public void Start () {
		print("Is the navagent null? " + (navAgent == null).ToString() );
		//selection = transform.Find ("Selection").gameObject;
		navAgent = GetComponent<NavMeshAgent> ();

//		foreach(Component i in gameObject.GetComponentsInChildren<Material>()) {
//			if (i.name == "colour_units") {
//				GameObject foo = i.gameObject;
//				Shader s = foo.GetComponent<Shader> ();
//				s = GManager.colors [team];
//
//			}
//		}

//		transform.FindChild
	}

	public void Update () {

		//if (PlayerScript.players.ContainsKey (team) && PlayerScript.players [team].selected.Contains(this)) {
			//selection.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		//} else {
			//selection.transform.position = new Vector3(transform.position.x, -20, transform.position.z);
		//}

		//If the player has clicked on the terrain to create a move target, move towards it
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("target");
		if (PlayerScript.players.ContainsKey (team) && targets.Length > 0 && PlayerScript.players[team].selected.Contains(this)) {
			currentMoveTarget = targets[0].transform.position;
			//print("Nav agent destination is null?" + navAgent.destination == null);
			navAgent.destination = currentMoveTarget;
			currentAttackTarget = null;
			Destroy(targets[0]);
			//targets[0].removeFromParent();
		

		//If the unit is supposed to be attacking something, either move towards it, or damage it
		if (currentAttackTarget != null) {
			if (currentAttackTarget.GetComponent<Unit> () != null) {
				if (Vector3.Distance (currentAttackTarget.transform.position, gameObject.transform.position) <= range) {
					attack (currentAttackTarget);
					navAgent.destination = transform.position;
				} else {
					navAgent.destination = currentAttackTarget.transform.position;
				}
			}
		
		}
			
		//If the player clicks on the unit, select it
		if (PlayerScript.players.ContainsKey (team) && Input.GetMouseButtonDown (0) && PlayerScript.players [team].isLocalPlayer) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (gameObject.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) {
				print ("We clicked on the vehicle!");
				if (!Input.GetKey (KeyCode.LeftShift)) {
					PlayerScript.players [team].deselectAll ();
				}
				PlayerScript.players [team].addSelectedUnit (this);
				foreach (GameObject i in GameObject.FindGameObjectsWithTag("target")) {
					Destroy (i);
				}

			}
		}

		//If the unit is clicked by the enemy, tell all the enemy's selected units to attack it
		if (Input.GetMouseButtonDown (1) && (!(PlayerScript.players.ContainsKey (team)) || !(PlayerScript.players [team].isLocalPlayer))) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (gameObject.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) {
				print ("Clicked by the enemy!");
				foreach (Unit i in PlayerScript.getLocalPlayer().selected) {
					i.currentAttackTarget = gameObject;
				}

			}
		}

		if(isTestEnemy) {
			print("My health is: " + health.ToString());
		}

		if (health <= 0) {
			die ();
		}
	}

	void attack(GameObject target) {
		print("Attacking in progress...");
		print(Time.deltaTime);
		if (attackCooldown <= 0) {
			target.GetComponent<Unit>().health -= attackDamage;
			attackCooldown = attackSpeed;
		} else {
			attackCooldown -= Time.deltaTime*100;
		}
	}

	void die() {
		Destroy (gameObject);
	}
}
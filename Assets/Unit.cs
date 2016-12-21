using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	int health = 1;
	int cost;
	int creationTime;
	double speed;
	int attackDamage;
	float attackSpeed;
	double range = 2;
	float cooldown;
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

		if(debug) {
			if(count == 5 && currentAttackTarget != null) {
				print("My attack target is " + currentAttackTarget.ToString());
				count = 0;
			} else {
				count += 1;
			}
		}

//		if (!(PlayerScript.players.ContainsKey (team))) {
//			return;
//		}
//
//		if (PlayerScript.players [team] == null) {
//			return;
//		}
//
//		if (!(PlayerScript.players [team].isLocalPlayer)) {
//			return;
//		}
		//if (PlayerScript.players.ContainsKey (team) && PlayerScript.players [team].selected.Contains(this)) {
			//selection.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		//} else {
			//selection.transform.position = new Vector3(transform.position.x, -20, transform.position.z);
		//}
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("target");
		if (PlayerScript.players.ContainsKey (team) && targets.Length > 0 && PlayerScript.players[team].selected.Contains(this)) {
			currentMoveTarget = targets[0].transform.position;
			//print("Nav agent destination is null?" + navAgent.destination == null);
			navAgent.destination = currentMoveTarget;
			currentAttackTarget = null;
			Destroy(targets[0]);
			//targets[0].removeFromParent();
		}

		if (currentAttackTarget != null) {
			print ("I have a target!");
			print ("Is my target a unit? " + (currentAttackTarget.GetComponent<Unit> () != null).ToString());
			if (currentAttackTarget.GetComponent<Unit> () != null) {
				if (Vector3.Distance (currentAttackTarget.transform.position, gameObject.transform.position) <= range) {
					//attack (currentTarget);
					navAgent.destination = transform.position;
				} else {
					navAgent.destination = currentAttackTarget.transform.position;
				}
			}
		
		}
			
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
		//teamexist = PlayerScript.players.ContainsKey (team);
		if(isTestEnemy) {
			//print("Is it my player? " + (!(PlayerScript.players.ContainsKey (team)) || !(PlayerScript.players [team].isLocalPlayer)).ToString());
		}
		if (Input.GetMouseButtonDown (1) && (!(PlayerScript.players.ContainsKey (team)) || !(PlayerScript.players [team].isLocalPlayer))) {
			RaycastHit hit;
			print ("Clicked by the enemy!");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (gameObject.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) {
				print ("Clicked by the enemy!");
				foreach (Unit i in PlayerScript.getLocalPlayer().selected) {
					i.currentAttackTarget = gameObject;
				}

			}
		}

		if (health <= 0) {
			die ();
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

	void die() {
		Destroy (gameObject);
	}
}
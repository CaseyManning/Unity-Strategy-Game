using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Unit : NetworkBehaviour {

	public string name;
	public int health = 20;
	public int cost;
	public int creationTime;
	public float speed = 3;
	public int attackDamage = 1;
	public float attackSpeed = 0.3f;
	public double range = 2;
	float attackCooldown = 0;
	public GameObject currentAttackTarget;
	Vector3 currentMoveTarget;
	UnityEngine.AI.NavMeshAgent navAgent;
	[SyncVar]
	public int team;
	public bool debug = false;
	public GameObject attackProjectile;
	int count = 0;
	public bool teamexist = false;
	public float projectileSpeed = 5f;

	public void Start () {
		ClientScene.RegisterPrefab (attackProjectile);
//		Debug.LogError ("We are a unit, and our team is " + team);
//		if (PlayerScript.players [team].isClient) {
//			gameObject.GetComponent<NetworkIdentity> ().AssignClientAuthority (PlayerScript.players [team].connectionToClient);
//		} else {
//			gameObject.GetComponent<NetworkIdentity> ().AssignClientAuthority (PlayerScript.players [team].connectionToServer);
//		}
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();

//		foreach(Component i in gameObject.GetComponentsInChildren<Material>()) {
//			if (i.name == "colour_units") {
//				GameObject foo = i.gameObject;
//				Shader s = foo.GetComponent<Shader> ();
//				s = GManager.colors [team];
//
//			}
//		}

//		transform.FindChild
		if (gameObject.GetComponent<Building> () == null) {
			gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ().speed = speed;
		}
	}

	public void Update () {

		if (PlayerScript.players == null) {
			return;
		}

		if (!PlayerScript.players.ContainsKey (team)) {
			return;
		}

		transform.FindChild ("Selection").GetComponent<MeshRenderer> ().enabled = PlayerScript.players [team].selected.Contains (this);

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

		if (health <= 0) {
			die ();
		}
		if (! PlayerScript.players [team].isLocalPlayer) {
			return;
		}
		//if (PlayerScript.players.ContainsKey (team) && PlayerScript.players [team].selected.Contains(this)) {
			//selection.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		//} else {
			//selection.transform.position = new Vector3(transform.position.x, -20, transform.position.z);
		//}
		//If the player has clicked on the terrain to create a move target, move towards it
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("target");
//		if (PlayerScript.players.ContainsKey (team)) {
			if (gameObject.GetComponent<Building> () == null) {
				if (targets.Length > 0 && PlayerScript.players [team].selected.Contains (this)) {
					print ("Moving selected " + gameObject.name + " unit, owned by player " + team + " and the local player's team is " + PlayerScript.getLocalPlayer ().team);
//					if(PlayerScript.players[team].isLocalPlayer) {
						currentMoveTarget = targets [0].transform.position;
						//print("Nav agent destination is null?" + navAgent.destination == null);
						navAgent.destination = currentMoveTarget;
						currentAttackTarget = null;
						Destroy (targets [0]);
						//targets[0].removeFromParent();
//					}
				}
			}
//		}
		//If the unit is supposed to be attacking something, either move towards it, or damage it
//		if(PlayerScript.players[team].isLocalPlayer) {
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
//		}

		//If the player clicks on the unit, select it
//		if (PlayerScript.players.ContainsKey (team)) {
			if (Input.GetMouseButtonDown (0)) {
				PlayerScript.printPlayers();
//				if (PlayerScript.players [team].isLocalPlayer) {
					RaycastHit hit;
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					if (gameObject.GetComponent<Collider> ().Raycast (ray, out hit, Mathf.Infinity)) {
						print ("We clicked on the " + gameObject.name + " unit, owned by player " + PlayerScript.players [team].team + " and the local player's team is " + PlayerScript.getLocalPlayer ().team);
						if (!Input.GetKey (KeyCode.LeftShift)) {
							PlayerScript.players [team].deselectAll ();
						}
						PlayerScript.players [team].addSelectedUnit (this);
						foreach (GameObject i in GameObject.FindGameObjectsWithTag("target")) {
							Destroy (i);
						}
					}
//				}
			}
//		}
	}

	void attack(GameObject target) {
		print("Attacking in progress...");
		if (attackCooldown <= 0) {
			transform.LookAt (target.transform);
			target.GetComponent<Unit>().health -= attackDamage;
			attackCooldown = attackSpeed;
			//GameObject g = Instantiate (attackProjectile);
			PlayerScript.players[team].CmdSpawnProjectile (attackProjectile, target, speed, transform.position);

			/*
			GameObject g = Instantiate(currentAttackTarget);
			g.GetComponent<ProjectileScript> ().target = target;
			g.GetComponent<ProjectileScript> ().speed = speed;
			g.transform.position = transform.position;
			*/

		} else {
			attackCooldown -= Time.deltaTime;
		}
	}

	void die() {
		print ("Trying to kill myself on team " + team);
		PlayerScript.players [team].CmdDestroyUnit (gameObject);
	}
}
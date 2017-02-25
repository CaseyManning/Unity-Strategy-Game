using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	public float speed;
	public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.LookAt (target.transform);


			transform.Translate (new Vector3 (0, 0, speed * Time.deltaTime));
			if (Vector3.Distance (transform.position, target.transform.position) < speed) {
//			Destroy (gameObject);
			}
		} else {
			Destroy (gameObject);
		}

	}
}

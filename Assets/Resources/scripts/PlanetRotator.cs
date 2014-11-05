using UnityEngine;
using System.Collections;

public class PlanetRotator : MonoBehaviour {

	public float speed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.position, Vector3.up, speed * Time.deltaTime);
		transform.RotateAround (transform.position, Vector3.left, speed  * Time.deltaTime);
	}
}

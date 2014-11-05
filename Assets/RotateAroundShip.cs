using UnityEngine;
using System.Collections;

public class RotateAroundShip : MonoBehaviour {

	public float speed = 20f;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.Find ("Player");
//		transform.RotateAround (player.transform.localPosition, Vector3.forward, speed * Time.deltaTime);'
		this.transform.Rotate(0, 0, speed * Time.deltaTime);
	}
}

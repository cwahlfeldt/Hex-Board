using UnityEngine;
using System.Collections;

public class Transmorgifier : MonoBehaviour {

	private GameObject[] meshes;

	// Use this for initialization
	void Start () {

		meshes = GameObject.FindGameObjectsWithTag("Enemy");
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject me = this.gameObject;
		meshes = GameObject.FindGameObjectsWithTag("Enemy");


	}
}

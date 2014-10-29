using UnityEngine;
using System.Collections;

public class All : MonoBehaviour {

	public GameObject[] enemies;

	// Use this for initialization
	void Awake () {
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
	}
}

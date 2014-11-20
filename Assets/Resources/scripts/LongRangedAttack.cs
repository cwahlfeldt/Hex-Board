using UnityEngine;
using System.Collections;

public class LongRangedAttack : MonoBehaviour {

	private GameObject[] enemies;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		if (enemies != null) {
			foreach (GameObject enemy in enemies) {
				
			}

		}
		else
			return;
	}
}

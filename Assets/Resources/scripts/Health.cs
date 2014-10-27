using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float health = 3f;
	private GameObject player, health1, health2, health3, allHealth;

	// Use this for initialization
	void Awake () {
		player = GameObject.Find("Player");
		health1 = GameObject.Find ("health1");
		health2 = GameObject.Find ("health2");
		health3  = GameObject.Find ("health3");
		allHealth = GameObject.FindGameObjectWithTag ("health");
	}

	void Start () {

		allHealth = GameObject.FindGameObjectWithTag ("health");
		allHealth.SetActive (true);
		
	}
	
	// Update is called once per frame
	void Update () {

		if (health == 2) 
			health3.SetActive(false);
		if (health == 1)
			health2.SetActive(false);
		if (health == 0) {
			health1.SetActive(false);
			player.SetActive(false);
		}
	}
}

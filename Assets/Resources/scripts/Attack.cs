﻿using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private RaycastHit hitter;

	private GameObject player, enemy, dest, actions;
	private CharController charController;
	private CharacterController enemyCont;

	public bool playerattack = false, enemyattack = false, isattackover = false;

	private float speed;

	private Quaternion quat;
	private Vector3 relPosition, relPosition1;

	private PlayerPath playerComponent;
	private EnemyPath enemyComponent;
	private Health playerHealth;

	// this will help decide if the enemie was killed
	public int enemiesKilled;
	private int i = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
		actions = GameObject.Find ("Actions");

		speed = 7f;

		enemyComponent = enemy.GetComponent<EnemyPath> ();
		enemyCont = enemy.GetComponent<CharacterController> ();
		charController = player.GetComponent<CharController> ();
		playerHealth = player.GetComponent<Health> ();

	}
	
	// Update is called once per frame
	void Update () {
		Clicked ();

		// for player attack only
		if (playerattack == true) {
			quat = Quaternion.LookRotation (relPosition);
			player.transform.position = Vector3.Lerp (player.transform.position, dest.transform.position, speed * Time.deltaTime);
			player.transform.rotation = Quaternion.Slerp (player.transform.rotation, quat, (speed + 5) * Time.deltaTime);

			// custom wait timer 26 higher the number longer it takes to kill enemy
			i++;
			if (i == 40) {
				charController.enabled = !charController.enabled;
				charController.isontile = false;
				Destroy (enemy);
				playerattack = false;
				isattackover = true;
				i = 0;
			}
		}

		// for enemy attack
		if (enemyattack == true) {
			quat = Quaternion.LookRotation (relPosition1);
			//enemy.transform.rotation = Quaternion.Slerp (player.transform.rotation, quat, (speed + 5) * Time.deltaTime);

			i++;
			// busted ass timer works for now though...
			if (i == 100) {
				if (enemy != null) 
					enemyCont.enabled = !enemyCont.enabled;

				isattackover = true;
				enemyattack = false;
				i= 0;
			}
		}
	}

	void Clicked () {

		if (enemy != null) {
			relPosition = enemy.transform.position - player.transform.position;
			relPosition1 = player.transform.position - enemy.transform.position;
		}

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			isattackover = false;

			if (Physics.Raycast(ray, out hitter)) {

				// player
				if (hitter.transform.renderer.material.mainTexture == Resources.Load("textures/trans-tile-attack")) {
					if (enemy != null) {
						enemyComponent.enabled = !enemyComponent.enabled;

					}

					charController.enabled = !charController.enabled;

					dest = hitter.transform.gameObject;

					playerattack = true;
				}

				// enemy
				if (hitter.transform.renderer.material.mainTexture == Resources.Load("textures/trans-tile-enemy")) {
					playerHealth.health--;
					 
					enemyCont.enabled = !enemyCont.enabled;

					isattackover = false;
					enemyattack = true;
				}
			}
		}
	}
}

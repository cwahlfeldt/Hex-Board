using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private RaycastHit hitter;

	private GameObject player, dest, actions;
	private GameObject[] enemies;
	private CharController charController;
	private CharacterController enemyCont;
	private ArrayList correctship;

	public bool playerattack = false, enemyattack = false, isattackover = false;

	private float speed;

	private Quaternion quat;
	private Vector3 relPosition, relPosition1;

	private PlayerPath playerComponent;
	private EnemyPath enemyComponent;
	private Health playerHealth;
	private All all;

	// this will help decide if the enemy was killed
	public int enemiesKilled;
	private int i = 0;

	// Use this for initialization
	void Start () {
		correctship = new ArrayList ();
		player = GameObject.FindGameObjectWithTag ("Player");
//		enemy = GameObject.FindGameObjectWithTag ("Enemy");
		actions = GameObject.Find ("Actions");
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		all = actions.GetComponent<All> ();

		speed = 7f;

//		enemyComponent = enemy.GetComponent<EnemyPath> ();
//		enemyCont = enemy.GetComponent<CharacterController> ();
		charController = player.GetComponent<CharController> ();
		playerHealth = player.GetComponent<Health> ();

	}
	
	// Update is called once per frame
	void Update () {

		foreach (GameObject enemy in enemies) {
			i++;
			//print (i + " " + Vector3.Distance(enemy.transform.position, player.transform.position));

			if (Vector3.Distance(enemy.transform.position, player.transform.position) <= 3.2)
				correctship.Add(enemy);
			else
				correctship.Clear ();

			foreach (GameObject ship in correctship) {
				if (ship != null) {
					ship.GetComponent<CharacterController> ().enabled = !ship.GetComponent<CharacterController> ().enabled;
				}
			}
		}
//
//		}
//		Clicked ();
//
//		// for player attack only
//		if (playerattack == true) {
//			quat = Quaternion.LookRotation (relPosition);
//			player.transform.position = Vector3.Lerp (player.transform.position, dest.transform.position, speed * Time.deltaTime);
//			player.transform.rotation = Quaternion.Slerp (player.transform.rotation, quat, (speed + 5) * Time.deltaTime);
//
//			// custom wait timer 26 higher the number longer it takes to kill enemy
//			i++;
//			if (i == 40) {
//				charController.enabled = !charController.enabled;
//				charController.isontile = false;
//				Destroy (enemy);
//				playerattack = false;
//				isattackover = true;
//				i = 0;
//			}
//		}
//
//		// for enemy attack
//		if (enemyattack == true) {
//			quat = Quaternion.LookRotation (relPosition1);
//			//enemy.transform.rotation = Quaternion.Slerp (player.transform.rotation, quat, (speed + 5) * Time.deltaTime);
//
//			i++;
//			// busted ass timer works for now though...
//			if (i == 100) {
//				if (enemy != null) 
//					enemyCont.enabled = !enemyCont.enabled;
//
//				isattackover = true;
//				enemyattack = false;
//				i= 0;
//			}
//		}

		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject enemy in all.enemies) {

		}









	}

//	void Clicked () {
//
//		if (enemy != null) {
//			relPosition = enemy.transform.position - player.transform.position;
//			relPosition1 = player.transform.position - enemy.transform.position;
//		}
//
//		if (Input.GetKeyDown (KeyCode.Mouse0)) {
//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//
//			isattackover = false;
//
//			if (Physics.Raycast(ray, out hitter)) {
//
//				// player
//				if (hitter.transform.renderer.material.mainTexture == Resources.Load("textures/trans-tile-attack")) {
//					if (enemy != null) {
//						enemyComponent.enabled = !enemyComponent.enabled;
//
//					}
//
//					charController.enabled = !charController.enabled;
//
//					dest = hitter.transform.gameObject;
//
//					playerattack = true;
//				}
//
//				// enemy
//				if (hitter.transform.renderer.material.mainTexture == Resources.Load("textures/trans-tile-enemy")) {
//					playerHealth.health--;
//					 
//					enemyCont.enabled = !enemyCont.enabled;
//
//					isattackover = false;
//					enemyattack = true;
//				}
//			}
//		}
//	}
}

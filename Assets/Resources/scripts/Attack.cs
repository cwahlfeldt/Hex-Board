using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private RaycastHit hitter;

	private GameObject player, dest, theEnemy, enemy1;
	private GameObject[] enemies;
	private CharController charController;
	private CharacterController enemyCont;
	private ArrayList correctship;

	public bool playerattack = false, enemyattack = false, isattackover = false, attack = false;
	
	private Vector3 relPosition, relPosition1;

	private Health playerHealth;

	// this will help decide if the enemy was killed
	public int enemiesKilled;

	// Use this for initialization
	void Start () {
		correctship = new ArrayList ();
		player = GameObject.FindGameObjectWithTag ("Player");
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		theEnemy = new GameObject ();

		charController = player.GetComponent<CharController> ();
		playerHealth = player.GetComponent<Health> ();

	}
	
	// Update is called once per frame
	void Update () {

		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject enemy in enemies) {
			if (Vector3.Distance(player.transform.position, enemy.transform.position) < 5.5) {
				//print (enemy.name);
				theEnemy = enemy;
			}
		}

		// simple health minus
		if (Input.GetKeyDown (KeyCode.Mouse0) && charController.velocity < 15) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			isattackover = false;

			if (Physics.Raycast (ray, out hitter)) {
				
				// player
				if ( (hitter.transform.GetComponent<Renderer>().material.mainTexture == Resources.Load<Texture> ("textures/trans-tile-enemy") || 
				     hitter.transform.GetComponent<Renderer>().material.mainTexture == Resources.Load<Texture> ("textures/trans-tile-dbl"))) {

					if (theEnemy.transform.GetComponent<Renderer>().material.mainTexture == Resources.Load<Texture> ("textures/vehicle_enemyShip_red_dff"))
						playerHealth.health--;
					else if (theEnemy.transform.GetComponent<Renderer>().material.mainTexture == Resources.Load<Texture> ("textures/vehicle_playerShip_orange_dff")) {
						playerHealth.health -= 2;
					}
				}
			}
		}

		// for player attack
		foreach (GameObject enemy in enemies) {

			if (Vector3.Distance(enemy.transform.position, player.transform.position) <= 3.0)
				correctship.Add(enemy);
			else
				correctship.Clear ();

			foreach (GameObject ship in correctship) {
				if (Input.GetKeyDown(KeyCode.Mouse0) && charController.velocity < 15) {
					if (ship != null && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitter)) {
						if ((hitter.transform.GetComponent<Renderer>().material.mainTexture == Resources.Load<Texture> ("textures/trans-tile-attack") || 
						     hitter.transform.GetComponent<Renderer>().material.mainTexture == Resources.Load<Texture> ("textures/trans-tile-dbl")) &&
						     Vector3.Distance (hitter.transform.position, ship.transform.position) <= 3) {

							ship.GetComponent<EnemyAI> ().enabled = !(ship.GetComponent<EnemyAI> ().enabled);
						
							Destroy (ship);
						}
					}
				}
			}

		}
	
//		if (playerattack == true && enemy1 != null) {
//			i++;
//
//			if (i == 50) {
//				Destroy (enemy1);
//			}
//		}

	} // end of update
	
}







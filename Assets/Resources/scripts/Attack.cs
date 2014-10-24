using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private RaycastHit hitter, hitter1;

	private GameObject player, enemy, dest, actions;
	private CharacterController playerCont, enemyCont;

	public bool attack = false, isattackover = false;

	private float speed;

	private Quaternion quat;
	private Vector3 relPosition;

	private PlayerPath playerComponent;
	private EnemyPath enemyComponent;

	private int i = 0;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
		actions = GameObject.Find ("Actions");

		speed = 7f;

		playerComponent = player.GetComponent<PlayerPath> ();
		enemyComponent = enemy.GetComponent<EnemyPath> ();

		playerCont = player.GetComponent<CharacterController> ();
		enemyCont = enemy.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		Clicked ();

		// for player attack only
		if (attack == true) {
			player.transform.position = Vector3.Lerp (player.transform.position, dest.transform.position, speed * Time.deltaTime);
			player.transform.rotation = Quaternion.Slerp (player.transform.rotation, quat, (speed + 5) * Time.deltaTime);

			// custom wait timer 26 higher the number longer it takes to kill enemy
			i++;
			if (i == 26) {
				Destroy (enemy);
				attack = false;
				playerComponent.enabled = !playerComponent.enabled;
			}

//			if (Physics.Raycast (player.transform.position, player.transform.forward, out hitter1, 3.5f)) {
//				if (hitter1.transform.name == "Enemy") {
//					Destroy (enemy);
//				}
//				
//			}

		}
	}

	void Clicked () {

		relPosition = enemy.transform.position - player.transform.position;
		
		quat = Quaternion.LookRotation (relPosition);

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hitter)) {
				if (hitter.transform.renderer.material.mainTexture == Resources.Load("textures/trans-tile-attack")) {

					enemyComponent.enabled = !enemyComponent.enabled;
					playerComponent.enabled = !playerComponent.enabled;

					dest = hitter.transform.gameObject;

					attack = true;
				}

			}
		}
	}
}

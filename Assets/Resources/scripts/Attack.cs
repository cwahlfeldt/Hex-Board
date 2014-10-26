using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private RaycastHit hitter;

	private GameObject player, enemy, dest, actions;
	private CharController charController;

	public bool attack = false, isattackover = false;

	private float speed;

	private Quaternion quat;
	private Vector3 relPosition;

	private PlayerPath playerComponent;
	private EnemyPath enemyComponent;

	// this will help decide if the enemie was killed
	public int enemiesKilled, playerHealth;
	private int i = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
		actions = GameObject.Find ("Actions");

		speed = 7f;

		enemyComponent = enemy.GetComponent<EnemyPath> ();
		charController = player.GetComponent<CharController> ();

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
			if (i == 30) {
				charController.enabled = !charController.enabled;
				Destroy (enemy);
//				enemy.SetActive(false);
				attack = false;
			}
		}
	}

	void Clicked () {

		if (enemy != null)
			relPosition = enemy.transform.position - player.transform.position;
		
		quat = Quaternion.LookRotation (relPosition);

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hitter)) {
				print (hitter.transform.renderer.material.mainTexture);
				if (hitter.transform.renderer.material.mainTexture == Resources.Load("textures/trans-tile-attack")) {

					if (enemy != null)
						enemyComponent.enabled = !enemyComponent.enabled;

					charController.enabled = !charController.enabled;

					dest = hitter.transform.gameObject;

					print (hitter.transform.renderer.material.mainTexture);

					attack = true;
				}
			}
		}
	}
}

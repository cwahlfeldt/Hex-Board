using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	private GameObject[] tiles, enemies;
	private GameObject player, FrontOfEnemy;
	private ArrayList correctTiles, closestTiles;
	private Vector3 target; 
	private CharController playerCharC;
	public bool turn;
	public float dist = 0;

	// Use this for initialization
	void Start () {
		closestTiles = new ArrayList ();
		correctTiles = new ArrayList ();

		turn = false;

		tiles = GameObject.FindGameObjectsWithTag ("tile");
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		player = GameObject.Find ("Player");
		playerCharC = player.GetComponent<CharController> ();

		string str = transform.name;
		FrontOfEnemy = GameObject.Find ("FrontOfEnemy-" + str);
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.LookAt(player.transform, Vector3.up);

		tiles = GameObject.FindGameObjectsWithTag ("tile");
		playerCharC = player.GetComponent<CharController> ();

		foreach (GameObject tile in tiles) {
			RaycastHit hitter;
			if (Vector3.Distance (FrontOfEnemy.transform.position, tile.transform.position) > 1.6) {
				correctTiles.Add (tile);
			}
		}

		if (Input.GetKeyDown (KeyCode.Mouse0) && playerCharC.velocity < 15) {
			turn = true;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit))
				if (hit.transform.renderer.material.mainTexture == Resources.Load<Texture>("textures/trans-tile-player") ||
				    hit.transform.renderer.material.mainTexture == Resources.Load<Texture>("textures/trans-tile-enemy") ||
				    hit.transform.renderer.material.mainTexture == Resources.Load<Texture>("textures/trans-tile-attack") ){
			
					// causing major problems !!!!!!!
					foreach (GameObject tile in tiles) {
						if (Vector3.Distance (tile.transform.position, FrontOfEnemy.transform.position) < 2.7 && 
						    Vector3.Distance (tile.transform.position, FrontOfEnemy.transform.position) >= 1.7) {

							closestTiles.Add (tile);
							//target = tile.transform.position;
						}
					}
			
					correctTiles.Clear();
			}
		}

		foreach (GameObject closestTile in closestTiles) {
			target = closestTile.transform.position;

			//print (closestTile.name);
		}

		closestTiles.Clear ();
		dist = Vector3.Distance (this.transform.position, target);

		//print (playerCharC.velocity);

		if (Vector3.Distance (this.transform.position, target) < 0) {
			turn = false;
		}


		if (dist < 4.0 && turn == true) {
			transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 6);
		}

	}
}

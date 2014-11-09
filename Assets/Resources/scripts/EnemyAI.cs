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

//		foreach (GameObject tile in tiles) {
//			if (Vector3.Distance (tile.transform.position, FrontOfEnemy.transform.position) < 1.7 && Vector3.Distance (FrontOfEnemy.transform.position, tile.transform.position) > 1.6)
//				
//		}

		foreach (GameObject tile in tiles) {
			RaycastHit hitter;
			if (Vector3.Distance (FrontOfEnemy.transform.position, tile.transform.position) > 1.6) {
				correctTiles.Add (tile);
			}
		}

		if (Input.GetKeyUp(KeyCode.Mouse0)) {

			// causing major problems !!!!!!!
			foreach (GameObject tile in tiles) {
				if (Vector3.Distance (tile.transform.position, FrontOfEnemy.transform.position) < 2.2 && 
				    Vector3.Distance (tile.transform.position, FrontOfEnemy.transform.position) >= 1.7) {

					//print (tile.name);

					closestTiles.Add (tile);
				}
			}
	
			correctTiles.Clear();
			
		}

		foreach (GameObject closestTile in closestTiles) {
			target = closestTile.transform.position;

			print (closestTile.name);
		}

		closestTiles.Clear ();
		dist = Vector3.Distance (this.transform.position, target);

		if (dist < 4.5) {
			transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 6);
		}

	}
}

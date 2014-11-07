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

		// this is causing a problem with multiple enemies
		string str = transform.name;
		FrontOfEnemy = GameObject.Find ("FrontOfEnemy-" + str);
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.LookAt(player.transform, Vector3.up);

		tiles = GameObject.FindGameObjectsWithTag ("tile");

		if (Input.GetKeyDown(KeyCode.Mouse0)) {

//			foreach (GameObject tile in tiles) {
//				RaycastHit hitter;
////				Debug.DrawRay (tile.transform.position, Vector3.up);
//				if (Physics.Raycast (tile.transform.position, Vector3.down, out hitter, 10)) {
////					print ("pressed");
////					print (hitter.transform.name);
//
//					if (hitter.transform.tag != "Enemy" && hitter.transform.name != "Player")
//						correctTiles.Add (tile);
//
//				}
//			}

			foreach (GameObject tile in tiles) {
				if (Vector3.Distance (tile.transform.position, FrontOfEnemy.transform.position) <= 1.5 && 
				    Vector3.Distance (tile.transform.position, FrontOfEnemy.transform.position) > 0 && closestTiles.Count != 2) {

					closestTiles.Add (tile.gameObject);
				}
				else if (closestTiles.Count == 2)
					closestTiles.Clear ();

			}

			foreach (GameObject closestTile in closestTiles) {
				print (closestTile.name);
				target = closestTile.transform.position;
			}

			dist = Vector3.Distance (this.transform.position, target);
			//print (dist);
		
		}

		if (Vector3.Distance (this.transform.position, target) < 4)
			transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 6);

	}
}

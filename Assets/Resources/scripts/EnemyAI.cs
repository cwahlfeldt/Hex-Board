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

		if (Input.GetKeyUp(KeyCode.Mouse0)) {

			correctTiles.Clear();

			foreach (GameObject tile in tiles) {
				RaycastHit hitter;
				//				Debug.DrawRay (tile.transform.position, Vector3.up);
				if (Vector3.Distance (transform.position, tile.transform.position) > 2) {
					//print ("pressed");
					//					print (hitter.transform.name);
					correctTiles.Add (tile);
				}
			}

			foreach (GameObject tile in correctTiles) {
				if (Vector3.Distance (tile.transform.position, FrontOfEnemy.transform.position) <= 1.5 && 
				    Vector3.Distance (tile.transform.position, FrontOfEnemy.transform.position) > 0 && closestTiles.Count != 2) {

					closestTiles.Add (tile.gameObject);
				}
				else if (closestTiles.Count == 2)
					closestTiles.Clear ();

			}

			//print (dist);
		
		}

		foreach (GameObject closestTile in closestTiles) {
			target = closestTile.transform.position;
		}
		
		dist = Vector3.Distance (this.transform.position, target);

		if (Vector3.Distance (this.transform.position, target) < 4)
			transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 6);

	}
}

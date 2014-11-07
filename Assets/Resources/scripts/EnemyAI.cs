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

		tiles = GameObject.FindGameObjectsWithTag ("tile");
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		player = GameObject.Find ("Player");
		playerCharC = player.GetComponent<CharController> ();

		FrontOfEnemy = GameObject.Find ("FrontOfEnemy");
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.LookAt(player.transform, Vector3.up);

		foreach (GameObject tile in tiles) {
			if (Vector3.Distance (tile.transform.position, FrontOfEnemy.transform.position) <= 1.6 && 
			    Vector3.Distance (tile.transform.position, FrontOfEnemy.transform.position) > 0 && closestTiles.Count != 2) {

				closestTiles.Add (tile);
			}
			else if (closestTiles.Count == 2)
				closestTiles.Clear ();

		}

		foreach (GameObject closestTile in closestTiles) {
			print (closestTile.name);
			target = closestTile.transform.position;
		}

		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			dist = Vector3.Distance (transform.position, target);
		}

		if (dist <= 2.4 && dist != 0)
			transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 3);
		else
			return;
	}
}

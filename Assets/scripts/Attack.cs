using UnityEngine;
using System.Collections;

// handles who is attacked and when currently attacking just destroys enemies
public class Attack : MonoBehaviour {
	//gets current player and enemy objects
	private GameObject player, enemy;

	// current enemy tile and player tile
	private GameObject playerTile, enemyTile;

	private CharacterController playerCC, enemyCC;

	private ArrayList neighborTiles;

	// ray cast hit i like to call hitter....
	private RaycastHit hitter;
	private PlayerPath pp;
	private EnemyPath ep;

	private float dist;

	private bool attackRange;

	private GameObject[] tiles;

	// Use this for initialization
	void Start () {

		// initializes new array list the will get populated by the neighbor tiles
		neighborTiles = new ArrayList();

		tiles = GameObject.FindGameObjectsWithTag("tile");

		// initializes the two game objects player and enemy
		player = GameObject.FindGameObjectWithTag("Player");
		enemy = GameObject.FindGameObjectWithTag("Enemy");

		playerCC = player.GetComponent<CharacterController> ();
		//enemyCC = enemy.GetComponent<CharacterController> ();

		pp = player.GetComponent<PlayerPath> ();
		ep = enemy.GetComponent<EnemyPath> ();

		playerTile = GameObject.Find("child-piece1");
		enemyTile = GameObject.Find("child-piece25");

		// intializes the current tile
		CurrentTileOnBoard(player);
		CurrentTileOnBoard(enemy);

		GetNeighborTiles(playerTile);
	}

	// Update is called once per frame
	void Update () {

		// always gets tile on update
		CurrentTileOnBoard(player);
		CurrentTileOnBoard(enemy);

		#region  Finds neighbor tiles in realtime. A little buggy
		if (playerCC.velocity.magnitude <= 1) {
			neighborTiles.Clear();

			GetNeighborTiles(playerTile);
			foreach (GameObject neighbors in neighborTiles) {

				// change the texture on neibors then 
				neighbors.SetActive(false);
			}
		}
		else {
			foreach (GameObject neighbors in neighborTiles) {
				neighbors.SetActive(true);
			}
		}
		#endregion

		//GetNeighborTiles(enemyTile);

		dist = Mathf.Round(Vector3.Distance(playerTile.transform.position, enemyTile.transform.position));

		print (dist);

		// this works!!!!!!!! HORRIBLY
		if (dist == 0 && pp.turn == true)
			enemy.SetActive(false);
		else if (dist == 0 && ep.turn == true)
			player.SetActive(false);

	}
	#region GetNeighborTiles (GameObject insertTile) insertTile will == player or enemy tiles! 
	void GetNeighborTiles (GameObject insertTile) {

		foreach (GameObject theTiles in tiles) {
			if (Vector3.Distance(insertTile.transform.position, theTiles.transform.position) <= 3 && (
				Vector3.Distance(insertTile.transform.position, theTiles.transform.position) != 0)) {
				neighborTiles.Add (theTiles);
			}
		}
	}
	#endregion

	#region CurrentTileOnBoard Function
	void CurrentTileOnBoard (GameObject go) {
		if (Physics.Raycast (go.transform.position, Vector3.down, out hitter, 10f)) {
			if (go.tag == "Player")
				playerTile = hitter.transform.gameObject;
			else if (go.tag == "Enemy")
				enemyTile = hitter.transform.gameObject;
		}
	}
	#endregion

	void InAttackRange () {
		//if ()
	}
}

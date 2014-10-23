using UnityEngine;
using System.Collections;

// handles who is attacked and when currently attacking just destroys enemies
public class NeighborTiles : MonoBehaviour {
	//gets current player and enemy objects
	private GameObject player, enemy;

	// current enemy tile and player tile
	private GameObject playerTile, enemyTile;

	private CharacterController playerCC, enemyCC;
	
	// ray cast hit i like to call hitter....
	private RaycastHit hitter;
	private PlayerPath pp;
	private EnemyPath ep;

	private float dist;

	private bool attackRange;

	private ArrayList neighborTiles, attackTiles, playerAttackTiles;

	private GameObject[] tiles;

	// make this equal to a texture file at startup !!!!!!plz!!!!!!
	public Texture texture;

	// Use this for initialization
	void Start () {

		// initializes new array list the will get populated by the neighbor tiles
		neighborTiles = new ArrayList ();
		attackTiles = new ArrayList ();
		playerAttackTiles = new ArrayList ();

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

		#region Finds neighbor tiles in realtime!! fucking awesome!!!
		if (playerCC.velocity.magnitude <= 1) {
			if (neighborTiles != null)
				neighborTiles.Clear();

			GetNeighborTiles(playerTile);
			// sets players range tiles
			foreach (GameObject neighbors in neighborTiles) {

				// change the texture on neibors then 
				neighbors.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile-player");
			}

			// for enemy attack
			foreach (GameObject neighbors in neighborTiles) {

				if (Vector3.Distance(playerTile.transform.position, neighbors.transform.position) <= 3 &&
				    Vector3.Distance(enemyTile.transform.position, neighbors.transform.position) <= 3 &&
				    Vector3.Distance(playerTile.transform.position, enemyTile.transform.position) > 3) {

					attackTiles.Add (neighbors);
					foreach (GameObject atkneighbors in attackTiles)
						atkneighbors.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile-enemy");
				}
				else
					attackTiles.Clear ();
			} 
			// end of enemy attack

			// for player attack
			foreach (GameObject neighbors in neighborTiles) {

				if (Vector3.Distance(playerTile.transform.position, neighbors.transform.position) <= 3 &&
				    Vector3.Distance(enemyTile.transform.position, neighbors.transform.position) <= 3 &&
				    Vector3.Distance(playerTile.transform.position, enemyTile.transform.position) <= 3) {

					playerAttackTiles.Add(neighbors);
					foreach (GameObject playerAtkTiles in playerAttackTiles)
						playerAtkTiles.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile-attack");
				}
				else
					playerAttackTiles.Clear ();
			} 
			// end of player attack

		}

		// or else it just sets the material back to the default hex material
		else {
			foreach (GameObject neighbors in neighborTiles) 
				neighbors.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile");

		}
		#endregion

		//GetNeighborTiles(enemyTile);

		dist = Mathf.Round(Vector3.Distance(playerTile.transform.position, enemyTile.transform.position));

		// print (dist);

//		// this works!!!!!!!! HORRIBLY
//		if (dist == 0 && pp.turn == true)
//			enemy.SetActive(false);
//		else if (dist == 0 && ep.turn == true)
//			player.SetActive(false);

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

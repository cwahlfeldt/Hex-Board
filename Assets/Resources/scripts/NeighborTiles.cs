using UnityEngine;
using System.Collections;

// handles who is attacked and when currently attacking just destroys enemies
public class NeighborTiles : MonoBehaviour {
	//gets current player and enemy objects
	private GameObject player;
	private GameObject[] enemies;

	// current enemy tile and player tile
	public GameObject playerTile, enemyTile;

	private CharController playerCharC;

	// ray cast hit i like to call hitter....
	private RaycastHit hitter;

	private bool attackRange;

	private ArrayList neighborTiles, attackTiles, playerAttackTiles, dKillEnemies, enemyTiles;

	private GameObject[] tiles;

	private Attack atk;

	// Use this for initialization
	#region Start ()
	void Start () {

		// initializes new array list the will get populated by the neighbor tiles
		neighborTiles = new ArrayList ();
		attackTiles = new ArrayList ();
		playerAttackTiles = new ArrayList ();
		dKillEnemies = new ArrayList ();
		enemyTiles = new ArrayList ();

		atk = GameObject.Find ("Actions").GetComponent<Attack> ();
		atk.playerattack = false;

		tiles = GameObject.FindGameObjectsWithTag("tile");

		// initializes the two game objects player and enemy
		player = GameObject.FindGameObjectWithTag("Player");
		enemies = GameObject.FindGameObjectsWithTag("Enemy");

		playerCharC = player.GetComponent<CharController> ();

		playerTile = GameObject.Find("child-piece1");
		enemyTile = GameObject.Find("child-piece25");

		// intializes the current tile
		CurrentTileOnBoard(player);
//		CurrentTileOnBoard(enemy); 

		GetNeighborTiles(playerTile);
	}
	#endregion
	
	void Update () {

		// always gets tile on update
		CurrentTileOnBoard(player);
	
		// this is the jam!!!
		NeighborFinder ();

	}

	#region NeighborFinder () = Finds neighbor tiles in realtime!! fucking awesome!!!
	void NeighborFinder () {

		if (neighborTiles != null )
			neighborTiles.Clear();
		
		GetNeighborTiles(playerTile);

		// sets players range tiles
		foreach (GameObject neighbors in neighborTiles) {
			neighbors.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile-player");
		}
		
		// for player attack
		foreach (GameObject enemy in enemies) {
			foreach (GameObject neighbors in neighborTiles) {
				
				if (Vector3.Distance(playerTile.transform.position, neighbors.transform.position) <= 3 &&
				    Vector3.Distance(enemy.transform.position, neighbors.transform.position) <= 3 &&
				    Vector3.Distance(playerTile.transform.position, enemy.transform.position) <= 3 && atk.playerattack != true && enemy != null) {
					
					playerAttackTiles.Add(neighbors);

					foreach (GameObject playerAtkTiles in playerAttackTiles)
						playerAtkTiles.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile-attack");

				}
				else
					playerAttackTiles.Clear ();
			} 
		}
		// end of player attack



		// for enemy attack
		foreach (GameObject enemy in enemies) {
			foreach (GameObject neighbors in neighborTiles) {
				
				if (Vector3.Distance(enemy.transform.position, neighbors.transform.position) <= 3 &&
				    Vector3.Distance(enemy.transform.position, neighbors.transform.position) <= 3 &&
				    (Mathf.Round(Vector3.Distance(playerTile.transform.position, enemy.transform.position)) == 5 ||
				 Mathf.Round(Vector3.Distance(playerTile.transform.position, enemy.transform.position)) == 4)
				    && enemy != null) {
					
					attackTiles.Add (neighbors);
					foreach (GameObject atkneighbors in attackTiles)
						atkneighbors.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile-enemy");
				}
				else
					attackTiles.Clear ();
			} 
		}

		// end of enemy attack

//		foreach(GameObject GreenTile in playerAttackTiles) {
//			RaycastHit hitterBaby;
//			if (Physics.Raycast(GreenTile.transform.position, Vector3.up, out hitterBaby, 5f)) {
//				foreach (GameObject EnemyShip in enemies){
//					if (hitterBaby.transform.name == EnemyShip.transform.name) {
//						dKillEnemies.Add(EnemyShip);
//					}
//				}
//			}
//		}

		// updates based on current positions
		if (playerCharC.isontile == false) {
			ReUp ();
			foreach (GameObject neighbors in neighborTiles) 
				neighbors.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile");
		}
		foreach (GameObject enemy in enemies) {
			RaycastHit hitterboi;
			if (Physics.Raycast (enemy.transform.position, Vector3.down, out hitterboi, 5f)) {
				if (hitterboi.transform.renderer.material.mainTexture == Resources.Load<Texture>("textures/trans-tile-attack") ||
				    hitterboi.transform.renderer.material.mainTexture == Resources.Load<Texture>("textures/trans-tile-enemy")) {
					enemyTiles.Add (hitterboi.transform.gameObject);
				}
				else
					enemyTiles.Clear ();
			}
		}
		foreach (GameObject et in enemyTiles) {
			et.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile-player");
		}

	}// end of update
	#endregion



	// clears the entire board
	void ReUp () {
		foreach (GameObject thetile in tiles) 
			thetile.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile");
	}

	#region GetNeighborTiles (GameObject insertTile) insertTile will == player or enemy tiles! 
	void GetNeighborTiles (GameObject insertTile) {

		foreach (GameObject theTiles in tiles) {
			if (Vector3.Distance(insertTile.transform.position, theTiles.transform.position) <= 3 && (
				Vector3.Distance(insertTile.transform.position, theTiles.transform.position) != 0) && theTiles) {
				ReUp ();
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
}

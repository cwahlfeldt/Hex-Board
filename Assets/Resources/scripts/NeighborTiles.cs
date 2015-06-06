using UnityEngine;
using System.Collections;

// *****!!!!****!!!!!
// This has to be attached to a character, for it to work. Whether it be the PLAYER or AI.
// *****!!!!****!!!!!

// handles who is attacked and when currently attacking just destroys enemies
public class NeighborTiles : MonoBehaviour {
	//gets current player and enemy objects
	private GameObject player , enemy;
	private GameObject[] enemies;

	// current enemy tile and player tile
	public GameObject playerTile, enemyTile;

	private CharController playerCharC;

	// ray cast hit i like to call hitter....
	private RaycastHit hitter;

	private bool attackRange;

	private ArrayList neighborTiles, attackTiles, playerAttackTiles, longRangeTiles;

	private GameObject[] tiles;

	private Attack atk;

	private float maxDistance = 3f, sentient = 3f;

	private LongRangedAttack LRangeAtk;

	// Use this for initialization
	#region Start ()
	void Start () {

		// initializes new array list the will get populated by the neighbor tiles
		neighborTiles = new ArrayList ();
		attackTiles = new ArrayList ();
		playerAttackTiles = new ArrayList ();
		longRangeTiles = new ArrayList ();

		atk = GameObject.Find ("Actions").GetComponent<Attack> ();
		atk.playerattack = false;

		tiles = GameObject.FindGameObjectsWithTag("tile");

		// initializes the two game objects player and enemy
		player = GameObject.FindGameObjectWithTag("Player");
		enemies = GameObject.FindGameObjectsWithTag("Enemy");

		enemy = this.transform.gameObject;

		playerCharC = player.GetComponent<CharController> ();

		playerTile = GameObject.Find("child-piece1");
		enemyTile = GameObject.Find("child-piece25");

		LRangeAtk = GameObject.Find ("Actions").GetComponent<LongRangedAttack> ();

		// intializes the current tile
		CurrentTileOnBoard(player);
//		CurrentTileOnBoard(enemy); 

		GetNeighborTiles(playerTile);

		foreach (GameObject neighbors in neighborTiles) {
			neighbors.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture>("textures/trans-tile-player");
		}
	}
	#endregion
	
	void Update () {

		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		tiles = GameObject.FindGameObjectsWithTag("tile");

		// always gets tile on update
		CurrentTileOnBoard (player);
	
		// this is the jam!!!
		NeighborFinder ();

		/*
			!!!make the code below for each enemies current tile.!!!!

			!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! re write this !!!!
		 */
		if (LRangeAtk.longrange == true && enemies.Length > 1) {
			foreach (GameObject enemy in enemies) {
				
				foreach (GameObject tile in tiles) {
					if (Vector3.Distance (tile.transform.position, enemy.transform.position) < 2)
						longRangeTiles.Add (tile);
				}
			}
			
			foreach (GameObject enemyTiler in longRangeTiles) {
				if (enemyTiler.GetComponent<Renderer>().material.mainTexture == Resources.Load<Texture>("textures/trans-tile-attack") || 
				    enemyTiler.GetComponent<Renderer>().material.mainTexture == Resources.Load<Texture>("textures/trans-tile-enemy") || 
				    enemyTiler.GetComponent<Renderer>().material.mainTexture == Resources.Load<Texture>("textures/trans-tile-player")) {
					
					enemyTiler.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture> ("textures/trans-tile-long");
				}
			}
			
			//			longRangeTiles.TrimToSize();
			//			int i = 0;
			//			foreach (GameObject tile in longRangeTiles) {
			//				i++;
			//				if (Vector3.Distance(tile.transform.position, enemy.transform.position) > 1) {
			//					longRangeTiles.RemoveAt(i);
			//					longRangeTiles.TrimToSize();
			//					i = 0;
			//				}
			//			}
		} // end of longrange stuff

		if (Input.GetKeyUp (KeyCode.Mouse0))
			sentient = 1;

	} // end of update

	#region NeighborFinder () = Finds neighbor tiles in realtime!! fucking awesome!!!
	void NeighborFinder () {

		if (neighborTiles != null)
			neighborTiles.Clear();
		
		GetNeighborTiles(playerTile);

		// sets players range tiles
		foreach (GameObject enemy in enemies) {
			foreach (GameObject neighbors in neighborTiles) {
				if (enemy != null)
					neighbors.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture>("textures/trans-tile-player");
			}
		}
		
		// for player attack
		foreach (GameObject enemy in enemies) {
			foreach (GameObject neighbors in neighborTiles) {
				
				if (Vector3.Distance(playerTile.transform.position, neighbors.transform.position) <= maxDistance &&
				    Vector3.Distance(enemy.transform.position, neighbors.transform.position) <= maxDistance &&
				    Vector3.Distance(playerTile.transform.position, enemy.transform.position) <= maxDistance && 
				    atk.playerattack != true && enemy != null) {
					
					playerAttackTiles.Add(neighbors);

					foreach (GameObject playerAtkTiles in playerAttackTiles) {
						if (playerAtkTiles != null)
							playerAtkTiles.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture>("textures/trans-tile-attack");
					}
				}
				else
					playerAttackTiles.Clear ();
			} 
		}
		// end of player attack

		// for enemy attack
		foreach (GameObject enemy in enemies) {
			foreach (GameObject neighbors in neighborTiles) {
				
				if (Vector3.Distance(enemy.transform.position, neighbors.transform.position) <= 4 &&
				    ((Mathf.Round(Vector3.Distance(playerTile.transform.position, enemy.transform.position))) == 5 ||
					(Mathf.Round(Vector3.Distance(playerTile.transform.position, enemy.transform.position))) == 4) && 
				    enemy != null) {
					
					attackTiles.Add (neighbors);
					foreach (GameObject atkneighbors in attackTiles) {
						if (atkneighbors.GetComponent<Renderer>().material.mainTexture == Resources.Load<Texture>("textures/trans-tile-attack"))
							atkneighbors.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture>("textures/trans-tile-dbl");
						else
							atkneighbors.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture>("textures/trans-tile-enemy");
					}
				}
				else
					attackTiles.Clear ();
			} 
		} // end of enemy attack



		// updates based on current positions
		if (playerCharC.isontile == false) {
			longRangeTiles.Clear();
			ReUp ();
			foreach (GameObject neighbors in neighborTiles) 
				neighbors.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture>("textures/trans-tile");
		}

		foreach (GameObject enemy in enemies) {
			if (enemy == null)
				ReUp ();
		}
			
	}
	#endregion

	// clears the entire board
	void ReUp () {
		foreach (GameObject thetile in tiles) {
			if (thetile != null) 
				thetile.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture> ("textures/trans-tile");
		}
	}

	#region GetNeighborTiles (GameObject insertTile) insertTile will == player or enemy tiles! 
	void GetNeighborTiles (GameObject insertTile) {
	
		foreach (GameObject theTiles in tiles) {
			
			if ((playerCharC.ftl == true || LRangeAtk.longrange == true))
				maxDistance = 9;
			else 
				maxDistance = 3;

			if ((theTiles != null) && 
			    Vector3.Distance(insertTile.transform.position, theTiles.transform.position) <= maxDistance && (
				Vector3.Distance(insertTile.transform.position, theTiles.transform.position) != 0)) {
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

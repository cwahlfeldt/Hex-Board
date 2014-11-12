using UnityEngine;
using System.Collections;

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

	private ArrayList neighborTiles, attackTiles, playerAttackTiles, enemyTiles, dblAtkTiles;

	private GameObject[] tiles;

	private Attack atk;

	private float maxDistance = 3f;

	// Use this for initialization
	#region Start ()
	void Start () {

		// initializes new array list the will get populated by the neighbor tiles
		neighborTiles = new ArrayList ();
		attackTiles = new ArrayList ();
		playerAttackTiles = new ArrayList ();
		enemyTiles = new ArrayList ();
		dblAtkTiles = new ArrayList ();

		atk = GameObject.Find ("Actions").GetComponent<Attack> ();
		atk.playerattack = false;

		tiles = GameObject.FindGameObjectsWithTag("tile");

		// initializes the two game objects player and enemy
		player = GameObject.FindGameObjectWithTag("Player");
		enemies = GameObject.FindGameObjectsWithTag("Enemy");

		enemy = transform.gameObject;

		playerCharC = player.GetComponent<CharController> ();

		playerTile = GameObject.Find("child-piece1");
		enemyTile = GameObject.Find("child-piece25");

		// intializes the current tile
		CurrentTileOnBoard(player);
//		CurrentTileOnBoard(enemy); 

		GetNeighborTiles(playerTile);

		foreach (GameObject neighbors in neighborTiles) {
			neighbors.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile-player");
		}
	}
	#endregion
	
	void Update () {

		enemies = GameObject.FindGameObjectsWithTag("Enemy");

		// always gets tile on update
		CurrentTileOnBoard (player);
		CurrentTileOnBoard (enemy);
	
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
				
				if (Vector3.Distance(playerTile.transform.position, neighbors.transform.position) <= maxDistance &&
				    Vector3.Distance(enemy.transform.position, neighbors.transform.position) <= maxDistance &&
				    Vector3.Distance(playerTile.transform.position, enemy.transform.position) <= maxDistance && 
				    atk.playerattack != true && enemy != null) {
					
					playerAttackTiles.Add(neighbors);

					foreach (GameObject playerAtkTiles in playerAttackTiles) {
						if (playerAtkTiles != null)
							playerAtkTiles.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile-attack");
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
						if (atkneighbors.renderer.material.mainTexture == Resources.Load<Texture>("textures/trans-tile-attack"))
							atkneighbors.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile-dbl");
						else
							atkneighbors.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile-enemy");
					}
				}
				else
					attackTiles.Clear ();
			} 
		} // end of enemy attack

		// updates based on current positions
		if (playerCharC.isontile == false) {
			ReUp ();
			foreach (GameObject neighbors in neighborTiles) 
				neighbors.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile");
		}

	}// end of update
	#endregion

	// clears the entire board
	void ReUp () {
		foreach (GameObject thetile in tiles) {
			if (thetile != null) 
				thetile.renderer.material.mainTexture = Resources.Load<Texture> ("textures/trans-tile");
		}
	}

	#region GetNeighborTiles (GameObject insertTile) insertTile will == player or enemy tiles! 
	void GetNeighborTiles (GameObject insertTile) {
	
		foreach (GameObject theTiles in tiles) {
			
			if (playerCharC.ftl == true)
				maxDistance = 7;
			else 
				maxDistance = 3;

			if ((theTiles != null) && Vector3.Distance(insertTile.transform.position, theTiles.transform.position) <= maxDistance && (
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

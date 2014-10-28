using UnityEngine;
using System.Collections;

// handles who is attacked and when currently attacking just destroys enemies
public class NeighborTiles : MonoBehaviour {
	//gets current player and enemy objects
	private GameObject player, enemy;

	// current enemy tile and player tile
	private GameObject playerTile, enemyTile;

	private CharController playerCharC;

	// ray cast hit i like to call hitter....
	private RaycastHit hitter;

	private bool attackRange;

	private ArrayList neighborTiles, attackTiles, playerAttackTiles;

	private GameObject[] tiles;

	private Attack atk;

	// Use this for initialization
	#region Start ()
	void Start () {

		// initializes new array list the will get populated by the neighbor tiles
		neighborTiles = new ArrayList ();
		attackTiles = new ArrayList ();
		playerAttackTiles = new ArrayList ();

		atk = GameObject.Find ("Actions").GetComponent<Attack> ();
		atk.playerattack = false;

		tiles = GameObject.FindGameObjectsWithTag("tile");

		// initializes the two game objects player and enemy
		player = GameObject.FindGameObjectWithTag("Player");
		enemy = this.gameObject;

		playerCharC = player.GetComponent<CharController> ();

		playerTile = GameObject.Find("child-piece1");
		enemyTile = GameObject.Find("child-piece25");

		// intializes the current tile
		CurrentTileOnBoard(player);
		CurrentTileOnBoard(enemy); 

		GetNeighborTiles(playerTile);
	}
	#endregion
	
	void Update () {

		// always gets tile on update
		CurrentTileOnBoard(player);

		if (enemy != null)
				CurrentTileOnBoard(enemy);
	
		// this is the jam!!!
		NeighborFinder ();

		if (atk.isattackover == true)
			ReUp ();
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
		
		// for enemy attack
		foreach (GameObject neighbors in neighborTiles) {
			
			if (Vector3.Distance(playerTile.transform.position, neighbors.transform.position) <= 3 &&
			    Vector3.Distance(enemyTile.transform.position, neighbors.transform.position) <= 3 &&
			   	(Mathf.Round(Vector3.Distance(playerTile.transform.position, enemyTile.transform.position)) == 5 ||
			 	Mathf.Round(Vector3.Distance(playerTile.transform.position, enemyTile.transform.position)) == 4)
			  && enemy != null) {
				
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
			    Vector3.Distance(playerTile.transform.position, enemyTile.transform.position) <= 3 && atk.playerattack != true && enemy != null) {
				
				playerAttackTiles.Add(neighbors);
				foreach (GameObject playerAtkTiles in playerAttackTiles)
					playerAtkTiles.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile-attack");
			}
			else
				playerAttackTiles.Clear ();
		} 
		// end of player attack

		// updates based on current positions
		if (playerCharC.isontile == false) {
			foreach (GameObject neighbors in neighborTiles) 
				neighbors.renderer.material.mainTexture = Resources.Load<Texture>("textures/trans-tile");
		}
	}
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

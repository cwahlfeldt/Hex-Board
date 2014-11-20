using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

	#region Global Variables
	private int i = 0;
	private GameObject[] pieces = new GameObject[49];
	private GameObject[] planets;
	private GameObject[] enemies;
	private ArrayList tiles;
	#endregion

	void Start () {
		tiles = new ArrayList ();
		planets = GameObject.FindGameObjectsWithTag ("planet");

		for (int i = 0; i < pieces.Length; i++) {
			GameObject tile = GameObject.Find ("piece" + (i + 1).ToString());
			pieces[i] = tile;
		}
	}

	#region Update
	void Update () {
		// using i as a timer, because this function is basically a loop
		planets = GameObject.FindGameObjectsWithTag ("planet");

		i++;

		// creates the ranodom board
		if (i == 8)
			BoardGenerator ();

		if (i == 13)
			PlanetGenerator ();

		// disables this component for memory managment
		if (i == 15)
			this.enabled = !this.enabled;

	}
	#endregion

	#region BoardGenerator
	public void BoardGenerator () {
		// gets and stores all of the game objects by tag and destroys the specified game object

		// base randos range
		int rando0 = (int)Random.Range (12f, 18f);
		int rando1 = (int)Random.Range (24f, 28f);
		int rando2 = (int)Random.Range (32f, 37f);

		// additive randos
		int rando3 = (int)Random.Range (12f, 18f);
		int rando4 = (int)Random.Range (12f, 18f);
		int rando5 = (int)Random.Range (24f, 28f);
		int rando6 = (int)Random.Range (24f, 28f);
		int rando7 = (int)Random.Range (32f, 37f);
		int rando8 = (int)Random.Range (32f, 37f);
		int rando9 = (int)Random.Range (32f, 37f);
		
		Destroy (pieces [rando0]);
		Destroy (pieces [rando1]);
		Destroy (pieces [rando2]);
		Destroy (pieces [rando3]);
		Destroy (pieces [rando4]);
		Destroy (pieces [rando5]);
		Destroy (pieces [rando6]);
		Destroy (pieces [rando7]);
		Destroy (pieces [rando8]);
		Destroy (pieces [rando9]);

	}
	#endregion

	#region PlanetGenerator 
	// rewirte this!!!!!
	/*
	
		fuckk this
		
	 */
	public void PlanetGenerator () {

		int randomNum = 6;

		ArrayList randoTiles = new ArrayList ();

		for (int e = 0; e < 9; e++) {

			if (e < 4)
				randomNum = (int)Random.Range(4f, 24f);
			else
				randomNum = (int)Random.Range(25f, 49f);

			if (pieces[randomNum] != null)
				randoTiles.Add (pieces[randomNum]);
				
		}

		randoTiles.TrimToSize ();

		int i = 0;

	 	foreach (GameObject tile in randoTiles) {

			GameObject planet = planets[i];
			Vector3 planetSpawn = new Vector3 (tile.transform.position.x, tile.transform.position.y - 1f, tile.transform.position.z);
			
			GameObject.Instantiate (planet, planetSpawn, planet.transform.rotation);

			i++;
			if (i >= 8) {
				i = 0 ;
			}

		}
	}
	#endregion

}

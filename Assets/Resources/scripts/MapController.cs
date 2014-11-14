using UnityEngine;
using System.Collections;
using Pathfinding;

public class MapController : MonoBehaviour {

	#region Global Variables
	private int i = 0;
	private GameObject[] pieces;
	private GameObject[] planets;
	private GameObject[] enemies;
	#endregion

	#region Update
	void Update () {
		// using i as a timer, because this function is basically a loop
		i++;

		// creates the ranodom board
		if (i == 8)
			BoardGenerator ();
		// creates random planets
		if (i == 10)
			PlanetGenerator ();
		// disables this component for memory managment
		if (i == 15)
			this.enabled = !this.enabled;
	}
	#endregion

	#region BoardGenerator
	public void BoardGenerator () {
		// gets and stores all of the game objects by tag and destroys the specified game object
		pieces = GameObject.FindGameObjectsWithTag("pieceTag");
		
		int rando = (int)Random.Range(18f, 50f);
		int rando1 = (int)Random.Range(18f, 50f);
		int rando3 = (int)Random.Range(18f, 50f);
		int rando5 = (int)Random.Range(18f, 50f);
		int rando6 = (int)Random.Range(0f, 1f);
		int rando7 = (int)Random.Range(0f, 1f);
		int rando8 = (int)Random.Range(18f, 50f);
		int rando9 = (int)Random.Range(5f, 50f);
		int rando10 = (int)Random.Range(12f, 50f);
		
		Destroy (pieces [rando]);
		Destroy (pieces [rando1]);
		Destroy (pieces [rando3]);
		Destroy (pieces [rando5]);
		Destroy (pieces [rando6]);
		Destroy (pieces [rando7]);
		Destroy (pieces [rando7]);
		Destroy (pieces [rando10]);
		Destroy (pieces [rando9]);
		Destroy (pieces [rando8]);
		
		planets = GameObject.FindGameObjectsWithTag ("planet");
		
		foreach (GameObject planet in planets)
			print (planet.name);
	}
	#endregion

	#region PlanetGenerator
	public void PlanetGenerator () {
		pieces = GameObject.FindGameObjectsWithTag ("pieceTag");

		ArrayList randoTiles = new ArrayList ();

		for (int e = 1; e <= 8; e++) {
			int randomNum = (int)Random.Range(10,40);

			if (pieces[randomNum] != null)
				randoTiles.Add (pieces[randomNum]);
		}

		int i = 0;

	 	foreach (GameObject tile in randoTiles) {
			//int randomNum = (int)Random.Range(0,6);

			GameObject planet = planets[i];
			i++;
			if (i == 8)
				i = 0 ;

			Vector3 planetSpawn = new Vector3 (tile.transform.position.x, tile.transform.position.y - 1f, tile.transform.position.z);

			GameObject.Instantiate (planet, planetSpawn, planet.transform.rotation);
		}
	}
	#endregion

}

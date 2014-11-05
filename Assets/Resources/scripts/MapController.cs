using UnityEngine;
using System.Collections;
using Pathfinding;

public class MapController : MonoBehaviour {

	private int i = 0;
	private GameObject[] pieces;
	private GameObject[] planets;

	// Use this for initialization
	void Start () {

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

	void Update () {
		i++;

		if (i == 2) {
			AstarPath.active.Scan ();
			PlanetGenerator ();
		}
	}

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
			int randomNum = (int)Random.Range(0,4);

			GameObject planet = planets[randomNum];

			Vector3 planetSpawn = new Vector3 (tile.transform.position.x, tile.transform.position.y - 1f, tile.transform.position.z);

			GameObject.Instantiate (planet, planetSpawn, planet.transform.rotation);
		}



	}

}

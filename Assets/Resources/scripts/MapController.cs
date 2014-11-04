using UnityEngine;
using System.Collections;
using Pathfinding;

public class MapController : MonoBehaviour {

	private int i = 0;

	// Use this for initialization
	void Start () {

		// gets and stores all of the game objects by tag and destroys the specified game object
		GameObject[] pieces = GameObject.FindGameObjectsWithTag("pieceTag");

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

	}

	void Update () {
		i++;

		if (i == 2) {
			AstarPath.active.Scan ();
		}
	}

}

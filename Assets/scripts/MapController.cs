using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {
	

	// Use this for initialization
	void Start () {

		// gets and stores all of the game objects by tag and destroys the specified game object

		GameObject[] pieces = GameObject.FindGameObjectsWithTag("pieceTag");

		int rando = (int)Random.Range(13f, 19f);
		int rando1 = (int)Random.Range(12f, 19f);
		int rando2 = (int)Random.Range(12f, 19f);
		int rando3 = (int)Random.Range(11f, 19f);
		int rando4 = (int)Random.Range(12f, 19f);
		int rando5 = (int)Random.Range(12f, 19f);
		int rando6 = (int)Random.Range(15f, 19f);
		int rando7 = (int)Random.Range(12f, 19f);

//		Destroy (pieces [rando]);
//		Destroy (pieces [rando1]);
//		Destroy (pieces [rando2]);
//		Destroy (pieces [rando3]);
//		Destroy (pieces [rando4]);
//		Destroy (pieces [rando5]);
//		Destroy (pieces [rando6]);
//		Destroy (pieces [rando7]);

	}
}

using UnityEngine;
using System;
using System.Collections;

public class MapController : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// gets and stores all of the game objects by tag and destroys the specified game object

		GameObject[] pieces = GameObject.FindGameObjectsWithTag("pieceTag");

		print(pieces[0]);
		Destroy (pieces [4]);

	}
}

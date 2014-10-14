﻿using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {
	

	// Use this for initialization
	void Start () {

		// gets and stores all of the game objects by tag and destroys the specified game object

		GameObject[] pieces = GameObject.FindGameObjectsWithTag("pieceTag");
		int i = 0;
		foreach (GameObject go in pieces) {
			print (i + ": " + go.name);
			i++;
		}


		int rando = (int)Random.Range(18f, 25f);
		int rando1 = (int)Random.Range(18f, 25f);
		int rando2 = (int)Random.Range(18f, 25f);
		int rando3 = (int)Random.Range(18f, 25f);
		int rando4 = (int)Random.Range(18f, 25f);
		int rando5 = (int)Random.Range(18f, 25f);
		int rando6 = (int)Random.Range(0f, 1f);
		int rando7 = (int)Random.Range(0f, 1f);

		Destroy (pieces [rando]);
		Destroy (pieces [rando1]);
		Destroy (pieces [rando2]);
		Destroy (pieces [rando3]);
		Destroy (pieces [rando4]);
		Destroy (pieces [rando5]);
		Destroy (pieces [rando6]);
		Destroy (pieces [rando7]);
		Destroy (pieces [rando7]);

	}
}

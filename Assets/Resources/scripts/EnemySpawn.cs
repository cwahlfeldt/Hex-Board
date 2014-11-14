﻿using UnityEngine;
using System.Collections;

// spawns enemies in waves
public class EnemySpawn : MonoBehaviour {
	
	private GameObject[] enemies, tiles;
	private GameObject enemy;
	private CharController playerCharC;
	public int lvlOne = 5, lvlTwo = 15, lvlThree = 25, randomNum;

	// Use this for initialization
	void Start () {
		enemy = (GameObject) Resources.Load ("Prefabs/enemy");
		playerCharC = GameObject.Find ("Player").GetComponent<CharController> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (playerCharC.turnCounter == 2)
			tiles = GameObject.FindGameObjectsWithTag("tile");
	
		if (playerCharC.turnCounter == lvlOne) {
			Spawner (lvlOne);
		}
		else if (playerCharC.turnCounter == lvlTwo) {
			Spawner (lvlTwo);

		}
		else
			return;
	
	}

	void Spawner (int waveLevel) {
		if (waveLevel == lvlOne) {
			enemies = new GameObject [2];

			enemies[0] = enemy;
			enemies[1] = enemy;

			lvlOne = 0;
		}
		else if (waveLevel == lvlTwo) {
			enemies = new GameObject[4];

			enemies[0] = enemy;
			enemies[1] = enemy;
			enemies[2] = enemy;
			enemies[3] = enemy;

			lvlTwo = 0;
		}

		if (tiles[randomNum] != null) {
			foreach (GameObject enemy in enemies) {
				randomNum = (int)Random.Range (12, 40);
				enemy.name = "enemy" + randomNum;
				GameObject.Instantiate (enemy, tiles[randomNum].transform.position, enemy.transform.rotation);
			}
		}

	}

} // end of class

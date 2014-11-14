using UnityEngine;
using System.Collections;

// spawns enemies in waves
public class EnemySpawn : MonoBehaviour {
	
	private GameObject[] enemies, tiles;
	private GameObject enemy;
	private CharController playerCharC;
	public int lvlOne = 5, lvlTwo = 10, lvlThree = 20, randomNum;

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
			playerCharC.turnCounter = 1;
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

		if (tiles[randomNum] != null) {
			foreach (GameObject enemy in enemies) {
				randomNum = (int)Random.Range (12, 40);
				enemy.name = "enemy" + randomNum;
				GameObject.Instantiate (enemy, tiles[randomNum].transform.position, enemy.transform.rotation);
			}
		}

	}

} // end of class

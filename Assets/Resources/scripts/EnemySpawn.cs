using UnityEngine;
using System.Collections;

// spawns enemies in waves
public class EnemySpawn : MonoBehaviour {
	
	private GameObject[] enemies, tiles;
	private GameObject enemy;
	private CharController playerCharC;
	public int lvlOne = 5, 
			   lvlTwo = 7, 
	           lvlThree = 9, 
	           randomNum = 0;

	// Use this for initialization
	void Start () {
		enemy = (GameObject) Resources.Load ("Prefabs/enemy");
		playerCharC = GameObject.Find ("Player").GetComponent<CharController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (playerCharC.turnCounter == 2)
			tiles = GameObject.FindGameObjectsWithTag("tile");

		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		if (enemies.Length > 1)
			playerCharC.turnCounter = 1;
	
		if (playerCharC.turnCounter == lvlOne) {
			Spawner (lvlOne);
		}
		else if (playerCharC.turnCounter == lvlTwo) {
			Spawner (lvlTwo);
		}
		else if (playerCharC.turnCounter == lvlThree) {
			Spawner (lvlThree);
		}
		else return;
	}

	void Spawner (int waveLevel) {
		if (waveLevel == lvlOne) {
			enemies = new GameObject [2];

			enemies[0] = enemy;
			enemies[1] = enemy;

			lvlOne = 0;
		}
		else if (waveLevel == lvlTwo) {
			enemies = new GameObject [4];

			enemies[0] = enemy;
			enemies[1] = enemy;
			enemies[2] = enemy;
			enemies[3] = enemy;

			lvlTwo = 0;
		}
		else if (waveLevel == lvlThree) {
			enemies = new GameObject [7];
			
			enemies[0] = enemy;
			enemies[1] = enemy;
			enemies[2] = enemy;
			enemies[3] = enemy;
			enemies[4] = enemy;
			enemies[5] = enemy;
			enemies[6] = enemy;
			
			lvlThree = 0;
		}

		if (tiles[randomNum] != null) {
			foreach (GameObject enemy in enemies) {
				randomNum = (int)Random.Range (5, 40);
				enemy.name = "enemy" + randomNum;
				GameObject.Instantiate (enemy, tiles[randomNum].transform.position, enemy.transform.rotation);
			}
			playerCharC.turnCounter = 1;
		}

	}

} // end of class

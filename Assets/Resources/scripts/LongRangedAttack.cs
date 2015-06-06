using UnityEngine;
using System.Collections;

public class LongRangedAttack : MonoBehaviour {

	private GameObject[] enemies;
	private RaycastHit hitter;
	public bool longrange = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp ("l")) {
			longrange = !longrange;
		}

		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		if (enemies.Length > 1 && longrange == true) {
			foreach (GameObject enemy in enemies) {
				if (Physics.Raycast(enemy.transform.position, Vector3.down, out hitter, 5f)) {
					GameObject enemyTile = (GameObject) hitter.transform.gameObject;

					print (enemyTile.GetComponent<Renderer>().material.mainTexture.name);

					if (enemyTile.GetComponent<Renderer>().material.mainTexture == Resources.Load<Texture> ("textures/trans-tile-attack")) {
						enemyTile.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture> ("textures/trans-tile-long");
					}

				}
			}

		}
		else {
			return;
		}


	}
}

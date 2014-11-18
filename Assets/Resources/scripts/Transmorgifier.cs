using UnityEngine;
using System.Collections;

public class Transmorgifier : MonoBehaviour {

	private GameObject[] meshes;
	private ArrayList enemies;
	private CharController playerCharController;

	// Use this for initialization
	void Start () {
		enemies = new ArrayList ();
		meshes = GameObject.FindGameObjectsWithTag("Enemy");
		playerCharController = GameObject.Find ("Player").GetComponent<CharController> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		GameObject me = this.gameObject;

		meshes = GameObject.FindGameObjectsWithTag("Enemy");

		if (meshes != null) {
			if (playerCharController.isontile == false) {
				foreach (GameObject mesh in meshes) {
					if (mesh != me && mesh.name != "enemy") {
						enemies.Add (mesh);
					}
				}

				foreach (GameObject enemy in enemies) {
					/* 
					 * this causes major problems when printing 
					 * so dont print unless you have to
					 */
					//print ("Distance from "  + me.name + " and " + enemy.name + ": " + Vector3.Distance(enemy.transform.position, me.transform.position));

					if (enemy != null && Vector3.Distance (me.transform.position, enemy.transform.position) < 2) {
						Destroy (enemy);
						MeshFilter meshfilter = me.GetComponent<MeshFilter> ();
						MeshRenderer meshrend = me.GetComponent<MeshRenderer> ();
						meshrend.material.mainTexture = Resources.Load<Texture> ("textures/vehicle_playerShip_orange_dff");
						meshfilter.mesh = Resources.Load<Mesh> ("models/vehicle_playerShip");
					}
				}
			}
		}
	}
}

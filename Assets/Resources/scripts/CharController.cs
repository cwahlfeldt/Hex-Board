﻿using UnityEngine;
using System.Collections;
using Pathfinding;

// Moves the charObject to a tile object when the tile is clicked
public class CharController: MonoBehaviour {

	private float speed;
	public HexGen hexGen;
	public RaycastHit hit, hitter;
	private GameObject go;
	private GameObject[] enemies;
	public Vector3 childPieceLocation;
	private float dist;
	private Vector3 relPosition;
	private Quaternion quat;
	public bool isontile, restrictor;
	private int i = 0;

	// this is how you calculate velocity w/ out a rigid body or character controller
	//public float velocity = (current - previous) / Time.deltaTime;

	void Start() {

		// initializes vars
		restrictor = false;
		isontile = false;
		speed = 8f;
		go = new GameObject ();

		enemies =  GameObject.FindGameObjectsWithTag("Enemy");

	}
	
	void Update() {
		i++;

		if (Physics.Raycast (transform.position, Vector3.down, out hitter, 1f)) {
			if (hitter.transform.name == go.transform.name) {
				isontile = true;
			}
			else
				isontile = false;
		}

		if (i == 2) {
			AstarPath.active.Scan ();
		}

		//finds the ship on update so that it updates the coordinates
		GameObject ship = GameObject.FindGameObjectWithTag("Player");

		// when mouse button is clicked...(for touch controls CHANGE THIS)
		if (Input.GetKeyDown(KeyCode.Mouse0)) {

			// creates a plane for the character and it acts as the 'ground'
			Plane playerPlane = new Plane(Vector3.up, transform.position);

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			
			float hitdist = 0.0f;
			
			// checks for distance to tile and sets it to the target position
			if (playerPlane.Raycast(ray, out hitdist) && Physics.Raycast(ray, out hit) && !hit.transform.Equals("")) {

				// gets current clicked child piece
				go = GameObject.Find(hit.transform.gameObject.name);

				// gets child piece location of current clicked child piece
				childPieceLocation = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z);		

				// gets dist from current click
				dist = Vector3.Distance(ship.transform.position, childPieceLocation);

				// the relative position of ship to the tile location
				relPosition = childPieceLocation - ship.transform.position;

				// sets a quaternion where to rotate basd on the relative position 
				quat = Quaternion.LookRotation (relPosition);

			}
		}

		// draws a ray to the tile clicked
		Debug.DrawRay (ship.transform.position, childPieceLocation - ship.transform.position, Color.green);

		// will only move to one tile at a time.
		if (dist <= 4) {

			// smooth rotation using slerp
			transform.rotation = Quaternion.Slerp(ship.transform.rotation, quat, Time.deltaTime * (speed + 5f));

			// smooth transform position using lerp
			transform.position = Vector3.Lerp(transform.position, childPieceLocation, Time.deltaTime * (speed - 5f));

		}

		// this doesnt work.
//		foreach (GameObject enemy in enemies) {
//			if (enemy == null) {
//				speed = 2f;
//				restrictor = true;
//			}
//		}
		enemies =  GameObject.FindGameObjectsWithTag("Enemy");
		
	} // end of update function
	
} // end of class
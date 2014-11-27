﻿using UnityEngine;
using System.Collections;

// Moves the charObject to a tile object when the tile is clicked
public class CharController: MonoBehaviour {

	private float speed;
	public HexGen hexGen;
	public RaycastHit hit, hitter;
	private GameObject go;
	public Vector3 childPieceLocation;
	private float dist;
	private Vector3 relPosition;
	private Quaternion quat;
	public bool isontile, restrictor, ftl;
	private Bounds bounds;
	public EnemyAI enemyAi;
	private NeighborTiles neighbors;
	public int turnCounter = 1;

	// this is how you calculate velocity w/ out a rigid body or character controller
	public float velocity = 0f;

	void Start() {
	
		// initializes vars
		restrictor = false;
		isontile = true;
		ftl = false;
		speed = 8f;
		go = GameObject.Find ("child-piece1");

		childPieceLocation = GameObject.Find ("piece1").transform.position;

		transform.position = Vector3.Lerp(transform.position, childPieceLocation, Time.deltaTime * (speed - 5f));
	}
	
	void Update() {

		if (Vector3.Distance (childPieceLocation, transform.transform.position) < 2)
				isontile = true;
		else
				isontile = false;

		if (Input.GetKeyUp ("f")) 
			ftl = !ftl;
		
		//finds the ship on update so that it updates the coordinates
		GameObject ship = GameObject.FindGameObjectWithTag("Player");

		velocity = (childPieceLocation.magnitude - ship.transform.position.magnitude) / Time.deltaTime;

		// when mouse button is clicked...(for touch controls CHANGE THIS)
		if (Input.GetKeyUp(KeyCode.Mouse0)) {

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			// checks for distance to tile and sets it to the target position
			if ((Physics.Raycast(ray, out hit)) && !hit.transform.Equals("")) {
				turnCounter++;

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


		//print (Vector3.Distance (transform.position, childPieceLocation) * 8);

		// draws a ray to the tile clicked
		Debug.DrawRay (ship.transform.position, childPieceLocation - ship.transform.position, Color.green);

		float maxDistance;

		if (ftl == true) {
			maxDistance = 9;
		}
		else
			maxDistance = 4.0f;

		// will only move to one tile at a time.
		if (dist <= maxDistance) {

			// smooth rotation using slerp
			transform.rotation = Quaternion.Slerp(ship.transform.rotation, quat, Time.deltaTime * (speed + 5f));

			// smooth transform position using lerp
			transform.position = Vector3.Lerp(transform.position, childPieceLocation, Time.deltaTime * (speed - 5f));

		}

	} // end of update function
	
} // end of class






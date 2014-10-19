﻿using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyPath : MonoBehaviour {
	//The point to move to
	public Vector3 targetPosition;
	
	private Seeker seeker;
	//private GameObject player;
	private CharacterController controller;

	private GameObject go;
	
	//The calculated path
	public Path path;
	
	//The AI's speed per second
	public float speed = 6f;

	public float turningSpeed = 11f;
	
	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = .01f;
	
	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	public RaycastHit hit;
	

	public void Start () {
		seeker = GetComponent<Seeker>();
		//player = this.gameObject;
		controller = GetComponent<CharacterController>();
		targetPosition = new Vector3 (45,0,45);
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);

		// turn stuff
		// fix the turn system!!!!!
	}
	
	public void Update () {

		LeftMouseClick ();

		if (path == null) {
			controller.Move (Vector3.zero);
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count) {
			Debug.Log ("End Of Path Reached");
			controller.Move (Vector3.zero);  
			return;
		}

		//Direction to the next waypoint
		Move ();

		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		EnemyPathChecker ();

	}

	public void LeftMouseClick () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			if (Physics.Raycast (ray, out hit)) {
				go = GameObject.FindGameObjectWithTag("Player");
				
				targetPosition = go.transform.position;
				seeker.StartPath (transform.position, targetPosition, OnPathComplete);
			}
		}
	}

	public void Move () {
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position);
		dir *= speed * Time.fixedDeltaTime;
		RotateTowards (dir);
		controller.Move (dir);
	}

	public void EnemyPathChecker () {
		if (Vector3.Distance (transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			if (currentWaypoint != 1) {
				currentWaypoint++;
			}
			return;
		}
	}

	public void OnPathComplete (Path p) {
		Debug.Log ("Yay, we got a path back. Did it have an error? "+p.error);

		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
		}
	}

	protected virtual void RotateTowards (Vector3 dir) {
		
		if (dir == Vector3.zero) return;
		
		Quaternion rot = transform.rotation;
		Quaternion toTarget = Quaternion.LookRotation (dir);
		
		rot = Quaternion.Slerp (rot,toTarget,turningSpeed*Time.deltaTime);
		Vector3 euler = rot.eulerAngles;
		euler.z = 0;
		euler.x = 0;
		rot = Quaternion.Euler (euler);
		
		transform.rotation = rot;
	}

	public void OnDisable () {
		seeker.pathCallback -= OnPathComplete;
	} 
} 
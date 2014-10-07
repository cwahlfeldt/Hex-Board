﻿using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	// Click To Move script
	// Moves the object towards the mouse position on left mouse click
	
	public int smooth; // Determines how quickly object moves towards position
	private Vector3 targetPosition;
	float speed = 8f;
	HexGen hexGen;

	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			Plane playerPlane = new Plane(Vector3.up, transform.position);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Camera charCamera = GameObject.Find("shipCamera").camera;
			Ray shipRay = charCamera.ScreenPointToRay(Input.mousePosition);
			float hitdist = 0.0f;
			RaycastHit hit, hit1;

				
			 if (playerPlane.Raycast (ray, out hitdist) 
			 && Physics.Raycast(ray, out hit) 
			 && !hit.transform.Equals ("")) {

				GameObject go = GameObject.Find(hit.transform.gameObject.name);

				Vector3 childPieceLocation = new Vector3(go.transform.position.x, go.transform.position.y + 1.3f, go.transform.position.z);

				var targetPoint = childPieceLocation;
				targetPosition = childPieceLocation;

				transform.rotation = Quaternion.LookRotation(targetPoint - transform.position);

				//not working.....
				if (Physics.Raycast(shipRay, out hit1)) {
					print(hit1.point);
				}
			}
		}	
	
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * speed);
	}
}
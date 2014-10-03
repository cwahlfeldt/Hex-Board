using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	// Click To Move script
	// Moves the object towards the mouse position on left mouse click
	
	public int smooth; // Determines how quickly object moves towards position
	private Vector3 targetPosition;
	float speed = 5f;
	HexGen hexGen;
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			Plane playerPlane = new Plane(Vector3.up, transform.position);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float hitdist = 0.0f;
			RaycastHit hit;
				
				if (playerPlane.Raycast (ray, out hitdist) 
			    && Physics.Raycast(ray, out hit) 
			    && !hit.transform.Equals("")) {
					var targetPoint = ray.GetPoint(hitdist);
					targetPosition = ray.GetPoint(hitdist);
					var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
					transform.rotation = targetRotation;
			}
		}	

		transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * speed);
	}
}
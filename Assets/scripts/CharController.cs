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
			 && !hit.transform.Equals ("")) {

				GameObject go = GameObject.Find(hit.transform.gameObject.name);

				Vector3 childPieceLocation = new Vector3(go.transform.position.x, go.transform.position.y + 1.3f, go.transform.position.z);

				var targetPoint = childPieceLocation;
				targetPosition = childPieceLocation;

				transform.rotation = Quaternion.LookRotation(targetPoint - transform.position);

				print(Mathf.Round(hitdist));
			}
		}	

		transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * speed);
	}
}
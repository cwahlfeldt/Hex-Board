using UnityEngine;
using System.Collections;

// Moves the charObject to a tile object when the tile is clicked
public class CharController: MonoBehaviour {

	public float speed;
	public HexGen hexGen;
	public RaycastHit hit, shiphit;
	public GameObject go;
	public Vector3 childPieceLocation;
	public float dist;
	public Vector3 relPosition;
	public Quaternion quat;

	void Start() {

		// initializes speed var
		speed = 8f;

	}
	
	void Update() {

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
				childPieceLocation = new Vector3(go.transform.position.x, go.transform.position.y + 1.3f, go.transform.position.z);		

				// gets dist from current click
				dist = Vector3.Distance(ship.transform.position, childPieceLocation);

				// the relative position of ship to the tile location
				relPosition = childPieceLocation - ship.transform.position;

				// sets a quaternion where to rotate basd on the relative position 
				quat = Quaternion.LookRotation (relPosition);

				print (Mathf.Floor(dist));

			}
			
		}

		// draws a ray to the tile clicked
		Debug.DrawRay (ship.transform.position, childPieceLocation - ship.transform.position, Color.green);

		// will only move to one tile at a time.
		if (dist < 3) {

			// smooth rotation using slerp
			transform.rotation = Quaternion.Slerp(ship.transform.rotation, quat, Time.deltaTime * (speed + 9f));

			// smooth transform position using lerp
			transform.position = Vector3.Lerp(transform.position, childPieceLocation, Time.deltaTime * (speed - 3f));
		}
		
	} // end of update function
	
} // end of class
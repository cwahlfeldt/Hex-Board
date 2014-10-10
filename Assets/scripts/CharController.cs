using UnityEngine;
using System.Collections;

public class CharController: MonoBehaviour {
	
	// Click To Move script
	// Moves the object towards the mouse position on left mouse click
	
	public float smooth; // Determines how quickly object moves towards position
	private Vector3 targetPosition;
	public float speed;
	public HexGen hexGen;
	public RaycastHit hit, shiphit;
	public ArrayList hitDistances;
	//global index for update var basically finds how man clicks when clicking on object
	public int i = 0;
	public Vector3 fwd;
	GameObject go;
	Vector3 childPieceLocation;
	float dist;
	
	void Start() {
		hitDistances = new ArrayList();
		shiphit.distance = 5f;
		hitDistances.Add(2f);
		speed = 8f;
		smooth = 2f;
	}
	
	void Update() {
		GameObject ship = GameObject.FindGameObjectWithTag("Player");
		
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			Plane playerPlane = new Plane(Vector3.up, transform.position);
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Ray ray1 = camera.ScreenPointToRay(Input.mousePosition);
			
			float hitdist = 0.0f;
			
			// checks for distance to tile and sets it to the target position
			// also sets rotation of object
			if (playerPlane.Raycast(ray, out hitdist) && Physics.Raycast(ray, out hit) && !hit.transform.Equals("")) {
				
				go = GameObject.Find(hit.transform.gameObject.name);
				
				childPieceLocation = new Vector3(go.transform.position.x, go.transform.position.y + 1.3f, go.transform.position.z);
				
				fwd = go.transform.TransformDirection(Vector3.forward);
				
				
				var targetPoint = childPieceLocation;
				targetPosition = childPieceLocation;
				

				hitdist = Mathf.Floor(hitdist);
				
				hitDistances.Add(hitdist);
				i++;

				// gets dist from current click
				dist = Vector3.Distance(ship.transform.position, childPieceLocation);
				

				
			}
			
		}

		Vector3 relPosition = childPieceLocation - ship.transform.position;
		Quaternion quat = Quaternion.LookRotation (relPosition);
		Debug.DrawRay(ship.transform.position, childPieceLocation - ship.transform.position, Color.green);
		
		if (dist < 3) {
			transform.rotation = Quaternion.Slerp(ship.transform.rotation, quat, Time.deltaTime * (speed + 9f));
			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * (speed - 3f));

		}
		
	} // end of update function
	
} // end of class
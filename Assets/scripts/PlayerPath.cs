using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use pathfinding
using Pathfinding;
public class PlayerPath : MonoBehaviour {
	//The point to move to
	public Vector3 targetPosition;
	
	private Seeker seeker;
	//private GameObject player;
	private CharacterController controller;
	
	//The calculated path
	public Path path;
	
	//The AI's speed per second
	public float speed = 7f;

	public float turningSpeed = 11f;
	
	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = .01f;
	
	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	public RaycastHit hit;

	public GameObject enemy;

	public void Start () {
		seeker = GetComponent<Seeker>();
		//player = this.gameObject;
		controller = GetComponent<CharacterController>();
		targetPosition = Vector3.zero;
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
	}
	
	public void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			GameObject go;
			
			if (Physics.Raycast (ray, out hit)) {
				go = GameObject.Find (hit.transform.gameObject.name);
				
				targetPosition = go.transform.position;
				seeker.StartPath (transform.position, targetPosition, OnPathComplete);
			}
		}

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
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position);
		dir *= speed * Time.fixedDeltaTime;
		RotateTowards (dir);
		controller.Move (dir);

		enemy = GameObject.FindGameObjectWithTag ("Enemy");

		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			if (currentWaypoint != 1) {
				currentWaypoint++;
				print (Vector3.Distance(transform.position, enemy.transform.position));
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

	public void OnDisable () {
		seeker.pathCallback -= OnPathComplete;
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
} 
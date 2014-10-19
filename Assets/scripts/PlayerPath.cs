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

	// raycast hit for screen to mouse input
	public RaycastHit hit;

	// enemy object
	public GameObject enemy;

	public PlayerTurn pt;

	public EnemyTurn et;

	// checks if in attack range .. see #pathchecker
	public bool inAttackRange;

	public void Awake () {

		// turn stuff
		pt = GetComponent<PlayerTurn> ();
		et = GetComponent<EnemyTurn> ();
		pt.turn = true;
		et.turn = false;
	}

	public void Start () {

		// gets seeker component
		seeker = GetComponent<Seeker>( );

		//player = this.gameObject;
		controller = GetComponent<CharacterController>( );

		// first path is to the starting point of the level
		targetPosition = Vector3.zero;

		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
		inAttackRange = false;
		enemy = GameObject.FindGameObjectWithTag ("Enemy");

	}
	
	public void Update () {

		// starts path when the left mouse is clicked on a tile
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

		if (enemy != null)
			enemy = GameObject.FindGameObjectWithTag ("Enemy");
//		else
//			print ("enemy is fucking dead");

		// Check if we are close enough to the next waypoint
		// If we are, proceed to follow the next waypoint
		// also restricts path and checks attack range
		PathChecker ();

//		if (enemy != null)
//			print (Vector3.Distance(transform.position, enemy.transform.position));

		// Destroys enemy when in attack range.. needs way cooler 'animation' and effects
		Attack ();

	}

	public void LeftMouseClick () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			GameObject go;
			
			if (Physics.Raycast (ray, out hit)) {
				go = GameObject.Find (hit.transform.gameObject.name);
				
				targetPosition = go.transform.position;
				seeker.StartPath (transform.position, targetPosition, OnPathComplete);
			}
		}
	}

	public void PathChecker () {
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			// allows for only one space per 'turn'
			if (currentWaypoint != 1) {
				currentWaypoint++;
				
				// checks if in attack range
				if ((enemy != null) && (Vector3.Distance(transform.position, enemy.transform.position) < 2.75f)) {
					inAttackRange = true;
				}
			}
			return;
		}
	}

	public void Move () {
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position);
		dir *= speed * Time.fixedDeltaTime;
		RotateTowards (dir);
		controller.Move (dir);
	}

	public void Attack () {
		if (inAttackRange) {
			if (Vector3.Distance(transform.position, enemy.transform.position) < 1.90f){
				Destroy (enemy);
				inAttackRange = false;
			}
		}
	}

	public void OnPathComplete (Path p) {
	
		Debug.Log ("Yay, we got a path back. Did it have an error? " + p.error);

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








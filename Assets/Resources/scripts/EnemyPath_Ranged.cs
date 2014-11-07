using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyPath_Ranged : MonoBehaviour {
	
	//The point to move to
	public Vector3 targetPosition;
	
	private Seeker seeker;
	//private GameObject player;
	private CharacterController controller;
	
	private GameObject player, enemySpawn, playerSpawn;
	
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
	
	public bool turn;
	
	private CharController playerCharC;
	
	public void Awake () {
		
		// initializes turn
		turn = false;
		
		// gets seeker component
		seeker = GetComponent<Seeker> ();
		
		//player = this.gameObject;
		controller = GetComponent<CharacterController> ();
		
		player = GameObject.FindGameObjectWithTag("Player");
		playerCharC = player.GetComponent<CharController> ();

		enemySpawn = GameObject.Find ("piece100");
		playerSpawn = GameObject.Find ("piece101");

	}
	
	public void FixedUpdate () {
		
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
		
		// kind of buggy but gives the ai some character...
		if (Vector3.Distance (player.transform.position, playerCharC.childPieceLocation) <= 2.3);
			Move ();
		
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		EnemyPathChecker ();

		NNConstraint nodeConstraint = new NNConstraint();
		nodeConstraint.constrainWalkability = false;
		nodeConstraint.walkable = false;
		
		NNInfo nodeInfo = AstarPath.active.GetNearest (transform.position, nodeConstraint);
		
		if (playerCharC.isontile == true)
			nodeInfo.node.Walkable = false;
		else
			nodeInfo.node.Walkable = true;
	}
	
	public void LeftMouseClick () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			if (Physics.Raycast(ray, out hit)) {
				if (hit.transform.name != "Player") {
					player = GameObject.FindGameObjectWithTag("Player");

					// the new logic for ranged characters
					if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) < 7)
						targetPosition = enemySpawn.transform.position;
					else if (Vector3.Distance(this.gameObject.transform.position, enemySpawn.transform.position) < 5)
						targetPosition = playerSpawn.transform.position;
					else
						targetPosition = player.transform.position;

					if (Vector3.Distance (player.transform.position, playerCharC.childPieceLocation) <= 2)
						seeker.StartPath (transform.position, targetPosition, OnPathComplete);
				}
			}
		}
	}
	
	public void Move () {
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position);
		dir *= speed * Time.fixedDeltaTime;
		RotateTowards (dir);
		
		if (controller.enabled == true)
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

using UnityEngine;
using System.Collections;
using Pathfinding;
public class PlayerMovement : MonoBehaviour {
	private Transform _myT;
	private Seeker seeker;
	private CharacterController controller;
	private Path path;
	private int currentWaypoint = 0;
	public float speed = 50;
	public int nextWaypointDistance = 1;
	private Vector3 targetPosition;
	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker>();
		controller = GetComponent<CharacterController>();
		targetPosition = transform.position;
		_myT = transform;
	}
	// Update is called once per frame
	void Update () {
		DetectMouseClick();
		if (path == null) {      
			controller.Move (Vector3.zero); 
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count) {		
			controller.Move (Vector3.zero);  
			return;
		}
		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		controller.Move (dir * speed);
		Vector3 rot = path.vectorPath[currentWaypoint];
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
	public void OnPathComplete (Path p) {
		if (!p.error) 
		{
			path = p;
			currentWaypoint = 0;
		}		
	}
	//MouseMovementsToTargetALocationOnPlane
	void DetectMouseClick()
	{
		if(Input.GetMouseButtonUp(1)) //Right click to move
		{
			Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));
			RaycastHit[] hitInfos = Physics.RaycastAll(ray);				
			for(int i = 0; i < hitInfos.Length; i++)
			{					
				if (hitInfos[i].transform.tag == "Ground")
				{
					targetPosition  = hitInfos[i].point;
					seeker.StartPath (transform.position,targetPosition, OnPathComplete);
					Debug.Log(targetPosition);
				}
			}
		}
	}
}
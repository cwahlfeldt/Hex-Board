using UnityEngine;
using System.Collections;

public class PlayerTurn : MonoBehaviour {
	
	public AIPath aipath;
	public RaycastHit hit;
	public bool turn;
	public EnemyTurn et; 
	
	void Start () {
		turn = true;
	}
	
	void Update () {

		if (turn == true) {
			
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				GameObject go;
				
				if (Physics.Raycast (ray, out hit)) {
					go = GameObject.Find (hit.transform.gameObject.name);
					
					aipath.target = go.transform;

					turn = false;
					et.turn = true;
				}
			}
		}
	}
}
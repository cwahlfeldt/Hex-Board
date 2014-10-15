using UnityEngine;
using System.Collections;

public class EnemyTurn : MonoBehaviour {
	
	public EnemyAIPath eaipath;
	public RaycastHit hit;
	public bool turn;
	public PlayerTurn pt; 
	
	void Start () {
		turn = false;
	}
	
	void Update () {

		if (turn == true) {
			
			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				GameObject go;
				
				if (Physics.Raycast (ray, out hit)) {
					go = GameObject.Find (hit.transform.gameObject.name);
					
					eaipath.target = go.transform;
					
					turn = false;
					pt.turn = true;
				}
			}
		}
	}
}
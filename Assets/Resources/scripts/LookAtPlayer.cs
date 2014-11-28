using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

	private Vector3 myPosition, playerPosition;
	public float speed = 4f;

	// Update is called once per frame

	void Start () {
		myPosition = this.transform.position;
	}

	void Update () {
		if (GameObject.Find ("Player").gameObject == null)
			return;
		else
			playerPosition = new Vector3 (GameObject.Find ("Player").transform.position.x ,
			                              (-200.12349f),
			                              GameObject.Find ("Player").transform.position.z);

		if (GameObject.Find ("Player").GetComponent<CharController> ().isontile == false) 
			this.transform.position = Vector3.Lerp (this.transform.position, playerPosition, (speed - 3.97f) * Time.deltaTime);
		else
			this.transform.position = Vector3.Lerp (this.transform.position, myPosition, (speed - 2.5f) * Time.deltaTime);
	}
}

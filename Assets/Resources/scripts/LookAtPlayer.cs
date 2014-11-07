using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Player").GetComponent<CharController> ().isontile == false)
			transform.LookAt(GameObject.Find ("Player").transform.position, -Vector3.back);
	}
}

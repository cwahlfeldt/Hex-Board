using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

	// Update is called once per frame
	void Update () {
			transform.LookAt(GameObject.Find ("Player").transform.position, -Vector3.back);
	}
}

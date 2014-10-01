using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out hit)) {
				if (!hit.transform.tag.Equals("Untagged") )Debug.Log( gameObject.tag + " is clicked by mouse");
			}
		}
	}
}

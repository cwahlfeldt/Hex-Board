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
				if (!hit.transform.Equals("")) Debug.Log( hit.transform.gameObject.name + " is clicked by mouse: " + Input.mousePosition.y);
			}
		}
	}
}

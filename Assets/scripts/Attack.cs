using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private RaycastHit hitter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Clicked ();
	}

	void Clicked () {

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hitter)) {
				if (hitter.transform.renderer.material.mainTexture == Resources.Load("textures/trans-tile-attack"))
					GameObject.FindGameObjectWithTag("Enemy").SetActive(false);
			}
		}
	}
}

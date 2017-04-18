using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uvScrolling : MonoBehaviour {

	public float scrollSpeed;

	// Update is called once per frame
	void Update () {

		/*
		float move = speed * Time.deltaTime;

		transform.Translate (Vector3.down * move, Space.World);

		if (transform.position.y < -8.99) {
			transform.position = new Vector3 (transform.position.x, 11, transform.position.z);
		}
		*/

		float offset = Time.time * -scrollSpeed;
		transform.GetComponent<Renderer> ().material.mainTextureOffset = new Vector2 (0, offset);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxScrolling : MonoBehaviour {

	public float speed;

	void Start()
	{
		// = TransparencySortMode.Orthographic;
	}

	// Update is called once per frame
	void Update () {
		float move = speed * Time.deltaTime;

		transform.Translate (Vector3.down * move, Space.World);

		if (transform.position.y < -8.99) {
			transform.position = new Vector3 (transform.position.x, 11, transform.position.z);
		}
	}
}

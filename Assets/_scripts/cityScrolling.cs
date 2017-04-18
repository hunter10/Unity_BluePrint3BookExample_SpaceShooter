using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cityScrolling : MonoBehaviour {

	public float speed;

	// Update is called once per frame
	void Update () {
		float move = speed * Time.deltaTime;

		transform.Translate (Vector3.down * move, Space.World);

		if (transform.position.y < -16.7) {
			transform.position = new Vector3 (transform.position.x, 19.135f, transform.position.z);
		}
	}
}

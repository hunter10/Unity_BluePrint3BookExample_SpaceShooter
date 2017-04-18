using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave2Script : MonoBehaviour {

	Vector3 rotationDirection = new Vector3(0, 0, 1);
	float rotationSpeed = 50.0f;
	Transform parentTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		parentTransform = this.transform.parent;	

		/*
		if (Input.GetButton ("Horizontal")) {
			transform.RotateAround (parentTransform.position, rotationDirection, rotationSpeed * Time.deltaTime);
		}

		if (Input.GetButtonDown ("Fire1")) {
			Debug.Log ("Fire1");
			this.transform.rotation = Quaternion.AngleAxis (0, Vector3.up);
		}
		*/
	}

	void FixedUpdate(){
		if (parentTransform != null) {
			transform.RotateAround (parentTransform.position, rotationDirection, rotationSpeed * Time.deltaTime);
			this.transform.rotation = Quaternion.AngleAxis (0, Vector3.up);

			//this.transform.rotation.z = 0f;
			//this.transform.Rotate(transform.rotation.x, transform.rotation.y, 0f);
		}
	}
}

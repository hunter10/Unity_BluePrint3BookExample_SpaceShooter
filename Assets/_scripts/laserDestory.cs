using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserDestory : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (run ());
	}

	IEnumerator run()
	{
		yield return new WaitForSeconds (4);
		Destroy (this.gameObject);
	}
	
	void OnTriggerEnter(Collider col){
		if (col.tag == "enemy") {
			Destroy (this.gameObject);
		}
	}
}

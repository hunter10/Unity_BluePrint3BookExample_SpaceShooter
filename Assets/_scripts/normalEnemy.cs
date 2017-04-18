using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalEnemy : MonoBehaviour {

	float startTime;
	float shootTimeLeft;
	int shootTimeSeconds = 1;

	public GameObject enemylaser;
	float laserSpeed = 1.0f;

	Transform shootSpwnT;

	public GameObject enemyDeath;
	GameObject deathAnim;

	bool canBeHit = true;

	void Start(){
		shootSpwnT = this.transform.Find ("shootSpwn");
	}

	// Update is called once per frame
	void Update () {
		shootTimeLeft = Time.time - startTime;
		if (shootTimeLeft >= shootTimeSeconds) {
			Fire ();
			startTime = Time.time;
			shootTimeLeft = 0.0f;
		}

		if (deathAnim != null) {
			if (!deathAnim.GetComponent<Animation> ().IsPlaying ("death")) {
				Destroy (deathAnim);
				Destroy (this.gameObject);
			}
		}
	}

	void Fire(){
		GameObject instLaser = Instantiate (enemylaser, shootSpwnT.transform.position, shootSpwnT.transform.rotation);
		instLaser.GetComponent<Rigidbody> ().velocity = transform.TransformDirection (Vector3.up * -laserSpeed);
	}

	void OnTriggerEnter(Collider col){
		if (canBeHit == true) {
			if (col.tag == "laser") {
				canBeHit = false;
				this.GetComponent<Renderer> ().enabled = false;
				deathAnim = Instantiate (enemyDeath, transform.position, transform.rotation) as GameObject;
				deathAnim.GetComponent<Animation> () ["death"].speed = 2;
			}
		}
	}
}

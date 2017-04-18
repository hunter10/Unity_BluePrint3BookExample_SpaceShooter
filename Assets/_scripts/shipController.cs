using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class shipController : MonoBehaviour {

	enum shipState{
		MOVINGUP,
		MOVINGDOWN,
		MOVINGLEFT,
		MOVINGRIGHT,
		SHOOT,
		IDLE
	};

	shipState curretnState;
	public float speed;

	public GameObject laser;
	public GameObject shootSpwnPos1;
	public GameObject shootSpwnPos2;

	float initialLaserSpeed = 10.0f;
	float fireRate = 0.2f;
	float nextFire = 0.0f;

	bool playerInvincible = false;
	GameObject gameMgObj;

	public GameObject playerDeathObj;
	GameObject playerDeathAnim;

	GameObject shield;
	public bool shieldOn = false;
	float guiTime;
	int seconds;
	public float startTime;

	// Use this for initialization
	void Start () {
		gameMgObj = GameObject.Find ("gameManager");

		shield = GameObject.Find ("shield");
		shield.GetComponent<Renderer> ().enabled = false;
		startTime = Time.time;
	}

	public IEnumerator destroyPlayer(Vector3 dpos){
		playerDeathAnim = Instantiate (playerDeathObj, dpos, playerDeathObj.transform.rotation) as GameObject;
		playerDeathAnim.GetComponent<Animation> () ["death"].speed = 3.5f;
		yield return new WaitForSeconds (0.5f);
	}

	void OnTriggerEnter(Collider enemy){
		if (enemy.tag == "enemyLaser" || enemy.tag == "enemy") {

			if (playerInvincible == false) {
				//Destroy (this.gameObject);
				this.GetComponent<Renderer> ().enabled = false;

				var pPosition = transform.position;
				StartCoroutine (destroyPlayer (pPosition));
			}
		}
	}

	void activateShield(){
		shield.GetComponent<Renderer> ().enabled = true;
	}

	void deActivateShield(){
		shield.GetComponent<Renderer> ().enabled = false;
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			this.GetComponent<Renderer> ().enabled = false;
			var pPosition = transform.position;
			StartCoroutine (destroyPlayer (pPosition));
		}

		if (playerDeathAnim != null) {
			if (!playerDeathAnim.GetComponent<Animation> ().IsPlaying ("death")) {
				Destroy (playerDeathAnim);
				Destroy (this.gameObject);

				var script1 = gameMgObj.transform.gameObject.GetComponent<gameManager> ();
				script1.respawn = true;
				script1.playerLives -= 1;
			}
		}

		if (shieldOn == true) {
			activateShield ();
		} else {
			deActivateShield ();
		}

		guiTime = Time.time - startTime;
		seconds = (int)(guiTime % 60);
		//print (seconds);
		if (seconds == 3) {
			playerInvincible = false;
			shieldOn = false;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			if (transform.position.y < 3.75) {
				curretnState = shipState.MOVINGUP;
				ActionShip (curretnState);
			}
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			if (transform.position.y > -3) {
				curretnState = shipState.MOVINGDOWN;
				ActionShip (curretnState);
			}
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (transform.position.x > -2) {
				curretnState = shipState.MOVINGLEFT;
				ActionShip (curretnState);
			}
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			if (transform.position.x < 2) {
				curretnState = shipState.MOVINGRIGHT;
				ActionShip (curretnState);
			}
		}

		if (Input.GetKey (KeyCode.Space) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			curretnState = shipState.SHOOT;
			ActionShip (curretnState);
		}
	}

	void ActionShip(shipState state){
		switch (state) {
			case shipState.MOVINGUP:
				transform.Translate (0, speed * Time.deltaTime, 0, Space.World);
				break;
			case shipState.MOVINGDOWN:
				transform.Translate (0, speed * -Time.deltaTime, 0, Space.World);
				break;
			case shipState.MOVINGLEFT:
				transform.Translate (speed * -Time.deltaTime, 0, 0, Space.World);
				break;
			case shipState.MOVINGRIGHT:
				transform.Translate (speed * Time.deltaTime, 0, 0, Space.World);
				break;
			case shipState.SHOOT:
				{
					GameObject cloneLaser1 = Instantiate (laser, shootSpwnPos1.transform.position, shootSpwnPos1.transform.rotation) as GameObject;
					cloneLaser1.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.up * initialLaserSpeed);

					GameObject cloneLaser2 = Instantiate (laser, shootSpwnPos2.transform.position, shootSpwnPos2.transform.rotation) as GameObject;
					cloneLaser2.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.up * initialLaserSpeed);
				}
				break;
		}
	}
}

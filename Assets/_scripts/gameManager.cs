using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

	public Transform enemySpwn1;
	public Transform enemyTarg1;
	public Transform enemySpwn2;
	public Transform enemyTarg2;
	public GameObject enemyPrefab1;
	public GameObject enemyPrefab2;
	public GameObject enemyPrefab3;

	Vector3 targetSpwnDir1;
	Vector3 targetSpwnDir2;

	public float enemySpeed = 0.1f;
	public float enemySpeed2 = 0.2f;

	public GameObject playerObj;
	public Transform playerSpwn;
	public bool respawn = false;
	public int playerLives = 4;
	Component script1;
	GameObject player1;
	GameObject shield;

	public Texture2D playerLivesTxt;
	int playerScore = 0;
	public GUIStyle style;

	void OnGUI(){
		GUI.Label (new Rect (20, 660, 74, 85), playerLivesTxt, style);
		GUI.Label (new Rect (20, 660, 74, 85), playerLives.ToString(), style);
		GUI.Label (new Rect (20, 660, 74, 85), playerScore.ToString(), style);
	}

	public void respawnPlayer(){
		Debug.Log ("respawnPlayer!");
		player1 = Instantiate (playerObj, playerSpwn.transform.position, playerSpwn.transform.rotation) as GameObject;
		respawn = false;

		var script1 = player1.transform.gameObject.GetComponent<shipController> ();
		script1.shieldOn = true;
	}

	// Use this for initialization
	void Start () {
		targetSpwnDir1 = enemyTarg1.position - enemySpwn1.position;
		targetSpwnDir2 = enemyTarg2.position - enemySpwn2.position;
		//Debug.Log ("targetSpwnDir2:" + targetSpwnDir2);

		respawnPlayer ();

		//StartCoroutine(SendEnemy ());
		//StartCoroutine ("SendWave2");
	}

	IEnumerator SendEnemy(){
		//GameObject instantiatedProjectile = Instantiate (enemyPrefab1, enemySpwn1.transform.position, enemySpwn1.transform.rotation);
		//instantiatedProjectile.GetComponent<Rigidbody> ().velocity = instantiatedProjectile.transform.TransformDirection (targetSpwnDir1 * enemySpeed);

		StartCoroutine ("SendWave1");
		yield return new WaitForSeconds (5.0f);
		StartCoroutine ("SendWave2");
	}

	IEnumerator SendWave1(){
		for (int i = 0; i <= 3; i++) {
			GameObject instantiatedProjectile = Instantiate (enemyPrefab1, enemySpwn1.transform.position, enemySpwn1.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = transform.TransformDirection (targetSpwnDir1 * enemySpeed2);
			yield return new WaitForSeconds (1.0f);
		}
	}



	IEnumerator SendWave2(){
		for (int i = 0; i <= 5; i++) {
			GameObject instantiatedProjectile = Instantiate (enemyPrefab2, enemySpwn2.transform.position, enemySpwn2.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = transform.TransformDirection (targetSpwnDir2 * enemySpeed);
			yield return new WaitForSeconds (1.0f);
		}
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (respawn + ", " + playerLives);
		if (respawn == true && playerLives != 0) {
			respawnPlayer ();
		}
	}
}

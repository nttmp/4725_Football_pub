using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetBeefyRM : MonoBehaviour {

	public bool beefy = false;
	public bool alive = true;  //FUCK
	public GameObject player;  //SHIT
	public GameObject endCamera;  //PISS
	public bool gameComplete;
	public float growthFactor = 2.0f;
	float maxScale = 1.0f;
	Vector3 baseScale;
	Vector3 beefScale;
	PhysicMaterial basePhysics;
	public float roidRage = 100.0f;
	public float roidRageDecrement = 0.1f;
	//For the sticky enemies
	public GameObject parentingObject; //Use this so that the sticky enemies know what object to stick to
	List<GameObject> stickyObjectList = new List<GameObject> (); //Holds the current sticked enemies
	public GameObject explosionObject; //Some random object with a rigidbody that'll be used to call AddExplosionForce
	//For controlling the explosion force on the sticky enemies
	public float forceMultiplier = 100.0f;
	public float expPower = 10.0f;
	public float expRadius = 5.0f;
	public float number3 = 3.0f;
	//For boosting shenanigans
	public GameObject fpsCube;
	//For the detection of rPresses, which governs boosting
	bool rPressed;
	public float rTimer;
	public float rTimerMax;
	//Control Texture Switchig
	public Texture normalTexture;
	public Texture beefyTexture;

	//GUI OBJECTS
	public GameObject gui9;
	public GameObject gui8;
	public GameObject gui7;
	public GameObject gui6;
	public GameObject gui5;
	public GameObject gui4;
	public GameObject gui3;
	public GameObject gui2;
	public GameObject gui1;
	public GameObject gui0;
	public GameObject guiSwoll;
	public GameObject guiLift;
	public GameObject guiWin;
	public GameObject guiLose;

	bool restart;

	// Use this for initialization
	void Start () {
		baseScale = transform.localScale;
		beefScale = baseScale;
		basePhysics = this.collider.material;
		restart = false;
		gameComplete = false;
		rPressed = false;
		rTimer = 0.0f;

	}
	
	// Update is called once per frame
	void Update () {

		if (beefy) {
			fpsCube.renderer.material.mainTexture = beefyTexture;
		} 
		else {
			fpsCube.renderer.material.mainTexture = normalTexture;
		}

		//Debug.Log ("stickyObjectList.Count: " + stickyObjectList.Count);

		//Debug.Log ("Restart: " + restart);
		if (restart) {
			if (Input.GetKey ("r")) {
				//Debug.Log ("Restart");
				Application.LoadLevel(Application.loadedLevel);
			}
		}

		if ((Input.GetKey ("r")) && (rPressed == false)) {
			rPressed = true;
			rTimer = rTimerMax;
			beefy = true;
		}

		else if (Input.GetKey ("b") && roidRage > 0.0f) {
			beefy = true;
			roidRage -= roidRageDecrement;
			parentingObject.transform.DetachChildren(); //Don't even need the list?!
			//Handle throwing off all of the current sticked enemies.
			if (stickyObjectList.Count >= 1) {
				for (int i = 0; i < stickyObjectList.Count; i++) {
					stickyObjectList[i].rigidbody.isKinematic = false;
					stickyObjectList[i].rigidbody.AddExplosionForce(expPower, parentingObject.transform.position,  expRadius, number3);
					//					stickyObjectList[i].rigidbody.AddForce(Vector3.up * forceMultiplier);
					//					stickyObjectList[i].rigidbody.AddForce(Vector3.back * forceMultiplier);
					//					Debug.Log ("this object's activated: " + stickyObjectList[i].GetComponent<StickyAI>().activated);
					//					Debug.Log ("i = " + i);
				}
				//parentingObject.rigidbody.isKinematic = true;
				//explosionObject.rigidbody.AddExplosionForce(expPower, parentingObject.transform.position,  expRadius, number3);
				//parentingObject.rigidbody.isKinematic = false;
				stickyObjectList.Clear();
			}
		}

		else  {
			if (rPressed == false)
			beefy = false;
		}

		if (rTimer > 0.0f) {
			rTimer -= Time.deltaTime;
			if (rTimer < rTimerMax - 1.0f) {
				beefy = false;
			}
			if (rTimer <= 0.0f) {
				rTimer = 0.0f;
				rPressed = false;
			}
		}

		if ((Input.GetKey ("r") && rTimer >= rTimerMax - 1.0f) || (Input.GetKey ("b") && roidRage > 0.0f)) {
						beefy = true;
		} 
		else {
			beefy = false;
		}

		if (beefy && (Input.GetKey ("b"))) { //&& rPressed == false;
			transform.localScale = beefScale * growthFactor;
			//rigidbody.isKinematic = true ; rigidbody.Sleep();  //Before change
			//this.collider.material=new PhysicMaterial("Bouncy");
			//rigidbody.isKinematic = false ; rigidbody.WakeUp();  //After change
		}
		else {
			transform.localScale = baseScale;
			//rigidbody.isKinematic = true ; rigidbody.Sleep();  //Before change
			//this.collider.material= basePhysics;
			//rigidbody.isKinematic = false ; rigidbody.WakeUp();  //After change
		}
		AdjustGUI();
	}//Update

	void Beefy() {

	}

	void AdjustGUI() {
		if (roidRage < 90.0f) gui9.SetActive (false);
		if (roidRage < 80.0f) gui8.SetActive (false);
		if (roidRage < 70.0f) gui7.SetActive (false);
		if (roidRage < 60.0f) gui6.SetActive (false);
		if (roidRage < 50.0f) gui5.SetActive (false);
		if (roidRage < 40.0f) gui4.SetActive (false);
		if (roidRage < 30.0f) gui3.SetActive (false);
		if (roidRage < 20.0f) gui2.SetActive (false);
		if (roidRage < 10.0f) gui1.SetActive (false);
		if (roidRage < 0.0f) {
			gui0.SetActive (false);
			guiSwoll.SetActive (false);
			guiLift.SetActive (true);
		}
	}

	void OnTriggerEnter(Collider other) {
		//Normal enemy collision event: Lose the game
		if (other.gameObject.tag == "Enemy") {
			if (beefy) {
				other.GetComponent<PursueRM>().activated = false;
				other.GetComponent<BasicAIDK>().activated = false;
				other.rigidbody.AddForce(Vector3.up * forceMultiplier);
				other.rigidbody.AddForce(other.transform.forward * -forceMultiplier);
			}
			else {
				if (gameComplete == false) {
					alive = false;
					gameComplete = true;
					Debug.Log ("KILLED");
					gui9.SetActive (false);
					gui8.SetActive (false);
					gui7.SetActive (false);
					gui6.SetActive (false);
					gui5.SetActive (false);
					gui4.SetActive (false);
					gui3.SetActive (false);
					gui2.SetActive (false);
					gui1.SetActive (false);
					gui0.SetActive (false);
					guiSwoll.SetActive (false);
					guiLift.SetActive (false);
					guiLose.SetActive (true);
					endCamera.SetActive(true);
					restart = true;
					//Restart ();
				}
			}
		}//Normal

		//Handle the case of a sticky enemy
		else if (other.gameObject.tag == "Sticky") {
			if (other.GetComponent<StickyAI>().activated == true) {
				other.transform.parent = parentingObject.transform;
				other.GetComponent<StickyAI>().activated = false;
				other.rigidbody.isKinematic = true;
				stickyObjectList.Add (other.gameObject);
			}
		} //Sticky

	}//OnTriggerEnter

	public void ContingencyWinState() {
		Debug.Log ("We made it");
		if (gameComplete == false) {
			gameComplete = true;
			alive = false;
			Debug.Log ("ENDZONE");
			gui9.SetActive (false);
			gui8.SetActive (false);
			gui7.SetActive (false);
			gui6.SetActive (false);
			gui5.SetActive (false);
			gui4.SetActive (false);
			gui3.SetActive (false);
			gui2.SetActive (false);
			gui1.SetActive (false);
			gui0.SetActive (false);
			Debug.Log ("ENDZONE2");
			guiSwoll.SetActive (false);
			Debug.Log ("ENDZONE3");
			guiLift.SetActive (false);
			Debug.Log ("ENDZONE4");
			guiWin.SetActive (true);
			Debug.Log ("ENDZONE5");
			restart = true;
			Debug.Log ("Restart: " + restart);
			//Restart ();
		}
	}

	void Restart() {
		if (alive == false) 				
			player.SetActive(false);
		gameComplete = true;
		if (Input.GetKey ("r")) {
			Debug.Log ("Restart method");
			Application.LoadLevel(Application.loadedLevel);
		}
	}

}//Class

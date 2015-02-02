using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	bool win;
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
	public GameObject freakingUseThis;

	// Use this for initialization
	void Start () {
		win = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("The player entered the endzone");
			freakingUseThis.GetComponent<GetBeefyRM>().ContingencyWinState();
		}
	}
}

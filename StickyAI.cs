using UnityEngine;
using System.Collections;

public class StickyAI : MonoBehaviour {
	
	private GameObject target;
	private Vector3 targetVector; //Not used
	private Vector3 direction;
	public float ambientSpeed;
	public bool basic;
	public bool surroundAI;
	public float pastDistance;
	public float boost;
	public bool activated;  //ADDED
	float timeLimit = 5.0f;  //ADDED
	// Use this for initialization
	void Start () {
		activated = true;
		target = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (activated) {
			if (target != null) 
			{
				//transform.LookAt (target.transform);
				if (basic) {
					move (ambientSpeed);
				} 
				else if (surroundAI) {
					surround ();
				}
				
				//				if(target.GetComponentInChildren<GetBeefyRM>().beefy = true)
				//				{
				//					
				//				}
			}
		}
//		else {
//			CoolDown ();
//			
//		}
	}
	
	void move(float speed) {
		transform.LookAt (target.transform);
		//		targetVector = target.rigidbody.position - rigidbody.position;
		//		direction = targetVector / targetVector.magnitude;
		//		
		//		rigidbody.velocity = direction * (Time.fixedDeltaTime * speed);
		transform.Translate (Vector3.forward * speed);
		
	}
	
	void surround() {
		
		if (target.transform.position.z <= (rigidbody.position.z + pastDistance)) {
			//rigidbody.velocity = new Vector3 (0, 0, -1) * (Time.fixedDeltaTime * ambientSpeed);
			transform.Translate (Vector3.forward * ambientSpeed);
		} 
		else {
			move (ambientSpeed + boost);
		}
		
	}
	
//	void CoolDown() {
//		//float timeLimit = 5.0f;
//		//while (timeLimit > 0.0f) {
//		//Debug.Log (timeLimit);
//		timeLimit  -= Time.deltaTime;
//		//}
//		if (timeLimit <= 0.0f) {
//			activated = true;
//			timeLimit = 5.0f;
//		}
//	}
}

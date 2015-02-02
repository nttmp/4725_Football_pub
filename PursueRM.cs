//borrowed from http://answers.unity3d.com/questions/22130/how-do-i-make-an-object-always-face-the-player.html

using System;
using UnityEngine;

public class PursueRM : MonoBehaviour
{
	public Transform target;
	public float speed;
	public bool activated;
	float timeLimit = 5.0f;

	void Start() {
		activated = true;
	}
	
	void Update()
	{
		if (activated) {
			if (target != null) {
				transform.LookAt (target);
			}
			transform.Translate (Vector3.forward * speed);
		}
		else {
			CoolDown ();
		}
	}

	void CoolDown() {
		//float timeLimit = 5.0f;
		//while (timeLimit > 0.0f) {
			//Debug.Log (timeLimit);
			timeLimit  -= Time.deltaTime;
		//}
		if (timeLimit <= 0.0f) {
			activated = true;
			timeLimit = 5.0f;
		}
	}
}
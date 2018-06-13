using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmallBubble : MonoBehaviour {

	void OnTriggerEnter (Collider collide)
	{
		if (collide.CompareTag ("Player")) {
			Debug.Log("player dead");
		}

	}
}

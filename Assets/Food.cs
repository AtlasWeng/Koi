using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
	public int bubbleValue = 1;

	void OnTriggerEnter (Collider collider)
	{
		Debug.Log ("Fish eat food");

		if (collider.CompareTag ("Player")) {
			collider.GetComponent<ClickToMove> ().AddBubble (bubbleValue);
	
			Destroy (gameObject);
		}

	}

	void OnTriggerStay (Collider collider)
	{
		//Debug.Log ("Overlay");
		if (collider.CompareTag ("Food") || collider.CompareTag ("Enemy")) {
			GameObject.FindObjectOfType<FoodSpawner>().SpawnFood();
			//Debug.Log ("destroy");
			Destroy (gameObject);
		}
	}
}

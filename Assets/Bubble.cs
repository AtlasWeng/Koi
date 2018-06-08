using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

	public int bubbleDamage = 8;

	void OnTriggerEnter (Collider collider)
	{
		if (collider.CompareTag ("Enemy")) {
			EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth> ();
			enemyHealth.TakeDamage(bubbleDamage, transform.position);
			Debug.Log(enemyHealth.currentHealth);
			Destroy(gameObject);
		}
	} 
}

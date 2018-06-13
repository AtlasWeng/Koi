using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

	public int bubbleDamage = 8;
	public GameObject inkSplashPrefab;

	// private variables

	// store reference to component on gameobject

	void OnTriggerEnter (Collider collider)
	{
		if (! collider.CompareTag ("Enemy"))
			return;
		//if (collider.CompareTag ("Enemy")) {

		EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth> ();
		enemyHealth.TakeDamage(bubbleDamage, transform.position);

		Instantiate (inkSplashPrefab, transform.position, Quaternion.identity);


		Debug.Log(enemyHealth.currentHealth);
		Destroy(gameObject);
	} 
}

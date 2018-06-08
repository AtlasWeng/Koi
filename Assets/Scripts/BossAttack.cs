using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {

	//public int attackDamage;

	// private variables
	float timer;
	bool playerInRange;

	// store reference 
	GameObject player;
	// PlayerHealth
	EnemyHealth enemyhealth;

	// store reference to component on gameobject
	Animator _animator;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		//playerHealth = player.GetComponent <PlayerHealth> ();
		enemyhealth = GetComponent <EnemyHealth> ();
		_animator = GetComponent <Animator> ();
	}

	/*void OnTriggerEnter (Collider collider)
	{
		
	}*/

}

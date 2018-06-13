using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAttack : MonoBehaviour {

	public float normalSpeed;

	[Header ("Bubble Attack")]
	public GameObject splitBubblePrefab;
	public float bubbleSpeed = 200f;


	// private variables
	public float timer;
	bool playerInRange;

	// store reference to components on gameobject
	GameObject player;
	GameObject emitPoint;
	Transform originalPos;

	// PlayerHealth
	EnemyHealth enemyHealth;


	// store reference to component on gameobject
	Animator _animator;
	NavMeshAgent _nav;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		//playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		_animator = GetComponent <Animator> ();
		_nav = GetComponent <NavMeshAgent> ();
		originalPos = gameObject.transform;
		emitPoint = transform.Find ("Emit Point").gameObject;
	}

	// the attack way of state one
	void BubbleAttack ()
	{
		transform.LookAt(player.transform.position);

		// start the timer
		timer += Time.deltaTime;
		if (timer >= 5) {
			// set player's direction
			Vector3 dirToShoot = player.transform.position - emitPoint.transform.position;

			GameObject bubbles = Instantiate(splitBubblePrefab, emitPoint.transform.position, Quaternion.identity) as GameObject;
			bubbles.GetComponent<Rigidbody>().AddForce(dirToShoot.normalized * bubbleSpeed);

			// reset the timer
			timer = 0;
		}



	}

	// the attack way of state two
	void SummonAttack () {

	}

	// the attack way of state two
	void CrashAttack () {
		// reset timer;
		timer += Time.deltaTime;

		// set the moving speed
		_nav.speed = normalSpeed;
		if (timer >= 5) {
			_nav.SetDestination (player.transform.position);
			timer = 0;
		}
	}

	void Update ()
	{
		if (60 < enemyHealth.currentHealth && enemyHealth.currentHealth <= 99) {
			BubbleAttack ();
		}
		if (1 <= enemyHealth.currentHealth && enemyHealth.currentHealth <= 30) {
			CrashAttack ();
		}

		// set conditions of animator
		_animator.SetFloat ("_velocity", _nav.velocity.magnitude / normalSpeed);
	}
}

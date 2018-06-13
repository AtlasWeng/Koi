using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	// public variables
	public int damagePerShoot = 8;
	public float timeBetweenBubble = 0.2f;
	public float range = 100f;
	public float bubbleSpeed = 200f;
	public GameObject bubblePrefab;

	// private variables
	float timer;
	float effectDisplayTime;
	int shootableMask;
	Ray shootRay;
	RaycastHit shootHit;
	ParticleSystem shootParticle;

	// store reference to component on gameobject
	Animator _animator;
	AudioSource _audio;

	void Awake () {
		_animator = GetComponent <Animator> ();	
		shootableMask = LayerMask.GetMask ("Shootable");
		shootParticle = GetComponent <ParticleSystem> ();
		//_audio = GetComponent <AudioSource> ();
		Debug.Log("Shoot Mask is: " + shootableMask);
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;	

		// shot the bubble
		// Shoot ();
	}

	public void Shoot (Vector3 shootDir)
	{
		timer = 0;

		//_audio.Play ();

		//shootParticle.Stop ();
		//shootParticle.Play ();

		shootRay.origin = transform.position;
		shootRay.direction = shootDir;

		if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
			EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();	

			if (enemyHealth != null) {
				// emit a bubble
				GameObject bubble = Instantiate (bubblePrefab, transform.position, Quaternion.identity) as GameObject;
				bubble.GetComponent <Rigidbody> ().AddForce (shootDir.normalized * bubbleSpeed);
			}


		}
	}
}

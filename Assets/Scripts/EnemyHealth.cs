using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public float sinkSpeed = 2.5f;

	// UI elements
	public Image filler;

	// SFX
	public AudioClip deathSFX;

	// store reference to component on gameobject
	Animator _animator;
	AudioSource _audio;
	ParticleSystem _particle;
	CapsuleCollider _capsuleCollider;

	// private variable
	bool isDead;
	public bool isSinking;

	void Start () {
		_animator = GetComponent <Animator>();
		_audio = GetComponent <AudioSource>();
		_particle = GetComponentInChildren <ParticleSystem> ();
		_capsuleCollider = GetComponent <CapsuleCollider> ();

		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isSinking) {
			transform.Translate (Vector3.down * sinkSpeed * Time.deltaTime);
		}
	}

	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		if (isDead)
			return;

		//_audio.Play ();

		currentHealth -= amount;
		filler.fillAmount = currentHealth * 0.01f;

		//_particle.transform.position = hitPoint;
		//_particle.Play ();

		if (currentHealth <= 0) 
			Death ();
	}

	void Death () {
		isDead = true;

		_capsuleCollider.isTrigger = true;

		_animator.SetTrigger ("_dead");

		_audio.PlayOneShot(deathSFX);
	}

	public void StartSinking () {
		GetComponent <Rigidbody> ().isKinematic = true;

		isSinking = true;

		Destroy(gameObject, 2f);
	}
}

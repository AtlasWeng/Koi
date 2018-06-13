using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBubble : MonoBehaviour {

	// public variables
	public float smallBubbleSpeed = 200f;
	public int numsOfBubble = 6;
	public GameObject smallSplitBubblePrefab;
	public GameObject inkSplash;

	// reference to component on gameobject
	Rigidbody _rg;

	// private variables
	private Vector3 playerPos;
	private Vector3 forward;
	private Vector3 toward; 

	void Start () {
		playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		_rg = GetComponent <Rigidbody> ();

	}

	void Update () {
		forward = _rg.velocity;
		toward = playerPos - transform.position;

		if (Vector3.Dot (toward.normalized, forward.normalized) <= 0) {
			BubbleSplitting();

			Instantiate (inkSplash, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}

	}

	public void BubbleSplitting ()
	{
		Vector3 dirToMove = new Vector3 (Random.Range(0f, 1f), 0f, Random.Range(0f, 1f));
		for (int i = 0; i < numsOfBubble; i++) {
			GameObject smallBubble = Instantiate (smallSplitBubblePrefab, transform.position, Quaternion.identity) as GameObject;
			smallBubble.GetComponent<Rigidbody>().AddForce(dirToMove.normalized * smallBubbleSpeed);

			dirToMove = Quaternion.Euler(0, 60, 0) * dirToMove;
		}
	}


}

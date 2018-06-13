using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitScreenSize : MonoBehaviour {

	private float width;
	private float heigh;
	private float depth = 1f;

	// Use this for initialization
	void Start () {
		width = Mathf.Abs(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x * 2);
		heigh = Mathf.Abs(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).z * 2);

		transform.localScale = new Vector3(width, depth, heigh);
	}
}

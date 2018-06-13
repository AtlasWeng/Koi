using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticleSystem : MonoBehaviour {
	public float destroyAfterSecond;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, 1f);				
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

	public GameObject foodPrefab;
	public float timer;
	public int maxAmount;

	// private variables
	private float spawnX;
	private float spawnY;
	private float spawnZ;

	void Start () {
		//Debug.Log (Camera.main.ScreenToWorldPoint(new Vector2(0,0)).x);
		//Debug.Log (Camera.main.ScreenToWorldPoint(new Vector2(0,0)));
	}

	public void SpawnFood () {
		spawnX = Random.Range(-Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
		spawnZ = Random.Range(-Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y,  Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
		spawnY = GameObject.FindGameObjectWithTag ("Player").transform.position.y;
		Vector3 foodPos = new Vector3 (spawnX, spawnY, spawnZ);
		Instantiate (foodPrefab, foodPos, Quaternion.identity);
	}

	void Update ()
	{
		timer += Time.deltaTime;
		if (timer >= 0.5f) {
			SpawnFood();
			timer = 0f;
		}
	}

}

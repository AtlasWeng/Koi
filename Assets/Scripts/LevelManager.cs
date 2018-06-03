using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public float intervalTime = 2f;
	public static LevelManager lm;

	void Start ()
	{
		if (lm == null) {
			lm = this.GetComponent<LevelManager>();
		}
	}

	void OnEnable ()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode)
	{
		if (scene.buildIndex == 0) {
			AutoLoadNextScene ();
		}
	}

	void OnDisable () {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public void LoadScene (string level)
	{
		SceneManager.LoadScene(level);
	}

	public void LoadNextScene ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame () {
		Application.Quit();
	}

	void AutoLoadNextScene () {
		Invoke ("LoadNextScene", intervalTime);
	}

//	IEnumerator ChangeLevel () {
//		float fadeTime = GameObject.Find("GameManager").GetComponent<Fading>().BeginFade(1);
//		yield return new WaitForSeconds(fadeTime);
//		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//	}
}

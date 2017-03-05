using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCompleted : MonoBehaviour {
	public GameObject player; 
	public float distanceToWin;
	public GameObject[] collectorSpheres;
	public GameObject[] robots;
	public GameObject continueScreen;

	bool levelCompleted;
	// Use this for initialization
	void Start () {
		levelCompleted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (player.transform.position, transform.position) < distanceToWin) {
			if (levelCompleted) {
				continueScreen.SetActive (true);
				//Ensures you can't die anymore if you win lol
				for (int i = 0; i < robots.Length; i++) {
					robots [i].SetActive (false);
				}
				if (Input.GetKey (KeyCode.Space)) {
					SceneManager.LoadScene ("Jumpy_level");
					continueScreen.SetActive (false);
				}
			}
			levelCompleted = true;
			for (int i = 0; i < collectorSpheres.Length; i++) {
				if (!collectorSpheres [i].GetComponent<CollectorSphere> ().hasBeenCollected) {
					levelCompleted = false;
				}
			}
		}
		if(Input.GetKey(KeyCode.Return)){
			SceneManager.LoadScene ("Jumpy_level");
		}
	}
}

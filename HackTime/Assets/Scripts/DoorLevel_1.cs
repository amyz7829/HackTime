using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLevel_1 : MonoBehaviour {

	public GameObject barrier;
	public GameObject player;
	public GameObject continueScreen;
	public float distance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//If you're close to the door and the barrier is not there, then continue to level 2
		if (Vector3.Distance (player.transform.position, transform.position) < distance
			&& barrier.transform.localScale == Vector3.zero) {
			continueScreen.SetActive (true);
			if(Input.GetKey(KeyCode.Space)){
				SceneManager.LoadScene("Scenes/Level2");
			}
		}
	}
}

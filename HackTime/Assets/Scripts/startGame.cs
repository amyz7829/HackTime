using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startGame : MonoBehaviour {

	public Button playButton;
	public Button quitButton;

	public GameObject player;
	public GameObject startScreen;

	PlayerControllerMouse controller; 
	// Use this for initialization
	void Start () {
		controller = player.GetComponent<PlayerControllerMouse> ();
		controller.controlEnabled = false;

		Button play = playButton.GetComponent<Button> ();
		play.onClick.AddListener (Play);

		Button quit = quitButton.GetComponent<Button> ();
		quit.onClick.AddListener (Quit);
	}
	
	void Play(){
		startScreen.SetActive (false);
		controller.controlEnabled = true;
	}

	void Quit(){
		Application.Quit ();
	}

	void Update(){
		if (Input.GetKey (KeyCode.Space)) {
			startScreen.SetActive (false);
			controller.controlEnabled = true;
		}
	}
}

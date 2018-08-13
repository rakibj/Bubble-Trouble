using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

	private int numberOfBalls;
	public GameObject winPanel;
	public GameObject losePanel;
	public AudioClip buttonClip;
	public static bool playerTouched;
	public GameObject bgMusic;
	public Text levelText;
	// Use this for initialization
	void Awake () {
		numberOfBalls = 0;
		playerTouched = false;
	}
	
	// Update is called once per frame
	void Update () {
		CalculateBalls ();
		if (numberOfBalls <= 0) {
			Debug.Log ("Done");
			winPanel.SetActive (true);
			Time.timeScale = 0;
			bgMusic.SetActive (false);
		}
		if (playerTouched) {
			GameOver ();
		}

		if (winPanel.activeInHierarchy == false && losePanel.activeInHierarchy == false) {
			Time.timeScale = 1;
			if (bgMusic.activeInHierarchy == false) {
				bgMusic.SetActive (true);
			}
		}
	}

	private void CalculateBalls(){
		numberOfBalls = 
			GameObject.FindGameObjectsWithTag ("LargestBall").Length +
		GameObject.FindGameObjectsWithTag ("LargeBall").Length +
		GameObject.FindGameObjectsWithTag ("MediumBall").Length +
		GameObject.FindGameObjectsWithTag ("SmallBall").Length +
		GameObject.FindGameObjectsWithTag ("SmallestBall").Length;
	}

	public void GameOver(){
		losePanel.SetActive (true);
		Time.timeScale = 0;
		bgMusic.SetActive (false);

	}
	public void PlayButtonClickSound(){
		AudioSource asrc = GetComponent<AudioSource> ();
		asrc.PlayOneShot (buttonClip);
	}
	public void Replay(){
		if (levelText.text == "Level 01") {
			Application.LoadLevel ("Preparation");
		}
		if (levelText.text == "Level 02") {
			Application.LoadLevel ("Level2");
		}
		if (levelText.text == "Level 03") {
			Application.LoadLevel ("Level3");
		}
		if (levelText.text == "Level 04") {
			Application.LoadLevel ("Level4");
		}
		if (levelText.text == "Level 05") {
			Application.LoadLevel ("Level5");
		}
		
		PlayButtonClickSound ();

	}

	public void GoToMain(){
		Application.LoadLevel ("MainMenu");
		PlayButtonClickSound ();
	}

	public void GoToNextLevel(){
		if (levelText.text == "Level 01") {
			Application.LoadLevel ("Level2");
		}
		if (levelText.text == "Level 02") {
			Application.LoadLevel ("Level3");
		}
		if (levelText.text == "Level 03") {
			Application.LoadLevel ("Level4");
		}
		if (levelText.text == "Level 04") {
			Application.LoadLevel ("Level5");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public AudioClip buttonClip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void PlayButtonClickSound(){
		AudioSource asrc = GetComponent<AudioSource> ();
		asrc.PlayOneShot (buttonClip);
	}
	public void PlayButton(){
		Application.LoadLevel ("Preparation");
		PlayButtonClickSound ();
	}
	public void QuitButton(){
		Application.Quit ();
		PlayButtonClickSound ();
	}
}

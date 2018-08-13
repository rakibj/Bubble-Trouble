using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakScript : MonoBehaviour {

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private AnimationClip breakBrickClip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator BreakTheBrick(){
		animator.Play ("Brick Broken");
		yield return new WaitForSeconds (breakBrickClip.length);
		gameObject.SetActive (false);
	}
}

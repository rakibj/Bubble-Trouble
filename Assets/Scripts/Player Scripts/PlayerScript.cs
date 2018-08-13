using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour {

	public static PlayerScript instance;
	[SerializeField]
	private float speed = 8.0f;
	private float maxVelocity = 4.0f;
	private Button shootButton;
	[SerializeField]
	private Rigidbody2D myRigidBody;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private GameObject[] arrows;

	[SerializeField]
	private AnimationClip shootAnimationClip;

	[SerializeField]
	private AudioClip shootAudioClip;

	private float height;
	private bool canWalk;
	private bool shootOnce;
	private bool shootTwice;
	private bool moveLeft, moveRight;
	// Use this for initialization
	void Awake(){
		if (instance == null) {
			instance = this;
		}
		float cameraHeight = Camera.main.orthographicSize;
		height = -cameraHeight - 0.8f;
		canWalk = true;
		shootOnce = true;
		shootTwice = true;

		shootButton = GameObject.FindGameObjectWithTag ("ShootButton").GetComponent<Button> ();
		shootButton.onClick.AddListener (() => ShootTheArrow());
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//ShootTheArrow ();
	}



	void FixedUpdate(){
		MoveThePlayer ();
	}

	/*public void OnPointerDown(PointerEventData data){
		if (this.gameObject.tag == "MoveLeftButton") {
			Debug.Log ("Moving Left");
			MoveThePlayerLeft ();
		} else if (this.gameObject.tag == "MoveRightButton") {
			MoveThePlayerRight ();
		}
	}
	public void OnPointerUp(PointerEventData data){
	}
*/
	void MoveRight(){
		if (canWalk) {
			float force = 0.0f;
			float velocity = Mathf.Abs (myRigidBody.velocity.x);
			if (velocity < maxVelocity) {
				force = speed;
			}

			//change direction of the body
			Vector3 scale = transform.localScale;
			scale.x = 1;
			transform.localScale = scale;

			animator.SetBool ("Walk", true);
			myRigidBody.AddForce (new Vector2 (
				force,
				0
			));
		}

	}
	void MoveLeft(){
		if(canWalk){
			float force = 0.0f;
			float velocity = Mathf.Abs (myRigidBody.velocity.x);

		if (velocity < maxVelocity) {
			force = -speed;
		}

		//change direction of the body
		Vector3 scale = transform.localScale;
		scale.x = -1;
		transform.localScale = scale;

		animator.SetBool ("Walk", true);
			myRigidBody.AddForce (new Vector2 (
				force,
				0
			));
		}	
	}
	public void StopMoving(){
		moveLeft = moveRight = false;
		animator.SetBool ("Walk", false);
			
	}
	public void MoveThePlayerLeft(){
		moveLeft = true;
		moveRight = false;
	}

	public void MoveThePlayerRight(){
		moveLeft = false;
		moveRight = true;
	}

	void MoveThePlayer(){
		if (moveLeft) {
			MoveLeft ();
		}
		if (moveRight) {
			MoveRight ();
		}
	}

	/*
	void PlayerWalkKeyboard(){
		if(canWalk){
		float force = 0.0f;
		float velocity = Mathf.Abs (myRigidBody.velocity.x);

		float h = Input.GetAxis ("Horizontal");

		if (h > 0) {
			//moving right
			//limiting its velocity
			if (velocity < maxVelocity) {
				force = speed;
			}

			//change direction of the body
			Vector3 scale = transform.localScale;
			scale.x = 1;
			transform.localScale = scale;

			animator.SetBool ("Walk", true);

		} else if (h < 0) {

			//moving left

			//limiting its velocity
			if (velocity < maxVelocity) {
				force = -speed;
			}

			//change direction of the body
			Vector3 scale = transform.localScale;
			scale.x = -1;
			transform.localScale = scale;

			animator.SetBool ("Walk", true);

		} else {
			animator.SetBool ("Walk", false);
		}

		//add force on the body
		myRigidBody.AddForce (new Vector2 (
			force,
			0
		));

		}
	}*/
	public void SetShootOnce(bool value){
		shootOnce = true;
	}
	public void SetShootTwice(bool value){
		shootTwice = true;
	}

	public void ShootTheArrow(){
			//Debug.Log ("Shoot The Arrow");
			if (shootOnce) {
				shootOnce = false;
				StartCoroutine (PlayTheShootAnimation ());	
				GameObject arrow = Instantiate (arrows [0], 
					                   new Vector3 (this.transform.position.x, height, 0), 
					                   Quaternion.identity) 
					as GameObject;
			} else if (shootTwice) {
				shootTwice = false;
				StartCoroutine (PlayTheShootAnimation ());	
				GameObject arrow = Instantiate (arrows [1], 
					new Vector3 (this.transform.position.x, height, 0), 
					Quaternion.identity) 
					as GameObject;
			} 
	}


	IEnumerator PlayTheShootAnimation(){
		canWalk = false;
		animator.Play ("Player Shoot Animation");
		AudioSource.PlayClipAtPoint (shootAudioClip, transform.position);
		yield return new WaitForSeconds (shootAnimationClip.length);
		animator.SetBool ("Shoot", false);
		canWalk = true;
	}
}//class






























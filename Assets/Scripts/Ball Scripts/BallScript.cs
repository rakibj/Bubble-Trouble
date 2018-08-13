using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	private float forceX, forceY;
	[SerializeField]
	private bool moveLeft,moveRight;
	[SerializeField]
	private Rigidbody2D myRigidBody;
	[SerializeField]
	private GameObject originalBall;
	[SerializeField]
	private AudioClip[] popClips;

	private GameObject ball1,ball2;

	private BallScript ballScript1, ballScript2;

	private float ballBounceNormal,ballBounceHigh;

	void Awake(){
		ballBounceNormal = 5f;
		ballBounceHigh = 8f;
		SetBallSpeed ();
		InstantiateBalls ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MoveBall ();
	}


	void InstantiateBalls(){
		if (this.gameObject.tag != "SmallestBall") {
			ball1 = Instantiate (originalBall);
			ball2 = Instantiate (originalBall);

			ballScript1 = ball1.GetComponent<BallScript> ();
			ballScript2 = ball2.GetComponent<BallScript> ();

			ball1.SetActive (false);
			ball2.SetActive (false);
		}

	}

	public void SetMoveLeft(bool moveLeft){
		this.moveLeft = moveLeft;
		//this.moveRight = !moveLeft;
	}

	public void SetMoveRight(bool moveRight){
		this.moveRight = moveRight;
		this.moveLeft = !moveRight;
	}

	void MoveBall(){
		if (moveLeft) {
		//	Debug.Log ("Moving Ball to left");
			Vector3 temp = transform.position;
			temp.x -= (forceX * Time.deltaTime);
			transform.position = temp;
		}
		if (moveRight) {
		//	Debug.Log ("Moving Ball to right");
			Vector3 temp = transform.position;
			temp.x += (forceX * Time.deltaTime);
			transform.position = temp;
		}
	}

	void InitializeBallsAndTurnOffCurrentBall(){
		Vector3 position = transform.position;

		ball1.transform.position = position;
		ballScript1.SetMoveLeft (true);

		ball2.transform.position = position;
		ballScript2.SetMoveRight (true);

		this.gameObject.SetActive (false);
		ball1.SetActive (true);
		ball2.SetActive (true);
		if (this.transform.position.y > -1) {
			ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, ballBounceNormal);
			ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, ballBounceNormal);
		} else if (transform.position.y <= -1) {
			ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, ballBounceHigh);
			ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, ballBounceHigh);
		}


	}

	void SetBallSpeed(){

		forceX = 2.5f;
		switch (this.gameObject.tag) {
		case "LargestBall":
		//	Debug.Log ("Setting Ball Speed");
			forceY = 11.5f;
			break;
		case "LargeBall":
			forceY = 10.5f;
			break;
		case "MediumBall":
			forceY = 9f;
			break;
		case "SmallBall":
			forceY = 8f;
			break;
		case "SmallestBall":
			forceY = 7f;
			break;
		default:
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D c){
	//	Debug.Log ("Triggered on Bottom Brick");
		if(c.tag == "Player"){
			GameplayController.playerTouched = true;
		}
		if(c.tag == "FirstArrow" || c.tag == "SecondArrow" || c.tag == "FirstStickyArrow" || c.tag == "SecondStickyArrow" ){
			AudioSource.PlayClipAtPoint (popClips [Random.Range (0, popClips.Length)], this.transform.position); 
			if (this.gameObject.tag != "SmallestBall") {
				InitializeBallsAndTurnOffCurrentBall ();
				c.gameObject.SetActive (false);
			} else {
				gameObject.SetActive (false);
			}
		}
		if (c.tag == "UnbreakableBrickTop" || c.tag == "UnbreakableBrickTopVertical" || c.tag == "BreakableBrickTop" || c.tag == "BreakableBrickTopVertical") {
			myRigidBody.velocity = new Vector2 (0, 5f);
		
		}else if (c.tag == "UnbreakableBrickBottom" || c.tag == "UnbreakableBrickBottomVertical" || c.tag == "BreakableBrickBottom" || c.tag == "BreakableBrickBottomVertical") {
			myRigidBody.velocity = new Vector2 (0, -2f);

		}else if (c.tag == "UnbreakableBrickLeft" || c.tag == "UnbreakableBrickLeftVertical" || c.tag == "BreakableBrickLeft" || c.tag == "BreakableBrickLeftVertical") {
			moveLeft = true;
			moveRight = false;

		}else if (c.tag == "UnbreakableBrickRight" || c.tag == "UnbreakableBrickRightVertical" || c.tag == "BreakableBrickRight" || c.tag == "BreakableBrickRightVertical") {
			moveRight = true;
			moveLeft = false;

		}

		if(c.tag == "BottomBrick"){
			myRigidBody.velocity = new Vector2 (
				0,
				forceY
			);	
		}


		if (c.tag == "LeftBrick") {
			moveLeft = false;
			moveRight = true;
		}

		if (c.tag == "RightBrick") {
			moveLeft = true;
			moveRight = false;
		}
	} 
}

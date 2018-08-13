using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

	private float arrowSpeed = 6.0f;
	private bool canShootStickyArrow;

	[SerializeField]
	private AudioClip stickyArrowClip;

	// Use this for initialization
	void Start () {
		canShootStickyArrow = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.tag == "FirstStickyArrow") {
			if (canShootStickyArrow) {
				ShootArrow ();
			}
		} else if (gameObject.tag == "SecondStickyArrow") {
			if (canShootStickyArrow) {
				ShootArrow ();
			}
		} else {
			ShootArrow ();
		}
	}

	void ShootArrow(){
		Vector3 temp = transform.position;
		temp.y += (arrowSpeed * Time.unscaledDeltaTime);
		transform.position = temp;

	}

	IEnumerator ResetStickyArrow(){
	//	Debug.Log ("inside ienumerator");
		yield return new WaitForSeconds (2.5f);
		if (this.gameObject.tag == "FirstStickyArrow") {
			PlayerScript.instance.SetShootOnce (true);
		}else if (this.gameObject.tag == "SecondStickyArrow") {
			PlayerScript.instance.SetShootTwice (true);
		}
		this.gameObject.SetActive (false);
	}
	void OnTriggerEnter2D(Collider2D c){
		if (c.tag == "LargestBall" || c.tag == "LargeBall" || c.tag == "MediumBall" || c.tag == "SmallBall" || c.tag == "SmallestBall") {
			gameObject.SetActive (false);
			if (this.gameObject.tag == "FirstArrow" || this.gameObject.tag == "FirstStickyArrow") {
				PlayerScript.instance.SetShootOnce (true);
			} else if (this.gameObject.tag == "SecondArrow" || this.gameObject.tag == "SecondStickyArrow") {
				PlayerScript.instance.SetShootTwice (true);
			}
		} 
		if (c.tag == "TopBrick" || c.tag == "UnbreakableBrickTop" || c.tag == "UnbreakableBrickBottom" || c.tag == "UnbreakableBrickLeft"
			|| c.tag == "UnbreakableBrickRight" || c.tag == "UnbreakableBrickBottomVertical") {
			if (this.gameObject.tag == "FirstArrow") {
				gameObject.SetActive (false);
				PlayerScript.instance.SetShootOnce (true);
			} else if (this.gameObject.tag == "SecondArrow") {
				gameObject.SetActive (false);
				PlayerScript.instance.SetShootTwice (true);
			}
			if (this.gameObject.tag == "FirstStickyArrow") {
				canShootStickyArrow = false;
				Vector3 targetPos = c.transform.position;
				Vector3 temp = transform.position;

				if (c.tag == "TopBrick") {
					targetPos.y -= .989f;
				} else if (c.tag == "UnbreakableBrickTop" || c.tag == "UnbreakableBrickBottom" || c.tag == "UnbreakableBrickLeft"
					|| c.tag == "UnbreakableBrickRight") {
					targetPos.y -= 0.75f;
				} else if (c.tag == "UnbreakableBrickBottomVertical") {
					targetPos.y -= 0.97f; 
				}
					
				temp.y = targetPos.y;
				transform.position = temp;
				AudioSource.PlayClipAtPoint (stickyArrowClip, temp);
				StartCoroutine ("ResetStickyArrow");

			}else if(gameObject.tag == "SecondStickyArrow"){
				canShootStickyArrow = false;
				Vector3 targetPos = c.transform.position;
				Vector3 temp = transform.position;

				if (c.tag == "TopBrick") {
					targetPos.y -= .989f;
				} else if (c.tag == "UnbreakableBrickTop" || c.tag == "UnbreakableBrickBottom" || c.tag == "UnbreakableBrickLeft") {
					targetPos.y -= 0.75f;
				} else if (c.tag == "UnbreakableBrickBottomVertical") {
					targetPos.y -= 0.97f; 
				}

				temp.y = targetPos.y;
				transform.position = temp;
				AudioSource.PlayClipAtPoint (stickyArrowClip, temp);
				StartCoroutine ("ResetStickyArrow");
			}
		}

		if (c.tag == "BreakableBrickTop" || c.tag == "BreakableBrickBottom" || c.tag == "BreakableBrickLeft"
			|| c.tag == "BreakableBrickRight" || c.tag == "BreakableBrickTopVertical" || c.tag == "BreakableBrickBottomVertical"
			|| c.tag == "BreakableBrickLeftVertical" || c.tag == "BreakableBrickRightVertical ") {

			BreakScript brick = c.gameObject.GetComponentInParent<BreakScript> ();
			brick.StartCoroutine (brick.BreakTheBrick());
			if (this.gameObject.tag == "FirstStickyArrow" || this.gameObject.tag == "FirstArrow") {
				PlayerScript.instance.SetShootOnce (true);
			}else if (this.gameObject.tag == "SecondStickyArrow" || this.gameObject.tag == "SecondArrow") {
				PlayerScript.instance.SetShootTwice (true);
			}
			this.gameObject.SetActive (false);
		}
	}
}

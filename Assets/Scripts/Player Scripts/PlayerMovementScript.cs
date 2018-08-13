using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

	public void OnPointerDown(PointerEventData data){
		if (this.gameObject.tag == "MoveLeftButton") {
			PlayerScript.instance.MoveThePlayerLeft ();
		}
		if (this.gameObject.tag == "MoveRightButton") {
			PlayerScript.instance.MoveThePlayerRight ();
		}
	}

	public void OnPointerUp(PointerEventData data){
		PlayerScript.instance.StopMoving ();
	}
}

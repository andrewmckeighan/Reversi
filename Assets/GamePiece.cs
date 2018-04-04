using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour {

	bool blackUp = true;
	// Use this for initialization
	void Start () {
		if(gameObject.transform.localEulerAngles.z == 0.0f && gameObject.transform.localEulerAngles.x == 0.0f){
			blackUp = false;
		}
		//Debug.Log("blackup = " + blackUp + " rotation  = " + gameObject.transform.localEulerAngles.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
}

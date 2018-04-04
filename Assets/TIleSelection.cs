using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIleSelection : MonoBehaviour {

	public float y;
	public float x;
	public bool isSelected = false;

	public bool flag = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isSelected){
			flag = true;
		}
	}

	void OnMouseDown(){
		x = gameObject.transform.position.x;
		y = gameObject.transform.position.z;
		isSelected = true;
		//Debug.Log("x= " + x + " y= " + y);
		
	}

}

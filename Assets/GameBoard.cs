using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

	//public TIleSelection cubeScript;
	private char[,] boardManager = new char[8,8]; // B=Black W=White X=NoPiece

	private GameObject[,] cubeManager = new GameObject[8,8];//manage which cubes aren't pressed.
	//private GameObject[,] cubeTracker = new GameObject[8,8];
	private GameObject cube;
	private int x;
	private int y;

	private int turn = 0;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 8; i++){
			for(int j = 0; j< 8; j++){
				cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				if((i==4 && j ==4) || (i==3 &&j ==3)){
					cube.GetComponent<Renderer>().material.color = Color.white;
					boardManager[i,j] = 'W';
				}else if((i==4 && j ==3) || (i==3 &&j ==4)){
					cube.GetComponent<Renderer>().material.color = Color.black;
					boardManager[i,j] = 'B';
				}else if(i == 6 && j == 2)
				{
					cube.GetComponent<Renderer>().material.color = Color.red;
				}else{
					cube.GetComponent<Renderer>().material.color = Color.gray;
					boardManager[i,j] = 'X';
				}
				
				cube.transform.localScale = new Vector3(0.95f,0.95f,0.95f);
				cube.transform.position = new Vector3(-3.5f+j,-0.5f,3.5f-i);//BM value is x+3.5 , z-3.5
				cube.AddComponent<TIleSelection>();
				cubeManager[i,j] = cube;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		char updateManager = 'X';
		foreach (GameObject cubes in cubeManager){
			if(cubes.GetComponent<TIleSelection>().isSelected && !cubes.GetComponent<TIleSelection>().flag){
				if(turn % 2 == 0){
					updateManager = 'W';
					cubes.GetComponent<Renderer>().material.color = Color.white;
				}else{
					updateManager = 'B';
					cubes.GetComponent<Renderer>().material.color = Color.black;
				}
				x = (int)(cubes.GetComponent<TIleSelection>().x +3.5);
				y = Mathf.Abs((int)(cubes.GetComponent<TIleSelection>().y -3.5));
				Debug.Log("X = " + x + " Y = " + y);
				boardManager[y,x] = updateManager;
				changeRowsAndColumns(y,x);
				turn++;
			}
		}
		//Debug.Log( " tile " + cube.GetComponent<TIleSelection>().x);
	}


	void changeRowsAndColumns(int x, int y){
		char change = 'B';
		if(turn%2 == 0){
			change = 'W';
		}
		for(int i = 0; i < 8; i++){//change the opposite color!
			if(boardManager[i, y] != change && boardManager[i,y] != 'X'){
				boardManager[i,y] = change;
				if(change == 'W'){
					cubeManager[i,y].GetComponent<Renderer>().material.color = Color.white;
				}else{
					cubeManager[i,y].GetComponent<Renderer>().material.color = Color.black;
				}
			}
			if(boardManager[x, i] != change && boardManager[x,i] != 'X'){
				boardManager[x,i] = change;
				if(change == 'W'){
					cubeManager[x,i].GetComponent<Renderer>().material.color = Color.white;
				}else{
					cubeManager[x,i].GetComponent<Renderer>().material.color = Color.black;
				}
			}
		}
	}

	private bool checkIfClickable(char turn, int x, int y){
		bool answer = false;
		int i = x;
		int j = y;
		bool flag = false;
		char opp = 'W';
		if(turn == 'W'){
			opp = 'B';
		}
		//check left
		if(x > 1){
			while(i > 0){
				if(!flag && boardManager[i, y] == opp){
					
				}
			}
		}

		

		return answer;
	}

	

}

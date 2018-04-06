using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * NOTES:
 *
 * one of the things I am going to note is how the tiles change. Instead of changing colors, I could have focused more on the actual boardmanager rather than tile colors. But as I used the tiles to visualize
 * it became my focus when generating turns.
 */
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
		if (turn % 2 == 0)
		{
			updateManager = 'W';
		}
		else
		{
			updateManager = 'B';
		}

		foreach (GameObject cubes in cubeManager){
			if (cubes.GetComponent<TIleSelection>().isSelected && !cubes.GetComponent<TIleSelection>().flag)
			{
				if (checkIfClickable(updateManager, (int) (cubes.GetComponent<TIleSelection>().x + 3.5),
					Mathf.Abs((int) (cubes.GetComponent<TIleSelection>().y - 3.5))))
				{
				

				if (updateManager == 'W')
				{
					//changes color of selected tile, aka it places a piece in a blank spot.
					cubes.GetComponent<Renderer>().material.color = Color.white;
				}
				else
				{
					cubes.GetComponent<Renderer>().material.color = Color.black;
				}

				x = (int) (cubes.GetComponent<TIleSelection>().x + 3.5);
				y = Mathf.Abs((int) (cubes.GetComponent<TIleSelection>().y - 3.5));
				//Debug.Log("X = " + x + " Y = " + y);
				boardManager[y, x] = updateManager;
				//changeRowsAndColumns(y,x);
				turn++;
					
			}else{
					//Debug.Log("boardManager reset = " + boardManager[y,x]);
					cubes.GetComponent<TIleSelection>().resetTile();
					
				}
		}
			
		}
		//Debug.Log( " tile " + cube.GetComponent<TIleSelection>().x);
	}

	/**
	 * I know how garbage this looks. I just didn't have the time to change so it could be simplified. Someday I'll come back and change it when I have time.
	 */
	private bool checkIfClickable(char turn, int y, int x){//checks if tile can be placed and changes colors of tiles between.
		bool answer = false;
		int i = x;
		int j = y;
		bool clickable = false; //true if the change is found.
		bool reverseFound = false; //true there is reverse next to clicked object.
		Color color = Color.black;
		char opp = 'W';
		
		if(turn.Equals('W')){
			opp = 'B';
			color = Color.white;
		}

		Debug.Log("turn = " + turn + " Opp = " + opp);
		//checks row up.
		while (x > 1 && i >=0)
		{

			if (boardManager[x - 1, y].Equals('X') || boardManager[x - 1, y].Equals(turn))
			{
				break;
			}
			if (boardManager[i , y].Equals(opp) && !reverseFound)
			{
				reverseFound = true;
			}else if(reverseFound && boardManager[i, y].Equals(turn))
			{
				clickable = true;
				answer = true;
				break;
			}
			
			i--;
		}
		if (clickable)
		{
			while (i < x)
			{
				cubeManager[i, y].GetComponent<Renderer>().material.color = color;
				boardManager[i, y] = turn;
				i++;
			}
		}
		
		//check down
		i = x;
		clickable = false;
		reverseFound = false;
		while (x < 7 && i <=7)
		{
			if (boardManager[x + 1, y].Equals('X') || boardManager[x + 1, y].Equals(turn))
			{
				break;
			}
			if (boardManager[i , y].Equals(opp) && !reverseFound)
			{
				reverseFound = true;
			}else if(reverseFound && boardManager[i, y].Equals(turn))
			{
				clickable = true;
				answer = true;
				break;
			}
			i++;
		}
		if (clickable)
		{
			while (i > x)
			{
				cubeManager[i, y].GetComponent<Renderer>().material.color = color;
				boardManager[i, y] = turn;
				i--;
			}
		}
		
		
		
		//check left
		clickable = false;
		reverseFound = false;
		while (y > 1 && j >=0)
		{
			Debug.Log("x = " + x + " j = " + j + " Color " + boardManager[x, j]);
			Debug.Log("BoardManager.equals(turn) = " + boardManager[x,j].Equals(turn));

			if (boardManager[x, y-1].Equals('X') || boardManager[x, y - 1].Equals(turn))
			{
				Debug.Log("BREAK");
				break;
			}
			if (boardManager[x , j].Equals(opp) && !reverseFound)
			{
				Debug.Log("reversefound");
				reverseFound = true;
			}else if(reverseFound && boardManager[x, j].Equals(turn))
			{
				Debug.Log("Clickable");
				clickable = true;
				answer = true;
				break;
			}
			
			j--;
		}
		if (clickable)
		{
			while (j < y)
			{
				cubeManager[x, j].GetComponent<Renderer>().material.color = color;
				boardManager[x, j] = turn;
				j++;
			}
		}
		
		
		//check left
		clickable = false;
		reverseFound = false;
		while (y < 7 && j <=7)
		{
			if (boardManager[x, y + 1].Equals('X') || boardManager[x, y + 1].Equals(turn))
			{
				break;
			}
			if (boardManager[x , j].Equals(opp) && !reverseFound)
			{
				reverseFound = true;
			}else if(reverseFound && boardManager[x, j].Equals(turn))
			{
				clickable = true;
				answer = true;
				break;
			}
			j++;
		}
		if (clickable)
		{
			while (j > y)
			{
				cubeManager[x, j].GetComponent<Renderer>().material.color = color;
				boardManager[x, j] = turn;
				j--;
			}
		}

		i = x;
		j = y;
		clickable = false;
		reverseFound = false;
		while (y < 7 && j <=7 && x < 7 && i <= 7)
		{
			if (boardManager[x + 1, y + 1].Equals('X') || boardManager[x + 1, y + 1].Equals(turn))
			{
				break;
			}
			if (boardManager[i , j].Equals(opp) && !reverseFound)
			{
				reverseFound = true;
			}else if(reverseFound && boardManager[i, j].Equals(turn))
			{
				clickable = true;
				answer = true;
				break;
			}
			j++;
			i++;
		}
		if (clickable)
		{
			while (j > y && i > x)
			{
				cubeManager[i, j].GetComponent<Renderer>().material.color = color;
				boardManager[i, j] = turn;
				j--;
				i--;
			}
		}
		
		
		
		
		i = x;
		j = y;
		clickable = false;
		reverseFound = false;
		while (y > 0 && j >= 0 && x < 7 && i <= 7)
		{
			if (boardManager[x + 1, y - 1].Equals('X') || boardManager[x + 1, y - 1].Equals(turn))
			{
				break;
			}
			if (boardManager[i , j].Equals(opp) && !reverseFound)
			{
				reverseFound = true;
			}else if(reverseFound && boardManager[i, j].Equals(turn))
			{
				clickable = true;
				answer = true;
				break;
			}
			j--;
			i++;
		}
		if (clickable)
		{
			while (j < y && i > x)
			{
				cubeManager[i, j].GetComponent<Renderer>().material.color = color;
				boardManager[i, j] = turn;
				j++;
				i--;
			}
		}
		
		i = x;
		j = y;
		clickable = false;
		reverseFound = false;
		while (y > 0 && j >= 0 && x > 0 && i >= 0)
		{
			if (boardManager[x - 1, y - 1].Equals('X') || boardManager[x - 1, y - 1].Equals(turn))
			{
				break;
			}
			if (boardManager[i , j].Equals(opp) && !reverseFound)
			{
				reverseFound = true;
			}else if(reverseFound && boardManager[i, j].Equals(turn))
			{
				clickable = true;
				answer = true;
				break;
			}
			j--;
			i--;
		}
		if (clickable)
		{
			while (j < y && i < x)
			{
				cubeManager[i, j].GetComponent<Renderer>().material.color = color;
				boardManager[i, j] = turn;
				j++;
				i++;
			}
		}
		
		i = x;
		j = y;
		clickable = false;
		reverseFound = false;
		while (y < 7 && j <= 7 && x > 0 && i >= 0)
		{
			if (boardManager[x - 1, y + 1].Equals('X') || boardManager[x - 1, y + 1].Equals(turn))
			{
				break;
			}
			if (boardManager[i , j].Equals(opp) && !reverseFound)
			{
				reverseFound = true;
			}else if(reverseFound && boardManager[i, j].Equals(turn))
			{
				clickable = true;
				answer = true;
				break;
			}
			j++;
			i--;
		}
		if (clickable)
		{
			while (j > y && i < x)
			{
				cubeManager[i, j].GetComponent<Renderer>().material.color = color;
				boardManager[i, j] = turn;
				j--;
				i++;
			}
		}
		
		
		

		Debug.Log("clickable = " + clickable);
		return answer;
	}
	
	
}

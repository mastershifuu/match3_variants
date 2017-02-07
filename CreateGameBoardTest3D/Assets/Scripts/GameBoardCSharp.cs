using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameBoardCSharp : MonoBehaviour {

	//GameObject currentTarget=null;
	GameObject ball;
	public int rate = 10;
	public bool ballSet = true;

	List<Color> colors = new List<Color>();

	void Awake(){
		colors.Add (Color.red);
		colors.Add (Color.blue);
		colors.Add (Color.green);
		colors.Add (Color.yellow);
		colors.Add (Color.white);
	}

	string[] objectColors = { "red", "blue", "green", "yellow", "white" };

	void Start () {

		// GameObject GemCont = GameObject.FindGameObjectWithTag("GameController");
		// GameController GameControllerScript = (GameController) GemCont.GetComponent(typeof(GameController));

		CreateGameBoard(15,15);

		// GameControllerScript.BallsDestroyer();

	}


	void CreateGameBoard(uint cols, uint rows){
		
		GameObject ball = (GameObject)Resources.Load("GameBall");

		for (int i = 0; i < cols; i++) {
			for (int j = 0; j < rows; j++) {
				GameObject newBlock = (GameObject)Instantiate(ball,new Vector3(i,0,j), Quaternion.identity);
				newBlock.name="Block: " + i + "," + j;
				Color blockColor;

				int ValueColor = Random.Range (0, 5);
				blockColor = colors [ValueColor];
				newBlock.tag = objectColors [ValueColor];

				newBlock.GetComponent<Renderer>().material.color=blockColor;
				newBlock.transform.parent=transform;
			}
		}
		transform.position= new Vector3(cols-15,0,rows-15);
	}
}

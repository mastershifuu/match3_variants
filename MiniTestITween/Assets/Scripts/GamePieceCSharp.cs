using UnityEngine;
using System.Collections;

public class GamePieceCSharp : MonoBehaviour {

	private Color currentColor;
	private Vector3 currentPosition;
	GameBoardCSharp gameBoard;
	bool isActive;

	void Start () {
		currentColor=GetComponent<Renderer>().material.color;
		currentPosition=transform.position;
	}

	void SetGameboard(GameBoardCSharp gameBoard){
		this.gameBoard=gameBoard;
	}
	

}

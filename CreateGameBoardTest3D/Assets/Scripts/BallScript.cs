using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	void Update()
	{
		
	}

	void OnMouseDown () {
		if (InMove()){ 
			return;
		}
		
		GameObject GaCont = GameObject.FindGameObjectWithTag("GameController");
		GameController GameControllerScript = (GameController) GaCont.GetComponent(typeof(GameController));
		
		if (!GameControllerScript.SetActive){
			GameControllerScript.FirstObject = this.gameObject.name;
			iTween.ShakeScale (this.gameObject, iTween.Hash ("z", 0.3f, "x", 0.3f, "time", 0.8f));
	
			GameControllerScript.SetActive = true;
		}
		else{
			GameControllerScript.SecondObject = this.gameObject.name;
			GameControllerScript.ToMoveTriger = true;

			GameControllerScript.MyUpdate();
			//GameControllerScript.BallDestttroyer();
		}
	}
	
	bool InMove() {
		return (iTween.Count(this.gameObject) > 0);
	}

	public void DestroyMatched(){
		iTween.ScaleTo (this.gameObject, iTween.Hash("x", 0,"z", 0, "time", 2.0f));
		this.gameObject.tag = "NotActive";	
	}

	public void Echo(){
		Debug.Log("hi from: " + this.gameObject.name);
	}

}

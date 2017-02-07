using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour {
	string[] objectColors = { "red", "blue", "green", "yellow", "white" };
	public string FirstObject;
	public  string SecondObject;
	public  bool ToMoveTriger = false;
	public  bool SetActive = false;
	public GameObject FirstTry;

	private GameObject tempFirst;
	private GameObject tempSecond;
	Vector3 PosFirst;
	Vector3 PosSecond;

	public bool Check_mark = false;

	public int GemCounter = 3;
	GameObject[] FindList;
	int FindListCounter=0;

	Vector3[] GenCoordTest(Vector3 CurPos){
		Vector3[] Near = new Vector3[4] ;
		Near [0] = CurPos; Near [0].x++ ;
		Near [1] = CurPos; Near [1].x-- ;
		Near [2] = CurPos; Near [2].z++ ;
		Near [3] = CurPos; Near [3].z-- ;
		return Near ; 
	}

	void Update () {				
		if (ToMoveTriger){
			ToMoveTriger = false;
			SetActive = false;
		}
		
	}

	public void MyUpdate(){
			tempFirst = FirstTry.transform.Find (FirstObject).gameObject;
			tempSecond = FirstTry.transform.Find (SecondObject).gameObject;
			PosFirst = tempFirst.transform.position;
			PosSecond = tempSecond.transform.position;

			int horzDist = (int)Mathf.Abs (PosFirst.x - PosSecond.x);
			int vertDist = (int)Mathf.Abs (PosFirst.z - PosSecond.z);

			if ((horzDist == 1 && vertDist == 0) || (horzDist == 0 && vertDist == 1)) {

				iTween.MoveTo(tempFirst, PosSecond, 1.5f);
				iTween.MoveTo(tempSecond, PosFirst, 1.5f);

				 tempFirst.transform.position = PosSecond;
				 tempSecond.transform.position = PosFirst;

		
				 if ( BallsDestroyer(tempFirst, tempSecond) != 0){
				 	iTween.MoveTo(tempFirst, PosFirst, 1.5f);
				 	iTween.MoveTo(tempSecond, PosSecond, 1.5f);
				 }	
			}
	}
 
	int BallsDestroyer(GameObject tempFirst, GameObject tempSecond){
		int a = DelByTag(tempFirst);
		int b = DelByTag(tempSecond);
		if ((a == 0 && b == 1)|| (a == 1 && b == 0) || (a == 0 && b == 0)){
			BallDown();
			return 0;
		} else{
			return 1;
		}
		
	}

	int DelByTag(GameObject TempObj){

			//Debug.Log ("Color to Delete : "+TempObj.tag);
		GameObject[] ColorMass = GameObject.FindGameObjectsWithTag (TempObj.tag);

		
		FindList = new GameObject[ColorMass.Length];
		FindListCounter=0;
			// поиск соседей
		Search (TempObj, ColorMass);
		if (FindListCounter >= GemCounter){
		for(int i=0; i < FindListCounter; i++) FreeGem(FindList[i]);
		 return 0;
		} 
		else{
			return 1;
		}

	}

	GameObject[] Duble(GameObject thatObgect,GameObject[] Gem){

			/*уберем текущий объект из списка*/
		GameObject[] GemMass = new GameObject[Gem.Length - 1];
		int Counter = 0;
			/*сравниваем все объекты ищем наш*/
		foreach (GameObject GemObj in Gem)
			if (thatObgect.Equals (GemObj) != true){
			GemMass [Counter++] = GemObj;
			}
			//добавляем проверенный объект в массив проверенных
		FindList [FindListCounter] = thatObgect;
		FindListCounter++;
			//возвращаем массив без нашего объекта
		return GemMass;
		}

	void Search(GameObject thatObject,GameObject[] ColorMass){

			/*Убираем выбранный объект*/
		ColorMass = Duble (thatObject, ColorMass);
			/*п координаты соседей*/
		Vector3[] Neighbor = GenCoordTest (thatObject.transform.position);
			//перебираем оставшиеся объекты и ищем соседей
		foreach (GameObject GemObj in ColorMass) {
		Vector3 ObjectPosition=GemObj.transform.position;
		foreach(Vector3 Gems in Neighbor)
		if(Gems==ObjectPosition){
			//нашли объект запускаем Search
			Search(GemObj,ColorMass);
			break;
			}
		}

	}

	void FreeGem (GameObject Gem){

		iTween.ScaleTo (Gem, iTween.Hash("x", 0,"z", 0, "time", 2.0f));
		Gem.tag = "NotActive";
		//Gem.SetActive (false);


	}

	void MoveDown(){

		List<GameObject> AllGem = new List<GameObject>();

		for(int i = 0; i < 5; i++){
			GameObject[] TampAllGem = GameObject.FindGameObjectsWithTag(objectColors[i]);
			for(int j = 0; j < TampAllGem.Length; j++){
				AllGem.Add(TampAllGem[j]);
			}
			
		}
		GameObject[] NotActGem = GameObject.FindGameObjectsWithTag("NotActive");

		foreach (GameObject NotGem1 in NotActGem){
			Vector3 Coord = NotGem1.transform.position;
			Coord.z++;
			foreach(GameObject NotGem2 in NotActGem){ 
				if(Coord == NotGem2.transform.position){
					Coord.z++;
					break;
				}
			}
		}
	}

		public void BallDown(){
		List<GameObject> AllGem = new List<GameObject>();
		for(int i = 0; i < 5; i++){
			GameObject[] TampAllGem = GameObject.FindGameObjectsWithTag(objectColors[i]);
			for(int j = 0; j < TampAllGem.Length; j++){
				AllGem.Add(TampAllGem[j]);
			}
		}
		GameObject[] EmptyBalls = GameObject.FindGameObjectsWithTag("NotActive");
		foreach (GameObject ActGem in AllGem){
			Vector3 Coord = ActGem.transform.position;
			int Count = 0;
			foreach (GameObject Check in EmptyBalls){
				Vector3 CheckCoord = Check.transform.position;
				if (CheckCoord.x == Coord.x){
					if (CheckCoord.z < Coord.z){
						Count++;
					}
				}
			}
			if (Count>0){
				Vector3 NewCoord = Coord;
				NewCoord.z -= Count;
				iTween.MoveTo(ActGem.gameObject, NewCoord, 1.5f);
				Count = 0;
			}
		}
	NewBalls();
	}

	public void NewBalls(){
		GameObject[] NotActGem = GameObject.FindGameObjectsWithTag("NotActive");
		int[] BallsCount = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
		foreach (GameObject NoActGem in NotActGem){
				Vector3 NewCoord = NoActGem.transform.position;
				
				NewCoord.z = 14 - BallsCount[(int) NewCoord.x];
				Debug.Log ("Here: " + NewCoord);
				BallsCount[(int) NewCoord.x]++;
				Vector3 BigCoord = NewCoord;
				BigCoord.z += 10;
				iTween.MoveTo(NoActGem, BigCoord, 1.5f);
				iTween.ScaleTo (NoActGem, iTween.Hash("x", 1,"z", 1, "time", 2.0f));
				
		}
	}
}

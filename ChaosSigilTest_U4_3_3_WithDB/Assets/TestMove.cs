using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMove : MonoBehaviour {

	float time;
	// Use this for initialization
	void Start () {
		List<Vector2> posList = new List<Vector2> ();
		posList.Add (new Vector2(0.0f,-2.7f));
		posList.Add (new Vector2(-4.0f,0.0f));
		moveSeq (posList);

//		StartCoroutine ("yA");
//		Coroutine co = StartCoroutine ("yC");

	}
	void SequenceActions(params object[] actions){
		
	}

	IEnumerator yA()
	{
		int i = 0;
		i++;
		Debug.Log ("A"+i);
		yield return null;
		i++;
		Debug.Log ("A"+i);
		yield return null;
		i++;
		Debug.Log ("A"+i+" -> yB");
		yield return StartCoroutine ("yB");
		i++;
		Debug.Log("A"+i+" <- yB");
	}

	IEnumerator yB()
	{
		Debug.Log ("B1");
		yield return null;
		Debug.Log("B2");
		yield return null;
	}
	IEnumerator yC()
	{
		Debug.Log ("C1");
		yield return null;
		Debug.Log ("C2");
		yield return null;
		Debug.Log ("C3");
		yield return null;
	}
	// Update is called once per frame
	void Update () {
//		List<Vector2> posList = new List<Vector2> ();
//		posList.Add (new Vector2(4.0f,0.0f));
//		posList.Add (new Vector2(-4.0f,-3.0f));
//		moveSeq (posList);
//
//		time += Time.deltaTime;
//		transform.position = Vector2.Lerp(posList[0], posList[1], time/15.0f);

	}

	void moveSeq(List<Vector2> posList){


	}
	void moveBySpeed(Vector2 startPos, Vector2 endPos, float speed){
	
	}
	void moveByTime(Vector2 startPos, Vector2 endPos, float time){
	
	}
}

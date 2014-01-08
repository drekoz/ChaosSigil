using UnityEngine;
using System.Collections;

public class CSINTTrainingSceneController : CSSceneController {

	public CSMonster monster;
	// Use this for initialization
	protected override void Start () {
		base.Start();
		monster = GameObject.Find("Monster").GetComponent<CSMonster>();
//		CSINTTrainingBoard.createRandomRune(INTTrainingRuneType.kINTTrainingRuneTypeArrow);
	}

	public void ScoreByRuneCount(object param)
	{
		Debug.Log("SCORE!"+(int)param);
		monster.ChangeState(CSGOState.Happy);
	}

	public void FinishTimer()
	{
		Debug.Log("Timer is 00!");
	}
}

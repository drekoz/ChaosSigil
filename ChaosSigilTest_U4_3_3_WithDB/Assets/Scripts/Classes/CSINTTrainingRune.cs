using UnityEngine;
using System.Collections;

public enum INTTrainingRuneType {
	kINTTrainingRuneTypeNone = 0,
	kINTTrainingRuneTypeArrow = 1,
	kINTTrainingRuneTypeInfi = 2,
	kINTTrainingRuneTypeLine = 3,
	kINTTrainingRuneTypeRect = 4,
	kINTTrainingRuneTypeN = 5
}

public enum INTTrainingRuneState {
	kINTTrainingRuneStateNone,
	kINTTrainingRuneStateWaitToFall,
	kINTTrainingRuneStateFalling,
	kINTTrainingRuneStateInSlot
}



public class CSINTTrainingRune : CSGameObject {

	public const int RUNE_SLOT_NONE = -99;
	


	public INTTrainingRuneType intTrainingRuneType;
	public int currentSlotNo;
	public bool isVisited;
	public INTTrainingRuneState intTrainingRuneState;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		CSINTTrainingBoard parent = transform.parent.GetComponent<CSINTTrainingBoard>();
		if(parent != null)
		{
			parent.runeOnMouseDown(this);
		}
		else
		{

		}
	}
}



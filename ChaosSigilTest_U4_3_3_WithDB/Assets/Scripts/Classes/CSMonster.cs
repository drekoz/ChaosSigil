using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CSMonster : CSGameObject{

//	protected virtual void Update()
//	{
//		base.Update();
//	}
	public CSFood targetFood;
	public CSFountain targetFountain;

	Vector2 currentTargetWayPoint;

	public float delayBetweenWalkSecond = 2.0f;
	private bool delayBetweenWalk = false;
	public bool enableScratch = false;
	public bool isScratchd = false;



	public Vector2 prevScratchPos;
	public Vector2 curScratchPos;
	//AnimatorState
	public static int animState_Base_Happy = Animator.StringToHash("Base Layer.Happy");

	public static int animState_Base_Scratched = Animator.StringToHash("Base Layer.Scratched");
	public static int animState_Base_WaitForNextScratched = Animator.StringToHash("Base Layer.WaitForNextScratched");
	public static int animState_Base_FinishScratched = Animator.StringToHash("Base Layer.FinishScratched");

	public static int animState_Base_Tackle = Animator.StringToHash("Base Layer.Tackle");
	public static int animState_Base_PreEat = Animator.StringToHash("Base Layer.PreEat");
	public static int animState_Base_Eat = Animator.StringToHash("Base Layer.Eat");
	public static int animState_Base_IdleAfterEat = Animator.StringToHash("Base Layer.IdleAfterEat");
	public static int animState_Base_WaitForNextEat = Animator.StringToHash("Base Layer.WaitForNextEat");


	private int monsterID;
	private string monsterName;
	private string monsterNameByPlayer;
	private string monsterDesc;

	private int baseSTR;
	private int baseAGI;
	private int baseINT;

	private int trainedSTR;
	private int trainedAGI;
	private int trainedINT;

	protected override void Awake()
	{
		base.Awake();
//		useWalkAnimation = true;

		if(Application.loadedLevelName == "FarmScene"){
			farmSceneController = GameObject.Find("FarmSceneController").GetComponent<CSFarmSceneController>();
			ChangeState(CSGOState.FarmIdleWalk);
			StartDelayBetweenWalk();
		}
		else
		{
			ChangeState(CSGOState.Idle);
		}
	}

	protected override void Start()
	{
		base.Start();
	}
	protected override void Update()
	{
		base.Update();


	}

	public override void ChangeState (CSGOState newState)
	{
		base.ChangeState (newState);

		gameObjectState = newState;

		if(CSGOState.FarmIdleWalk == newState)
		{
			Debug.Log("CSMonster -> ChangeState(FarmIdleWalk)");
			enableScratch = true;
		}
		else if(CSGOState.FarmEat == newState)
		{
			Debug.Log("CSMonster -> ChangeState(FarmEat)");
		}
		else if(CSGOState.FarmShower == newState)
		{
			Debug.Log("CSMonser -> ChangeState(FarmShower)");
			enableScratch = false;
		}
		else if(CSGOState.Scratched == newState)
		{
			Debug.Log("CSMonser -> ChangeState(Scratched)");
		}
		else if(CSGOState.Happy == newState)
		{
			Debug.Log("CSMonster -> ChangeState(Happy)");
		}
	}

	public override void ResetStateFromNone()
	{
		base.ResetStateFromNone();

		if(gameObjectState == CSGOState.None)
		{
			if(animator.GetCurrentAnimatorStateInfo(0).nameHash == animState_Base_Stop || animator.GetCurrentAnimatorStateInfo(0).nameHash == animState_Base_Idle)
			{
				if(Application.loadedLevelName == "FarmScene")
				{
					ChangeState(CSGOState.FarmIdleWalk);
				}
				else
				{
					ChangeState(CSGOState.Idle);
				}
			}
		}
	}

	public override void UpdateState()
	{
		base.UpdateState();

		//State - FarmIdleWalk
		if(gameObjectState == CSGOState.FarmIdleWalk)
		{
			if(IsMoving || delayBetweenWalk)
			{
				return;
			}

			List<Vector2> tempWaypointList = new List<Vector2>();
			
			foreach(Vector2 tempPos in farmSceneController.waypointList)
			{
				
				if(tempPos != currentTargetWayPoint)
				{
					tempWaypointList.Add(tempPos);
				}
			}
			
			int tempWayPointIndex = UnityEngine.Random.Range(0,tempWaypointList.Count);
			currentTargetWayPoint = tempWaypointList[tempWayPointIndex];
			Move(currentTargetWayPoint);
		}
		//State - FarmEat
		else if(gameObjectState == CSGOState.FarmEat)
		{
			if(Application.loadedLevelName == "FarmScene")
			{
				Vector3 eatPosition = new Vector3(CSFarmSceneController.eatPosition.x,CSFarmSceneController.eatPosition.y,transform.position.z);
				if(transform.position != eatPosition)
				{
					Move(CSFarmSceneController.eatPosition,speed*2.0f);
				}
				//reset to none if food is already dropped and eaten
				else if(targetFood == null && farmSceneController.dropFood)
				{
					ChangeState(CSGOState.None);
				}
				else
				{
					if(targetFood != null)
					{
						FlipX(false);

						if(animator.GetCurrentAnimatorStateInfo(0).nameHash != animState_Base_Eat)
						{
							if(animator.GetCurrentAnimatorStateInfo(0).nameHash == animState_Base_WaitForNextEat)
							{
								EatFood();
							}
							else if(targetFood.currentEatCount == 0 && animator.GetCurrentAnimatorStateInfo(0).nameHash != animState_Base_PreEat)
							{
								animator.Play(animState_Base_PreEat);
							}
						}

					}
				}

			}

		}
		//State FarmShower
		else if (gameObjectState == CSGOState.FarmShower)
		{
			if(transform.position != new Vector3(CSFarmSceneController.showerPosition.x,CSFarmSceneController.showerPosition.y,transform.position.z))
			{
				Move(CSFarmSceneController.showerPosition,speed*2.0f);
			}
			else if(targetFountain.animator.GetCurrentAnimatorStateInfo(0).nameHash == CSFountain.animFadeInState)
			{
				enableScratch = true;
			}
			else if(targetFountain.animator.GetCurrentAnimatorStateInfo(0).nameHash == CSFountain.animFadeOutState)
			{
				ChangeState(CSGOState.None);
			}
		}
		//State Scratched
		else if (gameObjectState == CSGOState.Scratched)
		{
			if(isScratchd)
			{
				animator.Play(animState_Base_Scratched);
			}
		}
		//State Happy
		else if (gameObjectState == CSGOState.Happy)
		{
			if(animator.GetCurrentAnimatorStateInfo(0).nameHash != animState_Base_Happy)
			{
				animator.Play(animState_Base_Happy);
			}
			if(animator.GetCurrentAnimatorStateInfo(0).nameHash == animState_Base_Stop)
			{
				ChangeState(CSGOState.None);
			}
		}

	}
	protected override void FinishWalk()
	{
		if(Application.loadedLevelName == "FarmScene")
		{
			StartDelayBetweenWalk();
		}
	}
	void StartDelayBetweenWalk()
	{
		delayBetweenWalk = true;
		Invoke("DelayBetweenWalk",delayBetweenWalkSecond);
	}
	void DelayBetweenWalk()
	{
		delayBetweenWalk = false;
	}
	//----
	public void EatFood()
	{

		if(targetFood != null)
		{
			animator.Play(animState_Base_Eat);
		}
	}
	public void ShrinkFood()
	{
		targetFood.Eaten();
	}
	public void OnMouseDown()
	{
		Vector3 pos3 = Input.mousePosition;
		Vector2 pos2 = new Vector2(pos3.x,pos3.y);
		prevScratchPos = pos2;
		curScratchPos = pos2;
	}
	public void OnMouseUp()
	{
		if(gameObjectState == CSGOState.Scratched)
		{
			ChangeState(CSGOState.None);
		}
		isScratchd = false;
	}
	public void OnMouseDrag()
	{
//		Debug.Log("Drag");
		Vector3 pos3 = Input.mousePosition;
		Vector2 pos2 = new Vector2(pos3.x,pos3.y);
		curScratchPos = pos2;

		if(prevScratchPos != curScratchPos)
		{
			prevScratchPos = curScratchPos;
			if(IsMoving)
			{
				BreakIMove();
			}
			isScratchd = true;
		}
		else
		{
			isScratchd = false;
		}

		//farmIdle scratcged
		if(animator.GetCurrentAnimatorStateInfo(0).nameHash != animState_Base_Scratched && gameObjectState == CSGOState.FarmIdleWalk)
		{
			Scratched();
		}
		//farmShower scratchged
		if(animator.GetCurrentAnimatorStateInfo(0).nameHash != animState_Base_Scratched && gameObjectState == CSGOState.FarmShower)
		{
			ScratchedShower();
		}
	}
	public void Scratched()
	{
		if(!enableScratch)
		{
			return;
		}
		BreakIMove();
		ChangeState(CSGOState.Scratched);
	}
	public void ScratchedShower()
	{
		if(!enableScratch)
		{
			return;
		}
		BreakIMove();
		animator.Play(animState_Base_Scratched);
	}
	public void FinishScratchedAnimation()
	{
		if(gameObjectState == CSGOState.Scratched)
		{
			PopHeart();
			ScratchBuffUp();
		}
		else if(gameObjectState == CSGOState.FarmShower)
		{
			PopShiny();
			ScratchShowerBuffUp();
		}
	}
	public void PopShiny()
	{
		Debug.Log("PopShiny");
	}
	public void ScratchShowerBuffUp()
	{
		Debug.Log("ScratchShowerBuffUp");
	}
	public void PopHeart()
	{
		Debug.Log("PopHeart");
	}
	public void ScratchBuffUp()
	{
		Debug.Log("ScratchBuffUp");
	}

	//Accessor
	public int MonsterID
	{
		get
		{
			return monsterID;
		}
		set
		{
			monsterID = value;
		}
	}
	public int BaseSTR
	{
		get
		{
			return baseSTR;
		}
		set
		{
			baseSTR = value;
		}
	}
	public int BaseAGI
	{
		get
		{
			return baseAGI;
		}
		set
		{
			baseAGI = value;
		}
	}
	public int BaseINT
	{
		get
		{
			return baseINT;
		}
		set
		{
			baseINT = value;
		}
	}
	public int TrainedSTR
	{
		get
		{
			return trainedSTR;
		}
		set
		{
			trainedSTR = value;
		}
	}
	public int TrainedAGI
	{
		get
		{
			return trainedAGI;
		}
		set
		{
			trainedAGI = value;
		}
	}
	public int TrainedINT
	{
		get
		{
			return trainedINT;
		}
		set
		{
			trainedINT = value;
		}
	}
	public string MonsterDesc
	{
		get
		{
			return monsterDesc;
		}
		set
		{
			monsterDesc = value;
		}
	}
}

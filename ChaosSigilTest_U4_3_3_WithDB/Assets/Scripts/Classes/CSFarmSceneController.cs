using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class CSFarmSceneController : CSSceneController {

	public List<Vector2> waypointList = new List<Vector2>();
	Vector2 currentTargetWayPoint;
	public static Vector2 eatPosition = new Vector2(0.0f,-2.3705f);
	public static Vector2 showerPosition = new Vector2(0.0f,-2.3705f);

	public Vector2 meatPosition = new Vector2(-2.795f,-1.428f);
	public Vector2 mushroomPosition = new Vector2(-2.93f,-1.36f);
	public Vector2 fountainPosition = new Vector2(0.0f,1.8f);

	public static Vector3 spawnPosition = new Vector3(0.0f,-2.3705f,0.0f);
//	private bool isOrderToEat;
//	private bool isOrderToShower;
	private string foodName;
	public bool dropFood;

	CSMonster monster;
	CSFood food;
	CSFountain fountain;
	protected override void Awake()
	{
		base.Awake();
		waypointList.Add(new Vector2(-0.6f,-1.5f));
		waypointList.Add(new Vector2(-0.5f,-2.8f));
		waypointList.Add(new Vector2(-0.4f,-2.2f));
		waypointList.Add(new Vector2(-3.0f,-2.4f));
		waypointList.Add(new Vector2(3.0f,-2.4f));
	}
	protected override void Start()
	{
		base.Start();


		monster = CSGameManager.Instance.LoadMonsterGameObject();
		monster.enableScratch = true;

		GameObject monsterGO = monster.gameObject;
		monsterGO.transform.position = spawnPosition;
		monsterGO.name = "Monster";
		Debug.Log("Loaded Monster:"+monster.BaseSTR+"+("+monster.TrainedSTR+")");
	}


	//eat
	public void OrderMonsterToEatMeat()
	{
		if(food != null || monster.gameObjectState != CSGOState.FarmIdleWalk)
		{
			return;
		}
		dropFood = false;
		foodName = "Meat";
		monster.ChangeState(CSGOState.FarmEat);
	}
	public void OrderMonsterToEatMushroom()
	{
		if(food != null || monster.gameObjectState != CSGOState.FarmIdleWalk)
		{
			return;
		}
		dropFood = false;
		foodName = "Mushroom";
		monster.ChangeState(CSGOState.FarmEat);
	}
	public void OrderToEatTracker()
	{
		if(monster.gameObjectState == CSGOState.FarmEat)
		{
			//createFood
			if(monster.transform.position == new Vector3(eatPosition.x,eatPosition.y,0.0f) && food == null && !dropFood)
			{
				dropFood = true;
				CreateFood();
			}
			//give food to monster
			if(monster.transform.position == new Vector3(eatPosition.x,eatPosition.y,0.0f) && food != null)
			{
				monster.targetFood = food;
			}
		}

	}
	void CreateFood()
	{
		string prefabPath = "prefabs/Foods/prefab_"+foodName;
		Debug.Log(prefabPath);
		UnityEngine.Object prefab = Resources.Load(prefabPath, typeof(GameObject));
		GameObject clone = Instantiate(prefab) as GameObject;
		clone.name = "Food";
		//			Debug.Log("POS1"+clone.transform.position);
		clone.transform.position = transform.position;
		//			Debug.Log("POS2"+clone.transform.position);
		food = clone.GetComponent<CSFood>();
		if(foodName == "Meat")
		{
			food.transform.position = new Vector3(meatPosition.x,meatPosition.y,food.transform.position.z);
		}
		else if(foodName == "Mushroom")
		{
			food.transform.position = new Vector3(mushroomPosition.x,mushroomPosition.y,food.transform.position.z);
		}

		food.animator.SetTrigger("Pop");
	}
	//shower
	void OrderMonsterToShower()
	{
		if(monster.gameObjectState != CSGOState.FarmIdleWalk || fountain != null)
		{
			return;
		}
		monster.ChangeState(CSGOState.FarmShower);
	}

	public void OrderToShowerTracker()
	{
		if(monster.gameObjectState == CSGOState.FarmShower)
		{
			//createFountain
			if(monster.transform.position == new Vector3(showerPosition.x,showerPosition.y,0.0f) && fountain == null)
			{
//				monster.FlipX(false);
				CreateFountain();
				monster.targetFountain = fountain;
			}
			//enableScratch
//			else if(monster.transform.position == new Vector3(showerPosition.x,showerPosition.y,0.0f) && fountain != null && fountain.animator.GetCurrentAnimatorStateInfo(0).nameHash == CSFountain.FadeInState)
//			{
//				monster.enableScratch = true;
////				Debug.Log("SCRATCH");
//			}
//			else if(monster.transform.position == new Vector3(showerPosition.x,showerPosition.y,0.0f) && fountain != null && fountain.animator.GetCurrentAnimatorStateInfo(0).nameHash == CSFountain.animFadeOutState )
//			{
//				monster.isOrderedToShower = false;
//			}
		}
	}
	void CreateFountain()
	{
		string prefabPath = "prefabs/prefab_Fountain";
		UnityEngine.Object prefab = Resources.Load(prefabPath, typeof(GameObject));
		GameObject clone = Instantiate(prefab) as GameObject;
		Debug.Log("CLONE!"+clone);
		//			Debug.Log("POS1"+clone.transform.position);
//		clone.transform.position = transform.position;
		//			Debug.Log("POS2"+clone.transform.position);
		fountain = clone.GetComponent<CSFountain>();
		fountain.transform.position = new Vector3(fountainPosition.x,fountainPosition.y,fountain.transform.position.z);
//		if(foodName == "Meat")
//		{
//			food.transform.position = new Vector3(meatPosition.x,meatPosition.y,food.transform.position.z);
//		}
//		else if(foodName == "Mushroom")
//		{
//			food.transform.position = new Vector3(mushroomPosition.x,mushroomPosition.y,food.transform.position.z);
//		}
		
		fountain.animator.SetTrigger("FadeIn");
	}
	//Update

	protected override void Update()
	{
		base.Update();
		OrderToEatTracker();
		OrderToShowerTracker();
//		float inp = Input.GetAxis("Horizontal");
//		Debug.Log("INP:"+inp);
//		if(fountain.animator.GetCurrentAnimatorStateInfo(0).nameHash == CSFountain.FadeOutState)
//		{
//			Debug.Log("FOUT");
//		}
//		if(Input.GetMouseButtonDown(0))
//		{
//			Vector2 mousePos = Input.mousePosition;
//			Debug.Log("Screen:"+Screen.width+"|"+Screen.height);
//			Debug.Log("Mouse:"+mousePos);
//			Vector2 screenPos = mainCamera.ScreenToWorldPoint(mousePos);
//			Debug.Log("Pos:"+screenPos);
//
//			GameObject monGo = GameObject.Find("Monster");
//			CSMonster mon = monGo.GetComponent(typeof(CSMonster)) as CSMonster;
//
//			Debug.Log(mon);
//
//			mon.Move(screenPos);
//		}
	}
}

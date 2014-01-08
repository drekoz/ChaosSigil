using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SigilBaseShieldColor{
	kSigilBaseSheildColorNone,
	kSigilBaseSheildColorRed,
	kSigilBaseSheildColorBlue,
	kSigilBaseSheildColorYellow
}

public enum SigilBaseRingState{
	kSigilBaseRingStateNone,
	kSigilBaseRingStateFirst,
	kSigilBaseRingStateSecond,
	kSigilBaseRingStateThird
}

public class CSSigilBase : CSGameObject {

	// Use this for initialization
//	public float health;
	
	private CSSigilRing bigRing;
	private CSSigilRing midRing;
	private CSSigilRing smallRing;
	public float stateHealth = 100.0f;
	public SigilBaseRingState sigilBaseRingState = SigilBaseRingState.kSigilBaseRingStateThird;
	public List<float> possibleRegenerationTime;
	public Color colorRedShield;
	public Color colorBlueShield;
	public Color colorYellowSheild;

	private bool hasBarrier = true;
	private SigilBaseShieldColor shieldColor;
	private Color colorShield;
	protected override void Awake()
	{

		Transform ringsT = transform.FindChild("Rings");

		bigRing = ringsT.FindChild("Big").gameObject.GetComponent<CSSigilRing>();
		midRing = ringsT.FindChild("Mid").gameObject.GetComponent<CSSigilRing>();
		smallRing = ringsT.FindChild("Small").gameObject.GetComponent<CSSigilRing>();

//		Debug.Log("Arr:"+possibleRegenerationTime.Count);
	}
	protected override void Start () {
		health = stateHealth;
		RandomGenerateShieldColor();

//		Debug.Log("Arr:"+possibleRegenerationTime.Count);
	}
	private void RandomGenerateShieldColor()
	{

		int randShield = UnityEngine.Random.Range(1,4);
//		Debug.Log("RandomShield:"+randShield);
		switch(randShield)
		{
		case 1:
			shieldColor = SigilBaseShieldColor.kSigilBaseSheildColorRed;
			colorShield = colorRedShield;
			break;
		case 2:
			shieldColor = SigilBaseShieldColor.kSigilBaseSheildColorBlue;
			colorShield = colorBlueShield;
			break;
		case 3:
			shieldColor = SigilBaseShieldColor.kSigilBaseSheildColorYellow;
			colorShield = colorYellowSheild;
			break;
		default:
			shieldColor = SigilBaseShieldColor.kSigilBaseSheildColorNone;
			colorShield = Color.white;
			break;
		}

		hasBarrier = true;
	}
//	private void addRing(CSSigilRing ringToAdd)
//	{
//		sigilRingList.Add(ringToAdd);
//	}


//	public override void Attacked(float damage = 0.0f)
//	{
////		Debug.Log("SigilBase TakeDamage:"+damage);
//		decreaseHealth(damage);
//	}
	public void ReCheckRingState()
	{

		int bigNameHash = bigRing.animator.GetCurrentAnimatorStateInfo(0).nameHash;
		int midNameHash = midRing.animator.GetCurrentAnimatorStateInfo(0).nameHash;
		int smallNameHash = smallRing.animator.GetCurrentAnimatorStateInfo(0).nameHash;

		if(bigNameHash != CSSigilRing.hideState && bigNameHash != CSSigilRing.breakState)
		{
			sigilBaseRingState = SigilBaseRingState.kSigilBaseRingStateThird;
		}
		else if(midNameHash != CSSigilRing.hideState && midNameHash != CSSigilRing.breakState)
		{
			sigilBaseRingState = SigilBaseRingState.kSigilBaseRingStateSecond;
		}
		else if(smallNameHash != CSSigilRing.hideState && smallNameHash != CSSigilRing.breakState)
		{
			sigilBaseRingState = SigilBaseRingState.kSigilBaseRingStateFirst;
		}
	}
	public override void Attacked(GameObject attacker,float damage = 0.0f)
	{
		ReCheckRingState();
		CSGameObject attackerCSGO = attacker.GetComponent<CSGameObject>();

		//Change hitter to Monster
		if(attackerCSGO.GetType() == typeof(CSHitterSim))
		{
			CSHitterSim hitter = ((CSHitterSim)attackerCSGO);
			SigilBaseShieldColor hitColor = hitter.colorToHit;

			if(hasBarrier)
			{
				if(hitColor == shieldColor)
				{
					BreakBarrier();
					StartRegenerateShield();
				}
				else
				{
					CreateBouncingBarrier();
				}
			}
			else
			{
				decreaseHealth(damage);

				if(health > 0.0f)
				{
					Debug.Log("NOT ZERO:"+health);
					if(sigilBaseRingState == SigilBaseRingState.kSigilBaseRingStateThird)
					{
						bigRing.Attacked(attacker,damage);
					}
					else if(sigilBaseRingState == SigilBaseRingState.kSigilBaseRingStateSecond)
					{
						midRing.Attacked(attacker,damage);
					}
					else if(sigilBaseRingState == SigilBaseRingState.kSigilBaseRingStateFirst)
					{
						smallRing.Attacked(attacker,damage);
					}
				}
			}
		}
		else
		{
//			Debug.Log("A:"+attackerCSGO.GetType());
		}
	}
	void decreaseHealth(float amountToDecrease)
	{
		float tempNewHealth = health - amountToDecrease;
		if(tempNewHealth <= 0.0f)
		{
			Debug.Log("HealthZero!!!");
			health = 0.0f;
			DestroyRing();
		}
		else
		{
			health = tempNewHealth;
		}
//		Debug.Log("New Health:"+health);
//		UpdateRing();
	}
//	private void UpdateRing()
//	{
////		CreateRing();
//		DestroyRing();
//
//	}
//	private void CreateRing()
//	{
//
//	}
	private void DestroyRing()
	{
		Debug.Log("Go Destroy");
		if(health <= 0.0f)
		{


			BreakRegenerateRing();
			if(sigilBaseRingState == SigilBaseRingState.kSigilBaseRingStateThird)
			{
				Debug.Log("D -> BIG");
				bigRing.Break();
				sigilBaseRingState = SigilBaseRingState.kSigilBaseRingStateSecond;
				health = stateHealth;
			}
			else if(sigilBaseRingState == SigilBaseRingState.kSigilBaseRingStateSecond)
			{
				Debug.Log("D -> MID");
				midRing.Break();
				sigilBaseRingState = SigilBaseRingState.kSigilBaseRingStateFirst;
				health = stateHealth;
			}
			else if(sigilBaseRingState == SigilBaseRingState.kSigilBaseRingStateFirst)
			{
				Debug.Log("D -> SMALL");
				smallRing.Break();
				sigilBaseRingState = SigilBaseRingState.kSigilBaseRingStateNone;
				health = -1.0f;
			}

			BreakRegenerateRing();
			RandomGenerateShieldColor();
			StartRegenerateLostRing();
		}
	}

	private void BreakBarrier()
	{
		CreateBreakingBarrier();
		hasBarrier = false;
	}
	private void CreateBouncingBarrier()
	{
//		Debug.Log("CREATEBOUNC"+colorShield);
		string prefabPath = "prefabs/STRTrainingBarrier/prefab_STRTrainingBarrier";
		Object prefab = Resources.Load(prefabPath, typeof(GameObject));
		GameObject clone = Instantiate(prefab) as GameObject;

		CSSTRTrainingBarrier trainingBarrier = clone.GetComponent<CSSTRTrainingBarrier>();
		trainingBarrier.setColor(colorShield);
		trainingBarrier.Bouncing();
//		clone.transform.position = new Vector3(pos.x,pos.y,clone.transform.position.z);
		Destroy(clone,0.5f);
	}
	private void CreateBreakingBarrier()
	{

//		Debug.Log("CREATEBREAK"+colorShield);
		string prefabPath = "prefabs/STRTrainingBarrier/prefab_STRTrainingBarrier";
		Object prefab = Resources.Load(prefabPath, typeof(GameObject));
		GameObject clone = Instantiate(prefab) as GameObject;
		
		CSSTRTrainingBarrier trainingBarrier = clone.GetComponent<CSSTRTrainingBarrier>();
		trainingBarrier.setColor(colorShield);
		trainingBarrier.Break();
		Debug.Log("LEN:"+trainingBarrier.animator.GetCurrentAnimationClipState(0).Length);
//		animator.state
		//		clone.transform.position = new Vector3(pos.x,pos.y,clone.transform.position.z);

		Destroy(clone,1.0f);
	}
	void StartRegenerateShield()
	{
		int randIndex = UnityEngine.Random.Range(0,possibleRegenerationTime.Count);
		float randTime = possibleRegenerationTime[randIndex];
		RegenerateShield(randTime);
	}
	void BreakRegenerateShield()
	{
		StopCoroutine("IRegenerateShield");
	}
	void RegenerateShield(float timeToRegen)
	{
		BreakRegenerateShield();
		
		StartCoroutine("IRegenerateShield",timeToRegen);
	}
	IEnumerator IRegenerateShield(float timeToRegen)
	{
		float timeLeft = timeToRegen;
		while(true)
		{
			yield return null;
			timeLeft -= Time.deltaTime;
			if(timeLeft <= 0.0f)
			{
				break;
			}
		}
		RandomGenerateShieldColor();
	}

	private void StartRegenerateLostRing()
	{
//		Debug.Log("STARTREGEN:"+possibleRegenerationTime.Count);
		int randIndex = UnityEngine.Random.Range(0,possibleRegenerationTime.Count);
//		Debug.Log("Rand:"+randIndex);
		float randTime = possibleRegenerationTime[randIndex];

		RegenerateRing(randTime);

	}
	void BreakRegenerateRing()
	{
		StopCoroutine("IRegenerateRing");
	}
	void RegenerateRing(float timeToRegen)
	{
		BreakRegenerateRing();

		StartCoroutine("IRegenerateRing",timeToRegen);
	}
	IEnumerator IRegenerateRing(float timeToRegen)
	{

		float timeLeft = timeToRegen;
		float debugTime = timeToRegen;
		while(true)
		{
			yield return null;
			timeLeft -= Time.deltaTime;
//			Debug.Log("REGEN-TIME:"+timeLeft);
			if(timeLeft <= 0.0f)
			{
				break;
			}
		}

		RandomGenerateShieldColor();
		if(sigilBaseRingState == SigilBaseRingState.kSigilBaseRingStateSecond)
		{
			health = stateHealth;
			sigilBaseRingState = SigilBaseRingState.kSigilBaseRingStateThird;
			bigRing.Regenerate();
		}
		else if(sigilBaseRingState == SigilBaseRingState.kSigilBaseRingStateFirst)
		{
			health = stateHealth;
			midRing.Regenerate();
			sigilBaseRingState = SigilBaseRingState.kSigilBaseRingStateSecond;
			StartRegenerateLostRing();
		}
		Debug.Log("Regen UP!"+debugTime);
	}
	// Update is called once per frame
	protected override void Update () {
		
	}
	void OnMouseDown()
	{
		Debug.Log("DOWN");
	}
}

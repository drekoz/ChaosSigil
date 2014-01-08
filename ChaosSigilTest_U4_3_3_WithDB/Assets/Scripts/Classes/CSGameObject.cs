using UnityEngine;
using System.Collections;
using System;

public enum CSGOState{
	None,
	Idle,
	FarmIdleWalk,
	FarmEat,
	FarmShower,
	Scratched,
	Happy};

public class CSGameObject : MonoBehaviour {

	public float health;
	public float speed = 1.0f;
	public bool autoFlipX = true;

	public Animator animator;
	private AnimatorStateInfo animatorCurrentStateInfo;
	private bool isMoving = false;
	private bool isFading = false;

	public bool useWalkAnimation = false;
	public string walkAnimStateName = "Walk";
	public bool useIdleAnimation = false;
	public string idleAnimStateName = "Idle";

	public static int animState_Base_Idle = Animator.StringToHash("Base Layer.Idle");
	public static int animState_Base_Stop = Animator.StringToHash("Base Layer.Stop");
	private static int animState_Base_Walk = Animator.StringToHash("Base Layer.Walk");

	public CSFarmSceneController farmSceneController;
	public CSGOState gameObjectState = CSGOState.None;
//	private int startLayer;
	public int animationState_Base_Walk
	{
		get
		{
			return Animator.StringToHash("Base Layer."+walkAnimStateName);
		}
	}
	protected virtual void Awake()
	{
//		base.Awake();
		animator = GetComponent<Animator>() as Animator;

	}
	protected virtual void Start()
	{
//		base.Start();
//		startLayer = gameObject.layer;

		//debug
//		transform.FindChild("Body").GetComponent<SpriteRenderer>().color = Color.black;
	}
	public virtual void ChangeState(CSGOState newState)
	{

	}

//	public void PutToHideLayer()
//	{
//		gameObject.layer = 9;
//	}
//	public void PutToStartLayer()
//	{
//		gameObject.layer = startLayer;
//	}
	public virtual void Attacked(float damage = 0.0f)
	{

	}
	public virtual void Attacked(GameObject attacker,float damage = 0.0f)
	{
		Attacked(damage);
	}
	public void FlipX(bool willFlipX)
	{
		if(willFlipX)
		{

			gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x)*-1.0f,gameObject.transform.localScale.y,gameObject.transform.localScale.z);
		}
		else
		{
			gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x)*1.0f,gameObject.transform.localScale.y,gameObject.transform.localScale.z);
		}
	}
	//Fade
	public void FadeToWithinTime(float valueToFadeTo, float time)
	{
		breakIFade();

		ArrayList arrList = new ArrayList();
		arrList.Add(valueToFadeTo);
		arrList.Add(time);
		
		StartCoroutine("IFade",(object)arrList);
	}
	IEnumerator IFade(object iFadeArrList)
	{

		ArrayList arrList = (ArrayList)iFadeArrList;

		float alphaToFadeTo = (float)arrList[0];
		float totalFadeTime = (float)arrList[1];

		SpriteRenderer spr = GetComponent<SpriteRenderer>() as SpriteRenderer;
		float currentAlpha = spr.color.a;
		float deltaToFade = alphaToFadeTo - currentAlpha;
		float deltaLeftToFade = Mathf.Abs(deltaToFade);

		float fadeSpeed = deltaLeftToFade/totalFadeTime;

		float fadeDirection;
		if(alphaToFadeTo - currentAlpha < 0.0f)
		{fadeDirection = -1.0f;}
		else
		{fadeDirection = 1.0f;}

		float iFade = 0.0f;
//		Vector4 newColor = Vector4.zero;
//		Debug.Log("SPD:"+fadeSpeed);

		while ( deltaLeftToFade > 0.0f )
		{
//			Debug.Log("A:"+spr.color.a);
			iFade = fadeSpeed * Time.deltaTime;
			if(iFade <= deltaLeftToFade)
			{
				spr.color = new Color(spr.color.r,spr.color.g,spr.color.b,spr.color.a+(iFade * fadeDirection));
			}
			else
			{
				spr.color = new Color(spr.color.r,spr.color.g,spr.color.b,alphaToFadeTo);
				iFade = deltaLeftToFade;
			}
			deltaLeftToFade -= iFade;

//			Debug.Log(spr.color.a+"LEFT:"+deltaLeftToFade);

			yield return null;
		}

	}
	void breakIFade()
	{
		StopCoroutine("IFade");
		isFading = false;
	}
	//2D Move
//	public void MoveSequence(params Vector2[] targetPosition)
//	{
//
//	}
	public virtual void StopAllCoroutines()
	{
		Debug.Log("CSGameObject -> StopALlCOroutines");
		base.StopAllCoroutines();
		isMoving = false;
		isFading = false;
	}
	//MoveByVelo
//	public void MoveVC(Vector2 targetPosition, bool isLocalPosition = false)
//	{
////		rigidbody2D.velocity = new Vector2(speed,rigidbody2D.velocity.y);
////		Debug.Log("RIG:");
////
////		float toVel = 2.5f;
////		float maxVel = 15.0f;
////		float maxForce = 40.0f;
////		float gain = 5f;
////
////		Vector2 dist = targetPosition - new Vector2(transform.position.x,transform.position.y);
////		dist.y = 0; // ignore height differences
////		// calc a target vel proportional to distance (clamped to maxVel)
////		Vector2 tgtVel = Vector2.ClampMagnitude(toVel * dist, maxVel);
////		// calculate the velocity error
////		Vector2 error = tgtVel - rigidbody2D.velocity;
////		// calc a force proportional to the error (clamped to maxForce)
////		Vector2 force = Vector2.ClampMagnitude(gain * error, maxForce);
////		Debug.Log("Force"+force);
////		rigidbody2D.AddForce(force);
//		Debug.Log("xxx");
//		StartCoroutine("IMoveVC",targetPosition);
////		float mag = Vector3.Distance(targetPosition,transform.position);
////		Vector2 force = Vector3.Normalize(targetPosition - new Vector2(transform.position.x,transform.position.y));
////		rigidbody2D.AddForce(new Vector2(force.x*mag,force.y*mag));
//
//	}
//	IEnumerator IMoveVC(Vector2 targetPos)
//	{
//		while(new Vector3(targetPos.x,targetPos.y) != transform.position)
//		{
//			yield return null;
//
//			transform.position = new Vector3(Mathf.MoveTowards(transform.position.x,targetPos.x,speed*Time.deltaTime),transform.position.y,transform.position.z);
//		}
//	}
	//Move
	public void Move(Vector2 targetPosition, bool isLocalPosition = false)
	{

		BreakIMove();

		ArrayList arrList = new ArrayList();
		arrList.Add(targetPosition);
		arrList.Add(this.speed);
		arrList.Add(isLocalPosition);

		StartCoroutine("IMove",(object)arrList);
	}
	public void Move(Vector2 targetPosition, float speed, bool isLocalPosition = false)
	{
		BreakIMove();

		ArrayList arrList = new ArrayList();
		arrList.Add(targetPosition);
		arrList.Add(speed);
		arrList.Add(isLocalPosition);

		StartCoroutine("IMove",(object)arrList);
	}
	public void MoveWithinTime(Vector2 targetPosition, float time, bool isLocalPosition = false)
	{
		BreakIMove();
		
		ArrayList arrList = new ArrayList();
		arrList.Add(targetPosition);

		float distance = Vector2.Distance(targetPosition,new Vector2(transform.position.x,transform.position.y));
		float totalTime = distance*speed;
		float newSpeed = totalTime/time;
//		Debug.Log(speed+"|"+newSpeed);
		arrList.Add(newSpeed);
		arrList.Add(isLocalPosition);
		StartCoroutine("IMove",(object)arrList);
	}
	public void BreakIMove()
	{
		StopCoroutine("IMove");
		isMoving = false;
	}
	IEnumerator IMove(object iMoveArrList)
	{
		isMoving = true;

		ArrayList arrList = (ArrayList)iMoveArrList;
		Vector2 targetPosition = (Vector2)arrList[0];
		float speed = (float)arrList[1];
		bool isLocalPosition =(bool)arrList[2];

//		Vector3 newPosition;
		Vector2 currentPos;
		float posZ;
		if(isLocalPosition)
		{
			currentPos = transform.localPosition;
			posZ = transform.localPosition.z;
		}
		else
		{
			currentPos = transform.position;
			posZ = transform.position.z;
		}

		while(new Vector3(targetPosition.x,targetPosition.y) != new Vector3(currentPos.x,currentPos.y,posZ))
		{


//			if(animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName(walkAnimStateName)){
//				animator.SetTrigger(walkAnimStateName);
//			}

			//assign currentPos by isLocalPosition
			if(isLocalPosition)
			{
				currentPos = new Vector2(transform.localPosition.x,transform.localPosition.y);
				posZ = transform.localPosition.z;
			}else
			{
				currentPos = new Vector2(transform.position.x,transform.position.y);
				posZ = transform.position.z;
			}
			//FlipX
			if(autoFlipX)
			{
				if(targetPosition.x > currentPos.x)
				{
					FlipX(true);
				}
				else
				{
					FlipX(false);
				}
			}
			//move
//			Debug.Log(transform.position+"VS"+targetPosition);


			Vector3 newPos;

//			newPos.x = Mathf.MoveTowards(currentPos.x,targetPosition.x,speed*Time.deltaTime);
//			newPos.y = Mathf.MoveTowards(currentPos.y,targetPosition.y,speed*Time.deltaTime);
			newPos = Vector3.MoveTowards(currentPos,targetPosition,speed*Time.deltaTime);
			newPos.z = posZ;
//			Debug.Log("NewPos:"+newPos.x+"Y"+newPos.y);
			if(isLocalPosition)
			{
				transform.localPosition = newPos;
			}
			else
			{
				transform.position = newPos;
			}
//			Debug.Log("MOV");
			yield return null;
		}
//		Debug.Log("SELF:"+gameObject.GetType());
//		if(animator != null)
//		{
//			animator.SetTrigger("Stop");
//		}
		isMoving = false;
		FinishWalk();
	}
	protected virtual void FinishWalk()
	{

	}
	//accessor
	public bool IsMoving
	{
		get
		{
			return isMoving;

		}
	}
//	IEnumerator IMove(Vector2 targetPosition, float speed)

	public virtual void ResetStateFromNone()
	{

	}
	protected virtual void Update()
	{
		UpdateState();
		UpdateWalkAnimation();
		UpdateIdleAnimation();
		//if CSGameObject is moving then play WalkState (if "useWalkAnimation" is true)




//		if(Input.GetMouseButtonDown(0))
//		{
//			MoveVC(new Vector2(-2.0f,0.0f));
//		}
	}
	public virtual void UpdateIdleAnimation()
	{
		if(useIdleAnimation)
		{
			if(animator != null){
				animatorCurrentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
				if(animatorCurrentStateInfo.nameHash == animState_Base_Stop)
				{
					animator.Play(animState_Base_Idle);
				}
			}
		}
	}
	public virtual void UpdateWalkAnimation()
	{
		if(isMoving)
		{
			if(useWalkAnimation)
			{
				if(animator != null){
					animatorCurrentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
					if(!animatorCurrentStateInfo.IsName(walkAnimStateName))
					{
						animator.Play(animState_Base_Walk);
					}
				}
			}
		}
		else
		{
			if(useWalkAnimation)
			{
				if(animator != null)
				{
					animatorCurrentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
					if(animatorCurrentStateInfo.IsName(walkAnimStateName))
					{
						animator.Play(animState_Base_Stop);
					}
				}
			}
		}
	}
	public virtual void UpdateState()
	{
		ResetStateFromNone();
	}

}

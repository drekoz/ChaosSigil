using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SigilRingRotateDirection{
	kSigilRingRotateDirectionClockWise,
	SigilRingRotateDirectionAntiClockWise
}
public class CSSigilRing : CSGameObject {
	
	// Use this for initialization

	public List<float> possibleRegenerationTime = new List<float>();
	public SigilRingRotateDirection rotateDirection;
	private SpriteRenderer spriteRenderer;
	private float regenSpeed = 1.0f*1.0f;
//	private Animator animator;
//	private bool isRotating;
	public static int regenUpState = Animator.StringToHash("Base Layer.RegenUp");
	public static int attackedState = Animator.StringToHash("Base Layer.Attacked");
	public static int stopState = Animator.StringToHash("Base Layer.Stop");
	public static int breakState = Animator.StringToHash("Base Layer.Break");
	public static int hideState = Animator.StringToHash("Base Layer.Hide");

	private float breakSpeed = 2.0f;
	private bool isBreaking = false;

	protected override void Start () {
		base.Start();

//		Debug.Log("THIS"+transform.FindChild("Body").gameObject.GetComponent<Animator>());
//		GameObject ringBody = (GameObject)(transform.FindChild("Body").gameObject);
//		GameObject ob = transform.FindChild("Body");
		spriteRenderer = transform.FindChild("Body").gameObject.GetComponent<SpriteRenderer>();

		RotateRing();
		animator = GetComponent<Animator>();
//		animator.SetTrigger("RegenUp");
//		animator.SetTrigger("Attacked");
//		animator.enabled = false;
//		spriteRenderer.color = Color.black;
//		spriteRenderer.enabled = false;
//		Debug.Log(animator.runtimeAnimatorController.name);
	
//		Debug.Log(animator.runtimeAnimatorController.);
	}
	
	// Update is called once per frame
	protected override void Update () {
//		Debug.Log("SP1:"+spriteRenderer.color);
//		spriteRenderer.color = Color.black;
//		Debug.Log("SP2:"+spriteRenderer.color);
//		Debug.Log("SP:"+animator.speed);
		if(animator.GetCurrentAnimatorStateInfo(0).nameHash == breakState)
		{
			animator.speed = breakSpeed;
//			Debug.Log("Break State");
		}
		else
		{
			animator.speed = 1.0f;
		}
		if(animator.GetCurrentAnimatorStateInfo(0).nameHash == regenUpState)
		{
//			Debug.Log("regenUp");
			regenSpeed = 1.0f;
		}
		else if(animator.GetCurrentAnimatorStateInfo(0).nameHash != regenUpState && animator.GetCurrentAnimatorStateInfo(0).nameHash != attackedState)
		{
//			spriteRenderer.color = new Color(1.0f,1.0f,1.0f);
		}
	}

	void RotateRing()
	{
		StopCoroutine("IRotateRing");
		StartCoroutine("IRotateRing");
	}
	IEnumerator IRotateRing()
	{
		float rotateDir;
		while(true)
		{
			if(rotateDirection == SigilRingRotateDirection.kSigilRingRotateDirectionClockWise)
			{
				rotateDir = 1.0f;
			}
			else
			{
				rotateDir = -1.0f;
			}

			transform.RotateAround(transform.position,new Vector3(0.0f,0.0f,rotateDir),speed*Time.deltaTime);

			yield return null;
		}
	}
	public void Break()
	{

		if(isBreaking)
		{
			return;
		}
		Debug.Log("!BREAK!");
		animator.SetTrigger("Break");
		StartCoroutine("IBreak");
//		DisableRenderer();
	}
	IEnumerator IBreak()
	{
		isBreaking = true;
		float breakWait = 1.0f/breakSpeed;
		Debug.Log("BREAKSP:"+breakWait);
		yield return new WaitForSeconds(breakWait);

		isBreaking = false;
	}
	public void Regenerate()
	{
//		Debug.Log("RING-REGENUP");
//		Debug.Log(gameObject+"IS TRUE");
//		gameObject.SetActive(true);
		animator.SetTrigger("RegenUp");

//		spriteRenderer.color = new Color(0.0f,0.0f,0.0f,0.0f);
//		spriteRenderer.enabled = true;

	}
	public override void StopAllCoroutines()
	{
		base.StopAllCoroutines();
		isBreaking = false;
	}
	public override void Attacked(GameObject attacker,float damage = 0.0f)
	{
//		if(isBreaking)
//		{
//			return;
//		}
		Debug.Log("!ATTACKED!");
		animator.SetTrigger("Attacked");
	}
	public void ForceDisable()
	{
//		spriteRenderer.enabled = false;
	}
}

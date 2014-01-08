using UnityEngine;
using System.Collections;

public class CSSTRTrainingBarrier : CSGameObject {

	// Use this for initialization
	public SpriteRenderer spriteRenderer;

	protected override void Awake()
	{
		base.Awake();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	public void setColor(Color newColor)
	{
		spriteRenderer.color = newColor;
	}
	public void Break()
	{
		animator.SetTrigger("Break");

	}
	public void Bouncing()
	{
		animator.SetTrigger("Bounce");
	}
	protected void Update()
	{

	}
}

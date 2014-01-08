using UnityEngine;
using System.Collections;

public class CSFood : CSGameObject {

	public int totalEatCount = 3;
	public int currentEatCount = 0;
	public string foodName;
//	public static int animStopState = Animator.StringToHash("Base Layer.Stop");
	public static int animEaten1State = Animator.StringToHash("Base Layer.Eaten1");
	public static int animEaten2State = Animator.StringToHash("Base Layer.Eaten2");

	protected override void Start()
	{
		base.Start();
		animator.Play(Animator.StringToHash("Base Layer.Pop"));
	}
	public void Eaten()
	{
		increaseCurrentEatCount(1);
		UpdateFood();
	}
	void increaseCurrentEatCount(int numToDecrease)
	{
		int tempEatCount = currentEatCount + numToDecrease;
		if(tempEatCount >= totalEatCount)
		{
			currentEatCount = totalEatCount;
		}
		else
		{
			currentEatCount = tempEatCount;
		}
	}
	void ShrinkFood()
	{
		animator.Play(Animator.StringToHash("Base Layer.Eaten"+currentEatCount));
	}
	public void DestroyFood()
	{
//		Debug.Log("DESTROY FOOD");
		Destroy(gameObject);
	}
	void UpdateFood()
	{
		ShrinkFood();
	}
	protected override void Update()
	{

	}

}

using UnityEngine;
using System.Collections;

public class CSFountain : CSGameObject {

	public static int animFadeInState = Animator.StringToHash("Base Layer.FadeIn");
	public static int animFadeOutState = Animator.StringToHash("Base Layer.FadeOut");
	public static int HideState = Animator.StringToHash("Base Layer.Hide");
	void OnMouseDown()
	{
//		Debug.Log("MDON");
		if(animator.GetCurrentAnimatorStateInfo(0).nameHash == animFadeInState)
		{
			DestroyFountain();
		}
	}
	void DestroyFountain()
	{
		animator.SetTrigger("FadeOut");
//		Debug.Log("STA:"+animator.GetCurrentAnimatorStateInfo(0).nameHash+"///"+FadeOutState+"////"+HideState+"FADEIN"+FadeInState);
		Destroy(gameObject,0.5f);
	}
}

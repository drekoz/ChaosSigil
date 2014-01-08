using UnityEngine;
using System.Collections;

public class CSMainMenuSceneController : CSSceneController {

	GameObject goUIPopUp;

	public void YesPressed()
	{
		Debug.Log("CSMainMenuSceneController -> YES PRESSED");
		CSGameManager.Instance.changeScene("MonsterSelectionScene");
	}
	public void NoPressed()
	{
		Debug.Log("CSMainMenuSceneController -> NO PRESSED");
		if(goUIPopUp != null)
		{
			Destroy(goUIPopUp);
		}
	}

	public void NewGamePressed()
	{
		string prefabPath = "prefabs/prefab_UIPopUp";
		Object prefab = Resources.Load(prefabPath, typeof(GameObject));
		goUIPopUp = Instantiate(prefab) as GameObject;

//		clone.transform.position = transform.position;

		CSUIPopUp uiPopUp = goUIPopUp.GetComponent<CSUIPopUp>();

		uiPopUp.sendMessageTarget = gameObject;
		uiPopUp.sendMessageYesPressedMethodName = "YesPressed";
		uiPopUp.sendMessageNoPressedMethodName = "NoPressed";
	}
	public void ContinuePressed()
	{
		Debug.Log("CSMainMenuSceneController -> NO PRESSED");
	}
}

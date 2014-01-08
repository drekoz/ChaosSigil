using UnityEngine;
using System.Collections;

public class CSCreatePlayerProfileSceneController : CSSceneController {

	// Use this for initialization
	TouchScreenKeyboard keyboard;
	public tk2dTextMesh playerName;

	void Start () {
//		keyboard = TouchScreenKeyboard.Open("EDITx", TouchScreenKeyboardType.Default, false, false, false);
//
//		if(keyboard != null)
//		{
//			Debug.Log("has k:"+keyboard.text+"area:"+TouchScreenKeyboard.area);
//		}
//		else
//		{
//			Debug.Log("no k");
//		}
	}

	public void OnNextPressed()
	{
		Debug.Log("CreatePlayerProfileSceneController -> OnNextPressed");
		SavePlayer();
		GoToFarmScene();
	}
	void SavePlayer()
	{
		CSGameManager.Instance.LDeleteAllSavedPlayer();
		CSGameManager.Instance.SavePlayer(1,"AlphaPlayer");
	}
	void GoToFarmScene()
	{
		CSGameManager.Instance.changeScene("FarmScene");
	}
	// Update is called once per frame
	void Update () {
	
	}
}

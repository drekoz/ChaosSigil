using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CSMonsterSelectionSceneController : CSSceneController {


	private TouchScreenKeyboard keyboard;

	public tk2dTextMesh MonsterSTR;
	public tk2dTextMesh MonsterAGI;
	public tk2dTextMesh MonsterINT;
	public tk2dTextMesh MonsterDescription;
	public tk2dTextMesh MonsterName;


	private string tragaDesc;
	private int tragaMonsterID;
	private int tragaSTR;
	private int tragaAGI;
	private int tragaINT;

	private string argrumDesc;
	private int argrumMonsterID;
	private int argrumSTR;
	private int argrumAGI;
	private int argrumINT;

	private string lexiaDesc;
	private int lexiaMonsterID;
	private int lexiaSTR;
	private int lexiaAGI;
	private int lexiaINT;

	private int selectedMonsterTag;

	protected override void Start()
	{
		base.Start();

		Dictionary<string,object> resultDict;
		dbAccess dba;

		//Argrum
		dba = new dbAccess ("LocalDatabase");
		resultDict = dba.LSelectMonster("Argrum");
		
//		argrum = new CSArgrum();
		argrumMonsterID = (int)resultDict["monster_ID"];
		argrumDesc = resultDict["monster_desc"].ToString();
		argrumSTR = (int)resultDict["monster_baseSTR"];
		argrumAGI = (int)resultDict["monster_baseAGI"];
		argrumINT = (int)resultDict["monster_baseINT"];

		//Traga
		dba = new dbAccess ("LocalDatabase");
		resultDict = dba.LSelectMonster("Traga");

		tragaMonsterID = (int)resultDict["monster_ID"];
		tragaDesc = resultDict["monster_desc"].ToString();
		tragaSTR = (int)resultDict["monster_baseSTR"];
		tragaAGI = (int)resultDict["monster_baseAGI"];
		tragaINT = (int)resultDict["monster_baseINT"];

		//Lexia
		dba = new dbAccess ("LocalDatabase");
		resultDict = dba.LSelectMonster("Lexia");
		
		lexiaMonsterID = (int)resultDict["monster_ID"];
		lexiaDesc = resultDict["monster_desc"].ToString();
		lexiaSTR = (int)resultDict["monster_baseSTR"];
		lexiaAGI = (int)resultDict["monster_baseAGI"];
		lexiaINT = (int)resultDict["monster_baseINT"];

//		Debug.Log("xxx:"+traga.MonsterDesc);
	}

	void OnNextPressed(){
		Debug.Log("NEXTPRESSED");





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

//		TouchScreenKeyboard tk = TouchScreenKeyboard.Open("",TouchScreenKeyboardType.Default,false,false,true,true);
//		tk.active = true;

//		if(tk.active)
//		{
//			Debug.Log("ac");
//		}else
//		{
//			Debug.Log("AC NO");
//		}
		UpdateCurrentSelectedMonster();
		SaveSelectedMonster();
		GoToCreatePlayerProfileScene();
//		GoToFarmScene();
	}
	void SaveSelectedMonster()
	{
		int selectedMonsterID;
		if(selectedMonsterTag == 1)//argrum
		{
			//save Argrum
			selectedMonsterID = argrumMonsterID;
		}
		else if(selectedMonsterTag == 2)//lexia
		{
			//save Lexia
			selectedMonsterID = lexiaMonsterID;
		}
		else//Traga(3)
		{
			//save Traga
			selectedMonsterID = tragaMonsterID;
		}
//		CSGameManager.Instance.DeleteAllSavedMonster();
		CSGameManager.Instance.DeleteAllSavedMonster();
		CSGameManager.Instance.SaveMonster(selectedMonsterID,MonsterName.text,7,8,9);
	}
	void UpdateCurrentSelectedMonster()
	{
		GameObject uiCyclingMenuGO = GameObject.Find("UICyclingMenu");
		CSUICyclingMenu uiCyclingMenuCSGO = uiCyclingMenuGO.GetComponent<CSUICyclingMenu>();
		CSUICyclingMenuItem midItem = uiCyclingMenuCSGO.GetMidItem();
		selectedMonsterTag = uiCyclingMenuCSGO.GetMidItem().itemTag;
//		Debug.Log("SEL:"+midItem+"tag"+itemTag);

		if(selectedMonsterTag == 1) //agrum
		{
			MonsterName.text = "Argrum";
			MonsterDescription.text = argrumDesc;
			MonsterSTR.text = argrumSTR.ToString();
			MonsterAGI.text = argrumAGI.ToString();
			MonsterINT.text = argrumINT.ToString();
		}
		else if(selectedMonsterTag == 2)//Lexia
		{
			MonsterName.text = "Lexia";
			MonsterDescription.text = lexiaDesc;
			MonsterSTR.text = lexiaSTR.ToString();
			MonsterAGI.text = lexiaAGI.ToString();
			MonsterINT.text = lexiaINT.ToString();
		}
		else if(selectedMonsterTag == 3)//Traga
		{
			MonsterName.text = "Traga";
			MonsterDescription.text = tragaDesc;
			MonsterSTR.text = tragaSTR.ToString();
			MonsterAGI.text = tragaAGI.ToString();
			MonsterINT.text = tragaINT.ToString();
		}
	}
	void GoToCreatePlayerProfileScene()
	{
		CSGameManager.Instance.changeScene("CreatePlayerProfileScene");
	}
	void GoToFarmScene()
	{
		CSGameManager.Instance.changeScene("FarmScene");
	}

	protected override void Update()
	{
		UpdateCurrentSelectedMonster();
	}
}

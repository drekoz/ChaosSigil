using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class CSGameManager : CSSingleton<CSGameManager> {

	protected CSGameManager(){}

	private string _previousScene = "";
	private string _currentScene = "";

	DateTime serverDateTimeRef;//yyyy/mm/dd
	DateTime deviceDateTimeRef;
	bool connetTimeServerSuccess = false;
	CSMonster monster;

	public void changeScene(string newScene)
	{
		Debug.Log("CSGameManager -> changeScene("+newScene+")");
		PreviousScene = CurrentScene;
		Application.LoadLevel(newScene);
		CurrentScene = PreviousScene;
	}
	//Monster

	public void SaveMonster(CSMonster monsterToSave)
	{
		SaveMonster(-99,"xxx",-99,-99,-99);

	}
	public void SaveMonster(int sMonsterID, string sMonsterName, int sTrainedSTR, int sTrainedAGI, int sTrainedINT)
	{
		dbAccess dba = new dbAccess ("SaveGame");
		int saveMonsterRecordCount = dba.CountMonsterRecord();
		//check if monster table already has monster or not(should hav 1 or 0)
		if(saveMonsterRecordCount == 0)
		{
			//no saved monster, we insert this one
			dba.InsertMonster(sMonsterID,sMonsterName,sTrainedSTR,sTrainedAGI,sTrainedINT);
		}
		else
		{
			dba.UpdateMonster(sMonsterID,sMonsterName,sTrainedSTR,sTrainedAGI,sTrainedINT);
		}

	}
	public void DeleteAllSavedMonster()
	{
		dbAccess dba = new dbAccess ("SaveGame");
		dba.ClearMonsterTable();
	}

	public CSMonster LoadMonsterGameObject()
	{
		dbAccess sDBA = new dbAccess("SaveGame");
		Dictionary<string,object> sSelectedMonster = sDBA.SSelectMonster();

		int sMonsterID = (int)sSelectedMonster["monster_ID"];
		string sMonsterName = sSelectedMonster["monster_name"].ToString();
		int sTrainedSTR = (int)sSelectedMonster["monster_trainedSTR"];
		int sTrainedAGI = (int)sSelectedMonster["monster_trainedAGI"];
		int sTrainedINT = (int)sSelectedMonster["monster_trainedINT"];


		dbAccess lDBA = new dbAccess ("LocalDatabase");
		Dictionary<string,object> lSelectedMonster = lDBA.LSelectMonster(sMonsterID);
		string lMonsterName = lSelectedMonster["monster_name"].ToString();
		int lBaseSTR = (int)lSelectedMonster["monster_baseSTR"];
		int lBaseAGI = (int)lSelectedMonster["monster_baseAGI"];
		int lBaseINT = (int)lSelectedMonster["monster_baseINT"];

		string prefabPath = "prefabs/prefab_"+lMonsterName;
		Debug.Log("PREFAB"+prefabPath);
		UnityEngine.Object prefab = Resources.Load(prefabPath, typeof(GameObject));
		GameObject clone = Instantiate(prefab) as GameObject;

		CSMonster csMonster = clone.GetComponent<CSMonster>();
		csMonster.name = sMonsterName;
		csMonster.BaseSTR = lBaseSTR;
		csMonster.BaseAGI = lBaseAGI;
		csMonster.BaseINT = lBaseINT;
		csMonster.TrainedSTR = sTrainedSTR;
		csMonster.TrainedAGI = sTrainedAGI;
		csMonster.TrainedINT = sTrainedINT;

//		clone.transform.position = Vector3.zero;
		//select monster 
		//get take its id to query for its name
		//use its name connects string to call prefab

		return csMonster;
	}

	//Player
	public void SavePlayer(int sPlayerID,string sPlayerName)
	{
		dbAccess dba = new dbAccess ("SaveGame");
		int savePlayerRecordCount = dba.CountPlayerRecord();

		if(savePlayerRecordCount == 0)
		{
			dba.InsertPlayer(sPlayerID,sPlayerName);
		}
		else
		{
			dba.UpdatePlayer(sPlayerID,sPlayerName);
		}
	}
	public void LDeleteAllSavedPlayer()
	{
		dbAccess dba = new dbAccess ("SaveGame");
		dba.ClearPlayerTable();
	}

	//Time
	public DateTime ServerDateTimeRef
	{
		get
		{
			return serverDateTimeRef;
		}
	}
	//Accessors
	public string PreviousScene
	{
		get
		{
			return _previousScene;
		}
		set
		{
			_previousScene = value;
		}
	}
	public string CurrentScene
	{
		get
		{
			return _currentScene;
		}
		set
		{
			_currentScene = value;
		}
	}


//#pragma mark -
//#pragma mark DateTime Methods
	
	public void GetCurrentServerDateTime()
	{
		string url = "http://wiki.greansoft.com/buddy.php";
		//		DateTime = DateTime.UtcNow;	
		connetTimeServerSuccess = false;
		WWW www = new WWW(url);
		StartCoroutine("IWaitForRequest",(object)www);
	}
	public DateTime GetSimServerDateTime()
	{
		TimeSpan ts = DateTime.Now - deviceDateTimeRef;
		return serverDateTimeRef.Date + ts;
	}
	IEnumerator IWaitForRequest(object wwwToWait)
	{
		WWW www = (WWW)wwwToWait;
		
		string rawContent = "";
		string betweenBodyContent = "";
		string[] dateTimeStrings;//dd/mm/yyyy/hh/mm/ss
//		if(www.error == null)
//		{
//			Debug.Log("ERR NULL");
//		}
//		else
//		{
//			Debug.Log("ERR NOT NULL");
//		}

//		Debug.Log("WWW"+www.error);
		yield return www;
		if(www.error == null)
		{
			connetTimeServerSuccess = true;
			Debug.Log("WWW OK!"+rawContent);
			rawContent = www.text;
			betweenBodyContent = Between(www.text,"<body>","</body>");
			dateTimeStrings = betweenBodyContent.Split('/');
			
			int year = Convert.ToInt32(dateTimeStrings[2]);
			int month = Convert.ToInt32(dateTimeStrings[1]);
			int day = Convert.ToInt32(dateTimeStrings[0]);
			
			int hour = Convert.ToInt32(dateTimeStrings[3]);
			int minute = Convert.ToInt32(dateTimeStrings[4]);
			int second = Convert.ToInt32(dateTimeStrings[5]);
			
			serverDateTimeRef = new DateTime(year,month,day);
			TimeSpan ts = new TimeSpan(hour,minute,second);
			//			Debug.Log("TICKS:"+ts.TotalSeconds);
			//serverTime
			serverDateTimeRef = serverDateTimeRef.Date+ts;
			//DeviceTime
			deviceDateTimeRef = DateTime.Now;
			
//			Debug.Log("SDT:"+serverDateTimeRef.ToString());
//			Debug.Log("STRCount:"+dateTimeStrings.Length);
			int j = 0;
//			for(int i = 0 ; i < dateTimeStrings.Length ; i++)
//			{
//				j++;
//				Debug.Log(dateTimeStrings.Length+"->"+dateTimeStrings[i]+"(((*"+j);
//			}
		}
		else
		{
			Debug.Log("WWW ERROR:"+www.error);
		}
		
	}
	
	public string Between(string src, string findfrom, string findto)
	{
		int start = src.IndexOf(findfrom);
		int to = src.IndexOf(findto, start + findfrom.Length);
		if (start < 0 || to < 0) return "";
		string s = src.Substring(
			start + findfrom.Length, 
			to - start - findfrom.Length);
		return s;
	}

}

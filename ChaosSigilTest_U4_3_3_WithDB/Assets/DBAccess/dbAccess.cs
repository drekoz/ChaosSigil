using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using Mono.Data.SqliteClient;
using Mono.Data;
using System.Collections.Generic;

public class dbAccess {


	private string dbFilename;

	public dbAccess(string _dbFilename)
	{
		dbFilename = _dbFilename;
	}

	public void selectUser(){

		SqliteDatabase sqlDB = new SqliteDatabase(dbFilename);
		string query = "Select * From user";
		DataTable dt = sqlDB.ExecuteQuery(query);

		string name = dt.Rows [0] ["name"].ToString ();
		Debug.Log(name);
	}
	//LPlayer
	public int CountPlayerRecord()
	{
		SqliteDatabase sqlDB = new SqliteDatabase(dbFilename);
		string query = "SELECT * FROM player";
		DataTable dt = sqlDB.ExecuteQuery(query);
		return dt.Rows.Count;
	}
	public void InsertPlayer(int sPlayerID,string sPlayerName)
	{
		SqliteDatabase sqlDB = new SqliteDatabase(dbFilename);
		string insertSQLString = 
			"Insert INTO player " +
				"(player_ID,player_Name) " +
				"VALUES ("+sPlayerID+", '"+sPlayerName+"')";
//		Debug.Log(insertSQLString);
		sqlDB.ExecuteNonQuery(insertSQLString);
	}
	public void UpdatePlayer(int sPlayerID,string sPlayerName)
	{
		SqliteDatabase sqlDB = new SqliteDatabase(dbFilename);
		string updateSQLString = "Update Player SET " +
				"player_ID = "+sPlayerID+","+
				"player_name = "+"'"+sPlayerName+"'";
//		Debug.Log(updateSQLString);
		sqlDB.ExecuteNonQuery(updateSQLString);
	}
	public void ClearPlayerTable()
	{
		SqliteDatabase sqlDB = new SqliteDatabase(dbFilename);
		string deleteSQLString = 
			"DELETE FROM player";
//		Debug.Log(deleteSQLString);
		sqlDB.ExecuteNonQuery(deleteSQLString);
	}
	//LMonster
	public int CountMonsterRecord()
	{
		SqliteDatabase sqlDB = new SqliteDatabase(dbFilename);
		string query = "SELECT * FROM monster";
		DataTable dt = sqlDB.ExecuteQuery(query);
		return dt.Rows.Count;
	}
	public void InsertMonster(int sMonsterID, string sMonsterName, int sTrainedSTR, int sTrainedAGI, int sTrainedINT)
	{
		SqliteDatabase sqlDB = new SqliteDatabase(dbFilename);
		string insertSQLString = 
			"Insert INTO monster " +
			"(monster_ID,monster_name,monster_trainedSTR,monster_trainedAGI,monster_trainedINT) " +
				"VALUES ("+sMonsterID+",'"+sMonsterName+"',"+sTrainedSTR+","+sTrainedAGI+","+sTrainedINT+")";
		sqlDB.ExecuteNonQuery(insertSQLString);
	}
	public void ClearMonsterTable()
	{
		SqliteDatabase sqlDB = new SqliteDatabase(dbFilename);
		string deleteSQLString = 
			"DELETE FROM monster";
		sqlDB.ExecuteNonQuery(deleteSQLString);
	}
	public void UpdateMonster(int monsterID, string monsterName, int trainedSTR, int trainedAGI, int trainedINT)
	{
		SqliteDatabase sqlDB = new SqliteDatabase(dbFilename);
		string updateSQLString = "Update Monster SET " +
								"monster_ID = "+monsterID+","+
								"monster_name = "+"'"+monsterName+"'"+","+
								"monster_trainedSTR = "+trainedSTR+","+
								"monster_trainedAGI = "+trainedAGI+","+
								"monster_trainedINT = "+trainedINT;
		sqlDB.ExecuteNonQuery(updateSQLString);
	}
	public Dictionary<string,object> SSelectMonster()
	{
		SqliteDatabase sqlDB = new SqliteDatabase(dbFilename);
		string query = "Select * From monster";
		DataTable dt = sqlDB.ExecuteQuery(query);
		
		int monID = (int)dt.Rows [0] ["monster_ID"];
		string name = dt.Rows [0] ["monster_name"].ToString ();
		int trainedSTR = (int)dt.Rows [0] ["monster_trainedSTR"];
		int trainedAGI = (int)dt.Rows [0] ["monster_trainedAGI"];
		int trainedINT = (int)dt.Rows [0] ["monster_trainedINT"];

		Dictionary<string,object> resultDict = new Dictionary<string,object>();
		
		resultDict.Add("monster_ID",monID);
		resultDict.Add("monster_name",name);
		resultDict.Add("monster_trainedSTR",trainedSTR);
		resultDict.Add("monster_trainedAGI",trainedAGI);
		resultDict.Add("monster_trainedINT",trainedINT);

		return resultDict;
	}

	public Dictionary<string,object> LSelectMonster()
	{
		SqliteDatabase sqlDB = new SqliteDatabase(dbFilename);
		string query = "Select * From monster";
		DataTable dt = sqlDB.ExecuteQuery(query);
		
		int monID = (int)dt.Rows [0] ["monster_ID"];
		string name = dt.Rows [0] ["monster_name"].ToString ();
		int baseSTR = (int)dt.Rows [0] ["monster_baseSTR"];
		int baseAGI = (int)dt.Rows [0] ["monster_baseAGI"];
		int baseINT = (int)dt.Rows [0] ["monster_baseINT"];
		string desc = dt.Rows [0] ["monster_desc"].ToString();
		
		Dictionary<string,object> resultDict = new Dictionary<string,object>();
		
		resultDict.Add("monster_ID",monID);
		resultDict.Add("monster_name",name);
		resultDict.Add("monster_baseSTR",baseSTR);
		resultDict.Add("monster_baseAGI",baseAGI);
		resultDict.Add("monster_baseINT",baseINT);
		resultDict.Add("monster_desc",desc);
		
		return resultDict;
	}
	public Dictionary<string,object> LSelectMonster(string nameToSelect)
	{
		//Dictionary<string,object>
		SqliteDatabase sqlDB = new SqliteDatabase(dbFilename);
		string query = "Select * From monster where monster_name = '"+nameToSelect+"'";
		DataTable dt = sqlDB.ExecuteQuery(query);

		int monID = (int)dt.Rows [0] ["monster_ID"];
		string name = dt.Rows [0] ["monster_name"].ToString ();
		int baseSTR = (int)dt.Rows [0] ["monster_baseSTR"];
		int baseAGI = (int)dt.Rows [0] ["monster_baseAGI"];
		int baseINT = (int)dt.Rows [0] ["monster_baseINT"];
		string desc = dt.Rows [0] ["monster_desc"].ToString();

		Dictionary<string,object> resultDict = new Dictionary<string,object>();

		resultDict.Add("monster_ID",monID);
		resultDict.Add("monster_name",name);
		resultDict.Add("monster_baseSTR",baseSTR);
		resultDict.Add("monster_baseAGI",baseAGI);
		resultDict.Add("monster_baseINT",baseINT);
		resultDict.Add("monster_desc",desc);

		return resultDict;
//		Debug.Log("RE"+name+"/"+baseSTR);
	}
	public Dictionary<string,object> LSelectMonster(int idToSelect)
	{
		//Dictionary<string,object>
		SqliteDatabase sqlDB = new SqliteDatabase(dbFilename);
		string query = "Select * From monster where monster_ID = "+idToSelect;
		DataTable dt = sqlDB.ExecuteQuery(query);
		
		int monID = (int)dt.Rows [0] ["monster_ID"];
		string name = dt.Rows [0] ["monster_name"].ToString ();
		int baseSTR = (int)dt.Rows [0] ["monster_baseSTR"];
		int baseAGI = (int)dt.Rows [0] ["monster_baseAGI"];
		int baseINT = (int)dt.Rows [0] ["monster_baseINT"];
		string desc = dt.Rows [0] ["monster_desc"].ToString();
		
		Dictionary<string,object> resultDict = new Dictionary<string,object>();
		
		resultDict.Add("monster_ID",monID);
		resultDict.Add("monster_name",name);
		resultDict.Add("monster_baseSTR",baseSTR);
		resultDict.Add("monster_baseAGI",baseAGI);
		resultDict.Add("monster_baseINT",baseINT);
		resultDict.Add("monster_desc",desc);
		
		return resultDict;
		//		Debug.Log("RE"+name+"/"+baseSTR);
	}

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

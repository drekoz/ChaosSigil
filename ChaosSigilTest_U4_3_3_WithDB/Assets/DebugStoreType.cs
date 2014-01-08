using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class DebugStoreType : MonoBehaviour {

	struct TestStruct
	{
		private int x;
		private System.Type type;

		public int X
		{
			get{return x;}
		}
		public System.Type Type
		{
			get{return type;}
		}
		public TestStruct(int x, System.Type type)
		{
			this.x = x;
			this.type = type;
		}
	}
	// Use this for initialization
	void Start () {
		System.Type myINT = typeof(int);
		Debug.Log("TYPE:"+myINT);

		float theFloat = 1.2345f;
		TestStruct ts = new TestStruct(1,typeof(int));
		Debug.Log("bCast:"+theFloat+" aCast"+Convert.ChangeType(theFloat,ts.Type));

		string lex = "DebugBlankClass";
		var x = Activator.CreateInstance(Type.GetType(lex));
		Debug.Log("x:"+x);
//		CSMonster lex = GameObject.Find("Lexia").GetComponent<CSMonster>();
//		Debug.Log("LEX1:"+lex+" type:"+((object)lex).GetType());

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

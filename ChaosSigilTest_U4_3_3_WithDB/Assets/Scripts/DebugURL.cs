using UnityEngine;
using System.Collections;
using System;
public class DebugURL : MonoBehaviour {
//
//	DateTime serverDateTimeRef;//yyyy/mm/dd
//	DateTime deviceDateTimeRef;
//
//
//	public void GetCurrentServerDateTime()
//	{
//		string url = "http://wiki.greansoft.com/buddy.php";
////		DateTime = DateTime.UtcNow;
//		WWW www = new WWW(url);
//		StartCoroutine("IWaitForRequest",(object)www);
//	}
//	IEnumerator IWaitForRequest(object wwwToWait)
//	{
//		WWW www = (WWW)wwwToWait;
//
//		string rawContent = "";
//		string betweenBodyContent = "";
//		string[] dateTimeStrings;//dd/mm/yyyy/hh/mm/ss
//
//		yield return www;
//		if(www.error == null)
//		{
//			Debug.Log("WWW OK!"+rawContent);
//			rawContent = www.text;
//			betweenBodyContent = Between(www.text,"<body>","</body>");
//			dateTimeStrings = betweenBodyContent.Split('/');
//
//			int year = Convert.ToInt32(dateTimeStrings[2]);
//			int month = Convert.ToInt32(dateTimeStrings[1]);
//			int day = Convert.ToInt32(dateTimeStrings[0]);
//
//			int hour = Convert.ToInt32(dateTimeStrings[3]);
//            int minute = Convert.ToInt32(dateTimeStrings[4]);
//            int second = Convert.ToInt32(dateTimeStrings[5]);
//
//			serverDateTimeRef = new DateTime(year,month,day);
//			TimeSpan ts = new TimeSpan(hour,minute,second);
////			Debug.Log("TICKS:"+ts.TotalSeconds);
//			//serverTime
//			serverDateTimeRef = serverDateTimeRef.Date+ts;
//			//DeviceTime
//			deviceDateTimeRef = DateTime.Now;
//
//			Debug.Log("SDT:"+serverDateTimeRef.ToString());
//
//			foreach(string temp in dateTimeStrings)
//			{
//				Debug.Log("->"+temp);
//			}
//		}
//		else
//		{
//			Debug.Log("WWW ERROR:"+www.error);
//		}
//
//	}
//
//	public string Between(string src, string findfrom, string findto)
//	{
//		int start = src.IndexOf(findfrom);
//		int to = src.IndexOf(findto, start + findfrom.Length);
//		if (start < 0 || to < 0) return "";
//		string s = src.Substring(
//			start + findfrom.Length, 
//			to - start - findfrom.Length);
//		return s;
//	}
//
//	// Use this for initialization
	void Start () {
//		GetCurrentServerDateTime();
		Invoke("Test",3.0f);

		
	}
	void Test()
	{
		Debug.Log("START:"+CSGameManager.Instance.ServerDateTimeRef);
		CSGameManager.Instance.Invoke("GetCurrentServerDateTime",2.0f);
	}
}

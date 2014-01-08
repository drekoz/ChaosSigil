using UnityEngine;
using System.Collections;
using System;

public class CSUIClock : MonoBehaviour {

	public tk2dTextMesh timerSprite;
	public float startTime = 0.0f;
	public float currentTime;
	public bool isCountDown = true;

	public GameObject sendMessageTarget = null;
	public string sendMessageFinishTimer = "";

	protected void DoSendMessage( string methodName, object parameter = null)
	{
		if (sendMessageTarget != null && methodName.Length > 0)
		{
			sendMessageTarget.SendMessage( methodName, parameter, SendMessageOptions.RequireReceiver );
		}
	}

	// Use this for initialization
	void Start () {
		currentTime = startTime;
		timerSprite.text = TwoDecimalDigitsTime(currentTime);
		StartTimer();
	}
	string TwoDecimalDigitsTime(float timeToConvert)
	{
		int paddingZeroesCount = 0;
		int firstDisplayDigits;
		int secondDisplayDigits;
		string result = "";

		float newRoundedTime = (Mathf.Round(timeToConvert*100.0f)/100.0f);
		firstDisplayDigits = (int)newRoundedTime;
		secondDisplayDigits = (int)((newRoundedTime*100.0f)%100);

		result = firstDisplayDigits+":"+secondDisplayDigits;
		if(secondDisplayDigits < 10)
		{
			result = result+"0";
		}
//		Debug.Log(newRoundedTime+"//"+result);
		return result;
	}
	// Update is called once per frame
	void Update () {

	}
	void StartTimer()
	{
		if(isCountDown)
		{
			StartCoroutine("ICountdown");
		}
		else
		{

		}
	}
	IEnumerator ICountdown()
	{
		while(currentTime >= 0.0f)
		{
			yield return null;
			currentTime -= Time.deltaTime;
			UpdateTimerSprite();
		}
		currentTime = 0.0f;
		UpdateTimerSprite();
		FinishTimer();
		StopCoroutine("ICountdown");
	}
	void UpdateTimerSprite()
	{
		timerSprite.text = TwoDecimalDigitsTime(currentTime);
	}
	void FinishTimer()
	{
//		Debug.Log("Finish!");
		DoSendMessage(sendMessageFinishTimer);
	}
}

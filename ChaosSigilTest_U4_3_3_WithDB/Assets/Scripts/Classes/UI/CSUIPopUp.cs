using UnityEngine;
using System.Collections;

public class CSUIPopUp : CSUI {

//	[SerializeField]
	public bool isYesNo;
	public GameObject sendMessageTarget;
	public string sendMessageNoPressedMethodName = "";
	public string sendMessageYesPressedMethodName = "";
	public string titleString;
	public string contentString;
	public GameObject yesButton;
	public GameObject noButton;



	private void DoSendMessage(string methodName){
		if (sendMessageTarget != null && methodName.Length > 0) {
			sendMessageTarget.SendMessage(methodName,SendMessageOptions.RequireReceiver);		
		}
		else
		{

			Debug.Log("SMS:"+sendMessageTarget);
		}
	}
//	protected override void Start()
//	{
//		if(yesButton == null)
//		{
//			transform.FindChild("PopUp_Yes");
//		}
//		if(noButton == null)
//		{
//			transform.FindChild("PopUp_No");
//		}
//	}
	public void ButtonPressed(object buttonTypeObj)
	{
		CSUIPopUpButton.ButtonType buttonType = (CSUIPopUpButton.ButtonType)buttonTypeObj;

		if(buttonType == CSUIPopUpButton.ButtonType.Yes)
		{
			YesPressed();
		}
		else
		{
			NoPressed();
		}
	}
	public void YesPressed()
	{
//		Debug.Log("CSUIPopUp -> YesPressed");
		DoSendMessage(sendMessageYesPressedMethodName);
	}
	public void NoPressed()
	{
//		Debug.Log("CSUIPopUp -> NoPressed");
		DoSendMessage(sendMessageNoPressedMethodName);
	}

}

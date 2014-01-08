using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CSUIPopUp))]
public class CSUIPopUpEditor : Editor {

	CSUIMethodBindingHelper methodBindingHelper = new CSUIMethodBindingHelper();
	public override void OnInspectorGUI()
	{
		GUI.changed = false;
		

		EditorGUIUtility.LookLikeControls(180);
		//		Debug.Log("target:"+target);
		CSUIPopUp btn = (CSUIPopUp)target;
		
		serializedObject.Update ();
		btn.isYesNo = EditorGUILayout.Toggle("is YesNo Type",btn.isYesNo);
		btn.titleString = EditorGUILayout.TextField("Title",btn.titleString);
		btn.contentString = EditorGUILayout.TextField("Content",btn.contentString);


//		btn.xxx = (GameObject)EditorGUILayout.ObjectField("XXX",(Object)btn.xxx,typeof(GameObject));
		btn.sendMessageTarget = methodBindingHelper.BeginMessageGUI (btn.sendMessageTarget);
		methodBindingHelper.MethodBinding ("Yes Pressed", typeof(object), btn.sendMessageTarget, ref btn.sendMessageYesPressedMethodName);
		methodBindingHelper.MethodBinding ("No Pressed", typeof(object), btn.sendMessageTarget, ref btn.sendMessageNoPressedMethodName);
		//Update
		if(!btn.isYesNo)
		{
			btn.transform.FindChild("PopUp_No").gameObject.SetActive(false);
		}else
		{
			btn.transform.FindChild("PopUp_No").gameObject.SetActive(true);
		}
		btn.transform.FindChild("PopUp_Title").gameObject.GetComponent<tk2dTextMesh>().text = btn.titleString;
		btn.transform.FindChild("PopUp_Content").gameObject.GetComponent<tk2dTextMesh>().text = btn.contentString;

		if(GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}
}

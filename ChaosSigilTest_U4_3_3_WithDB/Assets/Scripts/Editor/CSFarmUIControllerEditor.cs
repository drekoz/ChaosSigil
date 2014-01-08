using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
using UnityEditor;

[CustomEditor(typeof(CSFarmUIController))]
public class CSFarmUIControllerEditor : Editor {

	CSUIMethodBindingHelper methodBindingHelper = new CSUIMethodBindingHelper();
	public override void OnInspectorGUI()
	{
		EditorGUIUtility.LookLikeControls(180);
		//		Debug.Log("target:"+target);
		CSFarmUIController btn = (CSFarmUIController)target;
		
		
		
		
		serializedObject.Update ();
		
		btn.sendMessageTarget = methodBindingHelper.BeginMessageGUI (btn.sendMessageTarget);
		methodBindingHelper.MethodBinding ("Meat Pressed", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageMeatMethodName);
		methodBindingHelper.MethodBinding ("Mushroom Pressed", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageMushroomMethodName);
		methodBindingHelper.MethodBinding ("BasicBath Pressed", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageBasicBathMethodName);
//		methodBindingHelper.MethodBinding ("STRTrain Pressed", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageSTRTrainMethodName);
//		methodBindingHelper.MethodBinding ("AGITrain Pressed", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageAGITrainMethodName);
//		methodBindingHelper.MethodBinding ("INTTrain Pressed", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageINTTrainMethodName);

		methodBindingHelper.EndMessageGUI ();
		
//		GUILayout.Label("Sprites", EditorStyles.boldLabel);
//		EditorGUI.indentLevel++;
//		EditorGUI.indentLevel++;
//		
//		btn.onDownSprite = EditorGUILayout.ObjectField("OnDownSprite",btn.onDownSprite,typeof(Sprite),true,null) as Sprite;
//		btn.onUpSprite = EditorGUILayout.ObjectField("OnUpSprite",btn.onUpSprite,typeof(Sprite),true,null) as Sprite;
//		
//		EditorGUI.indentLevel--;
//		EditorGUI.indentLevel--;
		
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

//[CanEditMultipleObjects]
[CustomEditor(typeof(CSUIButton),false)]
public class CSUIButtonEditor : Editor {





	CSUIMethodBindingHelper methodBindingHelper = new CSUIMethodBindingHelper();
	public override void OnInspectorGUI()
	{
		GUI.changed = false;

		EditorGUIUtility.LookLikeControls(180);
//		Debug.Log("target:"+target);
		CSUIButton btn = (CSUIButton)target;

		serializedObject.Update ();

		btn.scaleUpWhenHold = EditorGUILayout.Toggle("ScaleUp When Hold",btn.scaleUpWhenHold);
		btn.scaleUpSpeed = EditorGUILayout.FloatField("ScaleUp Speed",btn.scaleUpSpeed);
		btn.maxScaleFactor = EditorGUILayout.Vector3Field("Max ScaleFactor",btn.maxScaleFactor);

		btn.sendMessageTarget = methodBindingHelper.BeginMessageGUI (btn.sendMessageTarget);
		methodBindingHelper.MethodBinding ("OnDown", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageOnDownMethodName);
		methodBindingHelper.MethodBinding ("OnUp", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageOnUpMethodName);
		methodBindingHelper.MethodBinding ("OnClick", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageOnClickMethodName);
		methodBindingHelper.MethodBinding ("OnRelease", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageOnReleaseMethodName);
		methodBindingHelper.EndMessageGUI ();

		GUILayout.Label("Sprites", EditorStyles.boldLabel);
		EditorGUI.indentLevel++;
		EditorGUI.indentLevel++;

		btn.onDownSprite = EditorGUILayout.ObjectField("OnDownSprite",btn.onDownSprite,typeof(Sprite),true,null) as Sprite;
		btn.onUpSprite = EditorGUILayout.ObjectField("OnUpSprite",btn.onUpSprite,typeof(Sprite),true,null) as Sprite;

		EditorGUI.indentLevel--;
		EditorGUI.indentLevel--;

		if (GUI.changed)
		{
			EditorUtility.SetDirty(btn);
		}
	}


}

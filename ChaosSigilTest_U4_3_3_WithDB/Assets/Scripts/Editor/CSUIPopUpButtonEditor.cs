using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CSUIPopUpButton))]
public class CSUIPopUpButtonEditor :Editor {

	void OnInspectorGUI()
	{

		EditorGUIUtility.LookLikeControls(180);
		CSUIPopUpButton btn = (CSUIPopUpButton)target;
		serializedObject.Update ();
		btn.buttonType = (CSUIPopUpButton.ButtonType)EditorGUILayout.EnumPopup("ButtonType",btn.buttonType);

	}
}

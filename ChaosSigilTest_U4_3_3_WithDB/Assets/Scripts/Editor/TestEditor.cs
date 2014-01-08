using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;


//[CustomEditor(typeof(CSUIButton))]
public class TestEditor : Editor {
	
	public override void OnInspectorGUI()
	{
		CSUIButton tar = new CSUIButton();
		GUILayout.Label("Send Message", EditorStyles.boldLabel);
		GameObject goo = EditorGUILayout.ObjectField("gox",tar,typeof(GameObject),true,null) as GameObject;
	}
}

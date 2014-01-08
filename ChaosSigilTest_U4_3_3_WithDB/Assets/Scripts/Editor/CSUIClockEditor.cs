using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CSUIClock))]
public class CSUIClockEditor : Editor {

	CSUIMethodBindingHelper methodBindingHelper = new CSUIMethodBindingHelper();
	public override void OnInspectorGUI()
	{
		EditorGUIUtility.LookLikeControls(180);
		CSUIClock btn = (CSUIClock)target;
		
		serializedObject.Update ();
		
		btn.sendMessageTarget = methodBindingHelper.BeginMessageGUI (btn.sendMessageTarget);
		methodBindingHelper.MethodBinding ("FinishTimer", typeof(object), btn.sendMessageTarget, ref btn.sendMessageFinishTimer);
		methodBindingHelper.EndMessageGUI ();
		
	}
}

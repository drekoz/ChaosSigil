using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CSINTTrainingBoard))]
public class CSINTTrainingBoardEditor : Editor {

	CSUIMethodBindingHelper methodBindingHelper = new CSUIMethodBindingHelper();
	public override void OnInspectorGUI()
	{
		EditorGUIUtility.LookLikeControls(180);
		CSINTTrainingBoard btn = (CSINTTrainingBoard)target;

		serializedObject.Update ();
		
		btn.sendMessageTarget = methodBindingHelper.BeginMessageGUI (btn.sendMessageTarget);
		methodBindingHelper.MethodBinding ("Score", typeof(object), btn.sendMessageTarget, ref btn.sendMessageScoreByRuneCount);
//		methodBindingHelper.MethodBinding ("Mushroom Pressed", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageMushroomMethodName);
//		methodBindingHelper.MethodBinding ("BasicBath Pressed", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageBasicBathMethodName);
		//		methodBindingHelper.MethodBinding ("STRTrain Pressed", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageSTRTrainMethodName);
		//		methodBindingHelper.MethodBinding ("AGITrain Pressed", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageAGITrainMethodName);
		//		methodBindingHelper.MethodBinding ("INTTrain Pressed", typeof(CSUIButton), btn.sendMessageTarget, ref btn.sendMessageINTTrainMethodName);
		
		methodBindingHelper.EndMessageGUI ();
		
	}
}

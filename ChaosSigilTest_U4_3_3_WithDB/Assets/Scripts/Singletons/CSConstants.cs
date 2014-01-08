using UnityEngine;
using System.Collections;




public class CSConstants : MonoBehaviour {

	public static Vector2 resetTouchPos = new Vector2(-1.0f,-1.0f);
//	public static Vector3 paddingBorderDim = new Vector3(0.9f,6.4f,0.2f);



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public static Resolution screenResolution
	{
		get
		{
			Debug.Log(Screen.currentResolution.width+"|"+Screen.currentResolution.height);
			Debug.Log(Screen.resolutions[0].width);
			Debug.Log(Screen.resolutions[0].height);
			return Screen.resolutions[0];
		}
	}
	public static Vector2 paddingBorder
	{
		get
		{
			if(screenResolution.height == 1136)
			{
				return Vector2.zero;
			}
			else
			{
				return new Vector2(0.9f,6.4f);
			}
		}
	}

}

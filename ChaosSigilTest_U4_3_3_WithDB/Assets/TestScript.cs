using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 pos = transform.position;
//		transform.position = new Vector3(pos.x+renderer.bounds.size.x,pos.y,pos.z);
		if(renderer != null)
		{
			Debug.Log("REN:"+renderer.bounds.size);
		}

//		Debug.Log("RES"+CSConstants.paddingBorderDim.y);
		transform.position = new Vector3(3.095f,transform.position.y,transform.position.z);

	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log("REN:"+renderer.bounds.size);
	}
}

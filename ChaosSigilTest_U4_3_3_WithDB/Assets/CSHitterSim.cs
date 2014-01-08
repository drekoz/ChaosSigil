using UnityEngine;
using System.Collections;



public class CSHitterSim : CSGameObject {

	public GameObject gameObjectToHit;
	public SigilBaseShieldColor colorToHit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Hit()
	{
		gameObjectToHit.GetComponent<CSGameObject>().Attacked(gameObject,10.0f);
	}

	void OnMouseDown()
	{
		Hit();
	}
}

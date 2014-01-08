using UnityEngine;
using System.Collections;

public class CSSceneController : MonoBehaviour {

	protected Camera mainCamera;
	// Use this for initialization
	protected virtual void Awake()
	{

	}
	protected virtual void Start () {
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>() as Camera;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}
}

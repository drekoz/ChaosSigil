using UnityEngine;
using System.Collections;

public class CSUIButton : CSUI {

	public GameObject sendMessageTarget = null;
//	public GameObject test = null;
	public string sendMessageOnDownMethodName = "";
	public string sendMessageOnUpMethodName = "";
	public string sendMessageOnClickMethodName = "";
	public string sendMessageOnReleaseMethodName = "";

	private Sprite defaultSprite;
	public Sprite onDownSprite;
	public Sprite onUpSprite;
	public GameObject debugGO;

	public Vector3 maxScaleFactor = new Vector3(1.1f,1.1f,1.1f);
	public bool scaleUpWhenHold = false;
	public float scaleUpSpeed = 2.0f;

	private Vector3 initialScale;
	private Vector3 maxScale;
	protected SpriteRenderer spriteRenderer;
	// Use this for initialization
	protected override void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		initialScale = transform.localScale;
		maxScale = new Vector3(transform.localScale.x*maxScaleFactor.x,transform.localScale.y*maxScaleFactor.y,transform.localScale.z*maxScaleFactor.z);

		if(spriteRenderer != null)
		{
			defaultSprite = spriteRenderer.sprite;
		}

	}
	
	// Update is called once per frame
	protected override void Update () {

	}
	protected virtual void OnMouseUpAsButton() {
		DoSendMessage (sendMessageOnClickMethodName);
	}

	private void DoSendMessage(string methodName){
		if (sendMessageTarget != null && methodName.Length > 0) {
			sendMessageTarget.SendMessage(methodName,SendMessageOptions.RequireReceiver);		
		}
	}

	void OnMouseDown()
	{
//		SpriteRenderer sr = GetComponent<SpriteRenderer>();
//		Debug.Log("SPR:"+spriteRenderer);
		if(spriteRenderer != null)
		{
			if(onDownSprite != null)
			{
				spriteRenderer.sprite = onDownSprite;
			}
		}

	}
	void OnMouseDrag()
	{
//		Debug.Log("DRAG");
		if(scaleUpWhenHold)
		{
			if(transform.localScale != maxScale)
			{
				transform.localScale = Vector3.MoveTowards(transform.localScale,maxScale,scaleUpSpeed*Time.deltaTime);
			}
		}
	}
	void OnMouseUp()
	{

		if(spriteRenderer != null)
		{
			if(onUpSprite != null)
			{
				spriteRenderer.sprite = onUpSprite;
			}
			else
			{
				spriteRenderer.sprite = defaultSprite;
			}
		}

		if(scaleUpWhenHold)
		{
			transform.localScale = initialScale;
		}
	}
}

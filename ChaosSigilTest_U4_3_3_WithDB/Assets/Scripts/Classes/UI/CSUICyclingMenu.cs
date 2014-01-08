using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CSEllipse
{
    public float a;
	public float b;



	public CSEllipse(float a, float b)
	{
		this.a = a;
		this.b = b;
	}
	[System.NonSerialized]
	public GameObject selectedItem = null;

}

public class CSUICyclingMenu : CSUI {

	public float angleGap = 10.0f;
	public float itemMaxScale = 1.0f;
	public float itemMinScale = 0.5f;
	public CSEllipse ellipse = new CSEllipse (5.0f,10.0f);
	public float moveSpeedFactor = 1.0f;
	
	
	private List<object> _menuItems = null;
	private float _m;
	private float _c;

	private Vector3 _center;
//	private CSUIController _UIController;
	private bool isSnapping;
	private Vector2 prevTouchPos;
	private Vector2 curTouchPos;

	private CSUICyclingMenuItem testMenuItem;

	void Awake()
	{
//		_UIController = GameObject.Find("UIController").GetComponent<CSUIController>();
//		
//		_UIController.OnTouchBegan += OnTouchDown;
//		_UIController.OnTouchMoved += OnTouchMoved;
//		_UIController.OnTouchEnded += OnTouchEnded;
		
		_menuItems = new List<object>();
		_center = transform.position;
		
		float deltaY = itemMinScale - itemMaxScale;
		float deltaX = 2*ellipse.b;
		_m = deltaY/deltaX;
		_c = itemMaxScale - (_m*(transform.position.y - ellipse.b));
		
//		Debug.Log("count:"+transform.childCount);
		for(int i = 0; i < transform.childCount ; i++)
		{
			object tempGameObject = transform.GetChild(i);
//			Debug.Log("Child-Type:"+tempGameObject.GetType().ToString());
			Debug.Log("Child:"+tempGameObject);
			addObject(tempGameObject);
//			Debug.Log("ChildX:"+_menuItems[i]);
		}
		rearrangeItems();
	}
	protected override void Start()
	{
		base.Start();
		testMenuItem = ((Transform)_menuItems[0]).gameObject.GetComponent<CSUICyclingMenuItem>();
		Debug.Log("TAG::"+testMenuItem.itemTag);
	}
	
	public void addObject(object objectToAdd)
	{
		_menuItems.Add(objectToAdd);
	}
	
	private void rearrangeItems()
	{
		angleGap = 360.0f/_menuItems.Count;
		float currentAngle = 270.0f;
		
		for(int i = 0; i < _menuItems.Count; i++)
		{
//			Debug.Log("XX"+_menuItems[i]);
			Transform tempItemTransform = _menuItems[i] as Transform;
//			Debug.Log("YY"+tempItemObject);
			CSUICyclingMenuItem tempCyclingMenuItem = tempItemTransform.GetComponent<CSUICyclingMenuItem>();
//			Debug.Log("ZZ"+xx.GetType().ToString());
			if(currentAngle > 360.0f){
				currentAngle = currentAngle % 360.0f;
			}
			tempCyclingMenuItem.itemDegree = currentAngle;
			tempCyclingMenuItem.UpdateItem();

			currentAngle += angleGap;
			SnapItem();
		}
	}
	
	public void CycleItemLeft()
	{
//		if(isSnapping)
//			return;
//		Debug.Log("CyLeft"+angleGap);
		float moveSpeed = angleGap/moveSpeedFactor;
		for(int i = 0; i < _menuItems.Count ; i++)
		{
			CSUICyclingMenuItem tempItem = (_menuItems[i] as Transform).GetComponent<CSUICyclingMenuItem>();
			tempItem.MoveByDegree(-angleGap,moveSpeed);
		}
	}
	public void CycleItemRight()
	{
//		Debug.Log("CyRight");
		float moveSpeed = angleGap/moveSpeedFactor;
		for(int i = 0; i < _menuItems.Count ; i++)
		{
			CSUICyclingMenuItem tempItem = (_menuItems[i] as Transform).GetComponent<CSUICyclingMenuItem>();
			tempItem.MoveByDegree(angleGap,moveSpeed);
		} 
	}
	public CSUICyclingMenuItem GetMidItem()
	{
//		Debug.Log(_menuItems.Count);
		if (_menuItems.Count == 0) {
//			Debug.Log ("before ret"+_menuItems.Count);
			return null;
		}

		Transform selectedGO = _menuItems[0] as Transform;
//		Debug.Log ("before ret:"+selectedGO.GetType());
		CSUICyclingMenuItem selectedItem = selectedGO.GetComponent<CSUICyclingMenuItem>();
		float SelectedDistance = Mathf.Abs(Mathf.Abs(selectedItem.itemDegree) - 270.0f);

		Transform curSelectedGO;
		CSUICyclingMenuItem curSelectedItem;
		float curDistance;
//		Debug.Log ("goloop");
		for (int i = 0; i < _menuItems.Count; i++) {
//			Debug.Log("loop:"+i);
			curSelectedGO = (_menuItems[i] as Transform);
//			Debug.Log("c:"+curSelectedGO);
			curSelectedItem = curSelectedGO.GetComponent<CSUICyclingMenuItem>();
			curDistance = Mathf.Abs(Mathf.Abs(curSelectedItem.itemDegree) - 270.0f);

			if(curDistance <= SelectedDistance)
			{
//				Debug.Log(curSelectedItem.itemTag+"("+curDistance+")"+"|"+selectedItem.itemTag);
				selectedItem = curSelectedItem;
				selectedGO = curSelectedGO;
				SelectedDistance = curDistance;
			}
//			Debug.Log("i="+i);
		}
		return selectedItem;
	}
	
	
	private void SnapItem()
	{

	}
	public void changeItemScale(CSUICyclingMenuItem itemToScale)
	{
		float posY = itemToScale.transform.position.y;
		float newScale = (_m*posY) + _c;
		float oldZ = itemToScale.transform.localScale.z;
		itemToScale.transform.localScale = new Vector3(newScale,newScale,oldZ);
	}
	//Update
	protected override void Update () {



	}
	//Touches
	void OnTouchDown()
	{

	}
	void OnTouchMoved()
	{
		
	}
	void OnTouchEnded()
	{
						
	}
	void OnMouseDrag()
	{
//		Debug.Log("Drag"+Input.mousePosition);
	}
	void OnMouseEnter()
	{
//		Debug.Log ("Enter");
		prevTouchPos = Input.mousePosition;
		curTouchPos = prevTouchPos;
	}
	void OnMouseExit()
	{
		prevTouchPos = CSConstants.resetTouchPos;
		curTouchPos = CSConstants.resetTouchPos;
	}
	void OnMouseOver()
	{
//		Debug.Log ("Over");
	}
}
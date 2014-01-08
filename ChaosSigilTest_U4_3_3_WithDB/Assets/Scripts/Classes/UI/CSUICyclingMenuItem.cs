using UnityEngine;
using System.Collections;

public class CSUICyclingMenuItem : CSUI {
	
	public float itemDegree;

	public bool isCycling;

	public int itemTag;
//	public Object itemObject;

	public void UpdateItem()
	{
		UpdateDegree();
		UpdateXY();
		UpdateScale();
		UpdateZOrder();
	}
	private void UpdateDegree()
	{
		float newAngle = itemDegree;
		if (itemDegree > 360.0f) 
		{
			newAngle = itemDegree % 360.0f;		
		} 
		else if (itemDegree < 0.0f) 
		{
//			itemDegree = 360.0f + (itemDegree % 360.0f);
			newAngle = 360.0f + (itemDegree % 360.0f);
		}

		itemDegree = newAngle;
	}
	private void UpdateXY()
	{
		CSUICyclingMenu cyclingMenu = transform.parent.GetComponent<CSUICyclingMenu>();
//		Debug.Log("Parent:"+cyclingMenu);
		float newPositionX = cyclingMenu.transform.position.x + cyclingMenu.ellipse.a*Mathf.Cos(itemDegree*Mathf.PI/180.0f);
		float newPositionY = cyclingMenu.transform.position.y + cyclingMenu.ellipse.b*Mathf.Sin(itemDegree*Mathf.PI/180.0f);
		Vector3 newPosition = new Vector3(newPositionX,newPositionY,transform.position.z);
		transform.position = newPosition;
	}
	private void UpdateScale()
	{
		CSUICyclingMenu cyclingMenu = transform.parent.GetComponent<CSUICyclingMenu>();
		cyclingMenu.changeItemScale(this);
	}
	private void UpdateZOrder()
	{
		Vector3 curPos = transform.position;
		Vector3 newPos = new Vector3 (curPos.x,curPos.y,curPos.y);
		transform.position = newPos;
	}

	public void MoveByDegree(float degree, float moveSpeed)
	{
		if(!isCycling)
		StartCoroutine (IMoveByDegree(degree,moveSpeed));
	}
	private IEnumerator IMoveByDegree(float degreeToMove,float moveSpeed)
	{
		isCycling = true;
//		int i = 0;
//		while(i < 100)
//		{
//			i++;
//			Debug.Log("i="+i+"("+degreeToMove+"|"+moveSpeed+")");
//			yield return null;
//		}

		float degreeLeftToProgress = Mathf.Abs(degreeToMove);
		float moveDirection = Mathf.Abs (degreeToMove)/degreeToMove;
//		Debug.Log ("Start:"+itemDegree);
		while (degreeLeftToProgress > 0.0f) 
		{
//			Debug.Log("Start While");
			float degreeToProgress = moveSpeed * Time.deltaTime*moveDirection;
			float tempDegreeLeftToProgress = Mathf.Abs(degreeLeftToProgress) - Mathf.Abs(degreeToProgress);
//			Debug.Log("Left"+degreeLeftToProgress);
			if(tempDegreeLeftToProgress < 0.0f)
			{
				degreeLeftToProgress = 0.0f;
				degreeToProgress = moveDirection*(Mathf.Abs(degreeToProgress)+tempDegreeLeftToProgress);
			}
			else
			{
				degreeLeftToProgress = tempDegreeLeftToProgress;
			}

			itemDegree += degreeToProgress;
//			Debug.Log("SU");
			UpdateItem();
//			Debug.Log("FU"+degreeLeftToProgress);
			if(degreeLeftToProgress > 0.0f)
			{
//				Debug.Log("Return"+degreeLeftToProgress);
				yield return null;
			}
			else
			{
				break;
			}
//			Debug.Log("End While");
		}
		isCycling = false;
//		Debug.Log ("END"+itemDegree);
	}
//	private IEnumerator MoveDegreeFromTo(float startDegree,float targetDegree, float moveSpeed)
//	{
//		float totalDegreeToProgress = targetDegree - startDegree;
//		float degreeLeftToProgress = totalDegreeToProgress;
//
//
//		//to do per frame
//		while (totalDegreeToProgress != 0.0f) 
//		{
//			Debug.Log("DegLeft:"+degreeLeftToProgress);
//			float degreeToProgress = (totalDegreeToProgress * moveSpeed) * Time.deltaTime;
//			float tempDegreeLeftToProgress = Mathf.Abs(degreeLeftToProgress) - Mathf.Abs(degreeToProgress);
//
//			if(tempDegreeLeftToProgress < 0.0f)
//			{
//				degreeToProgress = degreeLeftToProgress;
//				degreeLeftToProgress = 0.0f;
//			}
//			else
//			{
//				degreeToProgress = tempDegreeLeftToProgress;
//			}
//
//			itemDegree += degreeToProgress;
//
//			if(degreeLeftToProgress > 0.0f)
//			{
//				yield return null;
//			}
//		}
//
//	}
	
}

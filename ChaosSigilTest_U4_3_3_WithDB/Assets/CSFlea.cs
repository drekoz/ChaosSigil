using UnityEngine;
using System.Collections;

public class CSFlea : CSGameObject {

	public bool isInAGITraining = true;
	public bool AGITrainingCollide = false;
	public bool AGITrainingMouseDowned = false;

	protected override void Update()
	{
//		base.Update();
//		Debug.Log("GO POS!");
//		transform.position = Vector3.zero;
//		Debug.Log("Flea");
	}
	public void BrustToDeath()
	{
		BreakIMove();
		animator.SetTrigger("Die");
	}

//	void OnCollisionEnter2D(Collision2D collision)
//	{
//		Debug.Log("COLLI");
//
//	}
	void OnTriggerEnter2D(Collider2D col)
	{

		if(col.gameObject.name == gameObject.name)
		{
			return;
		}

		Debug.Log("COL:"+col.gameObject);
		CollidedToPlayerMonster();
	}
	void CollidedToPlayerMonster()
	{
		if(AGITrainingCollide || AGITrainingMouseDowned)
		{
			return;
		}
		AGITrainingCollide = true;
		BrustToDeath();
	}
	public void DestroyFlea()
	{
		Destroy(gameObject);
	}
	public void OnMouseDown()
	{
//		Debug.Log("DOWN");
		DestroyedByPlayer();
	}
	public void DestroyedByPlayer()
	{
		if(AGITrainingCollide || AGITrainingMouseDowned)
		{
			return;
		}
		AGITrainingMouseDowned = true;
		BrustToDeath();
	}
}

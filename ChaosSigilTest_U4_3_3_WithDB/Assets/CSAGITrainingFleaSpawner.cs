using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CSAGITrainingFleaSpawner : MonoBehaviour {

	// Use this for initialization
	public Vector2 targetPosition;
	public float spawnTimerInterval; //persec
	public float spawnProb;

	public float walkSpeed = 1.0f;
	public float jumpSpeed = 1.0f;
	public float flySpeed = 1.0f;

	public List<Vector2> walkSpawnPositions;
	public List<Vector2> jumpSpawnPositions;
	public List<Vector2> flySpawnPositions;
//	public GameObject flyingFlea;
	void Start () {
		CountDownSpawn();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void CountDownSpawn()
	{
		BreakCountDownSpawn();
		StartCoroutine("ICountDownSpawn");
	}
	void BreakCountDownSpawn()
	{
		StopCoroutine("ICountDownSpawn");
	}
	IEnumerator ICountDownSpawn()
	{
		float timeLeft = spawnTimerInterval;
		while(true)
		{
			yield return null;
			timeLeft -= Time.deltaTime;
			if(timeLeft <= 0.0f)
			{
				break;
			}
		}
		FinishSpawnTimer();
	}
	void FinishSpawnTimer()
	{
		SpawnFlea();
		CountDownSpawn();
	}
	void SpawnFlea()
	{
		Debug.Log("Spawn");
		int fleaType = UnityEngine.Random.Range(0,3);
//		fleaType = 2;
		Vector2 spawnerPosition = Vector2.zero;
		string prefabPath;
		Object prefab;
		GameObject clone;

		switch(fleaType){
		case 0://walk
			spawnerPosition = walkSpawnPositions[UnityEngine.Random.Range(0,walkSpawnPositions.Count)];
			transform.position = new Vector3(spawnerPosition.x,spawnerPosition.y,transform.position.z);

			prefabPath = "prefabs/prefab_Flea";
			prefab = Resources.Load(prefabPath, typeof(GameObject));
			clone = Instantiate(prefab) as GameObject;

//			Debug.Log("POS1"+clone.transform.position);
			clone.transform.position = transform.position;
//			Debug.Log("POS2"+clone.transform.position);
			CSGameObject cloneCSGO = clone.GetComponent<CSGameObject>();
			cloneCSGO.speed = walkSpeed;
			cloneCSGO.walkAnimStateName = "Walk";
//			Debug.Log("POS3:"+clone.transform.position+"///"+transform.position+"NAME:"+cloneCSGO);
			cloneCSGO.Move(targetPosition);

			break;
		case 1://fly
			spawnerPosition = flySpawnPositions[UnityEngine.Random.Range(0,flySpawnPositions.Count)];
			transform.position = new Vector3(spawnerPosition.x,spawnerPosition.y,transform.position.z);
			
			prefabPath = "prefabs/prefab_Flea";
			prefab = Resources.Load(prefabPath, typeof(GameObject));
			clone = Instantiate(prefab) as GameObject;
			
//						Debug.Log("POS1"+clone.transform.position);
			clone.transform.position = transform.position;
//						Debug.Log("POS2"+clone.transform.position);
			cloneCSGO = clone.GetComponent<CSGameObject>();
			cloneCSGO.speed = flySpeed;
			cloneCSGO.walkAnimStateName = "Fly";
			//			Debug.Log("POS3:"+clone.transform.position+"///"+transform.position+"NAME:"+cloneCSGO);
			cloneCSGO.Move(targetPosition);
			break;
		case 2://hop
			spawnerPosition = jumpSpawnPositions[UnityEngine.Random.Range(0,jumpSpawnPositions.Count)];
			transform.position = new Vector3(spawnerPosition.x,spawnerPosition.y,transform.position.z);
			
			prefabPath = "prefabs/prefab_Flea";
			prefab = Resources.Load(prefabPath, typeof(GameObject));
			clone = Instantiate(prefab) as GameObject;
			
			//						Debug.Log("POS1"+clone.transform.position);
			clone.transform.position = transform.position;
			//						Debug.Log("POS2"+clone.transform.position);
			cloneCSGO = clone.GetComponent<CSGameObject>();
			cloneCSGO.speed = jumpSpeed;
			cloneCSGO.walkAnimStateName = "Hop";
			cloneCSGO.speed = 1.0f;
			//			Debug.Log("POS3:"+clone.transform.position+"///"+transform.position+"NAME:"+cloneCSGO);
			cloneCSGO.Move(targetPosition);
			break;
		default:

			break;
		}

	}
}

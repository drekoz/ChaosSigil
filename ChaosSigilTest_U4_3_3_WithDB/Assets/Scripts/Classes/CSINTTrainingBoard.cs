using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum INTTrainingBoardDirection {
	kINTTrainingBoardDirectionNone = 0,
	kINTTrainingBoardDirectionUp = 1,
	kINTTrainingBoardDirectionDown = 2,
	kINTTrainingBoardDirectionLeft = 3,
	kINTTrainingBoardDirectionRight = 4,
}

public class CSINTTrainingBoard : MonoBehaviour {


	//-------
	//.....
	//56789
	//01234
	//--------
	
//	private List<Vector2> slotPositionArray; 
	public Vector2 boardOrigin = new Vector2(-2.725f,-2.855f);
	public Vector2 runeSize = new Vector2(1.09f,1.09f);
	public float runeSpeed = 3.0f;
	public float destroySpeed = 0.03f;
	public float fadeOutSpeed = 0.1f;
	public float comboTimer = 3.0f;
	public int boardColumn = 5;
	public int boardRow = 5;
	public int runeCountInPlayTable;
	public CSINTTrainingRune[] runesInTable;
	private bool disableClick = true;

	public GameObject sendMessageTarget = null;

	public string sendMessageScoreByRuneCount = "";
//	public string sendMessageMushroomMethodName = "";
//	public string sendMessageBasicBathMethodName = "";


	public CSINTTrainingRune createRune(INTTrainingRuneType runeType){
		
		string prefabPath = "prefabs/INTTrainingRune/prefab_INTTrainingRune";


		switch(runeType)
		{
		case INTTrainingRuneType.kINTTrainingRuneTypeArrow:
			prefabPath += "Arrow";
			break;
		case INTTrainingRuneType.kINTTrainingRuneTypeInfi:
			prefabPath += "Infi";
			break;
		case INTTrainingRuneType.kINTTrainingRuneTypeLine:
			prefabPath += "Line";
			break;
		case INTTrainingRuneType.kINTTrainingRuneTypeN:
			prefabPath += "N";
			break;
		case INTTrainingRuneType.kINTTrainingRuneTypeRect:
			prefabPath += "Rect";
			break;
		default:
			Debug.Log("INTTrainingRuneType None");
			break;
			
		}
		GameObject clone = null;
		Object prefab = Resources.Load(prefabPath, typeof(GameObject));
//		Debug.Log(prefab);
		clone = Instantiate(prefab) as GameObject;
		clone.transform.parent = gameObject.transform;
		CSINTTrainingRune runeToReturn = clone.GetComponent<CSINTTrainingRune>();
		runeToReturn.speed = runeSpeed;
//		for( int i = 0 ; i < 25 ; i++)
//		{
//			Object prefab = Resources.Load(prefabPath, typeof(GameObject));
//			Debug.Log(prefab);
//			clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
//			clone.transform.parent = gameObject.transform;
//			clone.transform.localPosition = runeSlotToPosition(i);
//		}

//		clone.GetComponent<CSGameObject>().FadeToWithinTime(0.5f,1.0f);
//		Debug.Log(clone);
//		SpriteRenderer sr = clone.GetComponent<SpriteRenderer>();
//		Debug.Log(sr);
//		sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,0.5f);
//		Debug.Log(sr.color.a);

		return runeToReturn;
	}


//-(INTDiamondRune*)createRuneAtSlot:(int)runeSlot withRuneType:(INTDiamondRuneType)runeType{
//		//    CCLOG(@"create Rune");
//		INTDiamondRune* runeToReturn = [INTDiamondRune createRuneType:runeType];
//		[self moveRune:runeToReturn ToSlot:runeSlot];
//		return runeToReturn;
//	}
	CSINTTrainingRune createRuneAtSlot(int runeSlot, INTTrainingRuneType runeType)
	{
		CSINTTrainingRune runeToReturn = createRune(runeType);
		moveRuneToSlot(runeToReturn,runeSlot);
		return runeToReturn;
	}
	void moveRuneToSlot(CSINTTrainingRune runeToMove, int targetSlot)
	{
//		Debug.Log("SLOT Move To"+targetSlot+" SPEED:"+runeToMove.speed);
		if (runeToMove.currentSlotNo == CSINTTrainingRune.RUNE_SLOT_NONE) {
			runeToMove.currentSlotNo = targetSlot;

			Vector2 runePosition = runeSlotToPosition(targetSlot);
			runeToMove.transform.localPosition = new Vector3(runePosition.x,runePosition.y,runeToMove.transform.position.z);
			//cont
			runesInTable[targetSlot] = runeToMove;
			
		}else{

			int oldSlot = runeToMove.currentSlotNo;

			runeToMove.currentSlotNo = targetSlot;

			Vector2 newRunePosition = runeSlotToPosition(targetSlot);
//			Debug.Log("TAR:"+targetSlot+" POS:"+newRunePosition);

//			float moveSpeed = runeFallSpeed*ccpDistance([runeToMove position], newRunePosition);
//			Debug.Log(targetSlot+"/"+runesInTable.Length);
			runesInTable[targetSlot] = runeToMove;
			runesInTable[oldSlot] = null;
			
//			if (![runeToMove parent]) {
//				[self addRuneToParent:runeToMove];
//			}
//			runeToMove.StopAllCoroutines();
//			[runeToMove stopAllActions];
//			runeToMove.Move(newRunePosition);
			runeToMove.Move(newRunePosition,true);
//			Debug.Log("SLOT:"+newRunePosition);
//			[runeToMove runAction:[CCMoveTo actionWithDuration:moveSpeed position:newRunePosition]];
		}
	}
	//	-(void)randomInitialRunesSet{
	//		//random 1st Slot
	//		int randSlot = (arc4random()%runeCountInPlayTable)+25;
//		int runeType = (arc4random()%5)+1;
//		//    randSlot = 25;
//		INTDiamondRune* firstRandomRune = [self createRuneAtSlot:randSlot withRuneType:(INTDiamondRuneType)runeType];
//		//    CCLOG(@"Start Rune Index:%i",randSlot);
//		
//		[self createNextAdjacentRuneFrom:firstRandomRune untilAdjCount:2 inPlayTable:NO];
//		[self fillEmptySlotAtPlayTable:NO];
//	}

	void randomInitialRunesSet()
	{
		int randSlot = UnityEngine.Random.Range(runeCountInPlayTable,(runeCountInPlayTable*2));
//		Debug.Log("randSlot:"+randSlot);
		INTTrainingRuneType runeType = (INTTrainingRuneType)UnityEngine.Random.Range(1,6);
		CSINTTrainingRune firstRandomRune =  createRuneAtSlot(randSlot,runeType);
		createNextAdjacentRuneUntilCount(firstRandomRune,2,false);
		fillEmptySlotAtPlayTable(false);
	}

	void fillEmptySlotAtPlayTable(bool willBePlacedAtPlayTable)
	{
		int startFillSlot;
		int endFillSlot;
		
		if (willBePlacedAtPlayTable) {
			startFillSlot = 0;
			endFillSlot = runeCountInPlayTable;
		}else{
			startFillSlot = runeCountInPlayTable;
			endFillSlot = runeCountInPlayTable*2;
		}
		
		
		for (int i = startFillSlot; i < endFillSlot; i++) {
			CSINTTrainingRune tempRune = runesInTable[i];
			if (tempRune == null) {
				INTTrainingRuneType runeType = (INTTrainingRuneType)UnityEngine.Random.Range(1,6);
				CSINTTrainingRune firstRandomRune =  createRuneAtSlot(i,runeType);
			}
		}
	}
	void runesFall()
	{
		for (int i = 0;  i < boardRow; i++) {
			for (int columnOffset = 0; columnOffset < boardColumn; columnOffset++) {
				for (int rowOffset = boardColumn; rowOffset <= (boardColumn*boardColumn*2)-boardColumn; rowOffset += boardColumn) {
					
					int currentSlotIndex = columnOffset+rowOffset;
					//            CCLOG(@"curIndex:%i",currentSlotIndex);
					CSINTTrainingRune currentRune = runesInTable[currentSlotIndex];
					int lowerSlotIndex = currentSlotIndex - boardColumn;
					CSINTTrainingRune lowerRune = runesInTable[lowerSlotIndex];
					
					//            CCLOG(@"cur:%@ low:%@",currentRune,lowerRune);
					if (currentRune != null && lowerRune == null) {
						moveRuneToSlot(currentRune,lowerSlotIndex);
//						Debug.Log("Lower:"+lowerSlotIndex);
					}
					
				}
			}
		}

//		string printString = "";
//		int rowCount = 0;
//		for(int x = 0 ; x < runesInTable.Length ; x++)
//		{
//			
//			
//			if(runesInTable[x] != null)
//			{
//				printString += "1 ";
//				
//			}
//			else
//			{
//				printString += "0 ";
//			}
//			
//			if(x%4 == 0 && x != 0)
//			{
//				printString += "\n";
//			}
//
//		}
//		Debug.Log(printString +"LEN"+printString.Length);
//		Debug.Log("--------");
	}
	
	Vector2 runeSlotToPosition(int slotToMap)
	{
		int slot = slotToMap;
		int runeRow = slot/boardColumn;
		int runeCol = slot%boardColumn;
		
		float posX = (boardOrigin.x+runeSize.x/2.0f) + (runeCol*runeSize.x);
		float posY = (boardOrigin.y+runeSize.y/2.0f) + (runeRow*runeSize.y);
//		Debug.Log("pos"+posX+"|"+posY+"|"+(boardOrigin.y+runeSize.y/2.0f));
		return new Vector2(posX,posY);
	}

	public void runeOnMouseDown(CSINTTrainingRune rune)
	{
		if(disableClick)
		{
			return;
		}
//		Debug.Log("RUNE:"+rune+"xx"+findAllAdjacentRuneWithRune(rune).Count);
//		Debug.Log("RUNE:"+findAllAdjacentRuneSetsWithAtleastMemberCount(2));
		List<CSINTTrainingRune> runeSetToDestroy = findAllAdjacentRuneWithRune(rune);

		if(runeSetToDestroy.Count >= 3)
		{
//			Debug.Log("rune:"+runeSetToDestroy.Count);
			foreach(CSINTTrainingRune tempRune in runeSetToDestroy)
			{
				int runeSlotToDestroy = tempRune.currentSlotNo;
				destroyRuneAtSlot(runeSlotToDestroy);
				fillTopRunesTableWithColumnOfSlot(runeSlotToDestroy);
			}
			updateHitRunes(runeSetToDestroy.Count);
			runesFall();
		}
		if(!destructibleRuneSetExist())
		{
			destroyAllRunes();
			randomInitialRunesSet();
			runesFall();
		}
		CountDownDisableClick();

	}
		// Use this for initialization
	void Start () {
//		createRune(INTTrainingRuneType.kINTTrainingRuneTypeArrow);
		runeCountInPlayTable = boardRow*boardColumn;
		runesInTable = new CSINTTrainingRune[runeCountInPlayTable*2];

		randomInitialRunesSet();
		runesFall();
		CountDownDisableClick();
	}
	
	// Update is called once per frame
	void Update () {

	}

	int createNextAdjacentRuneUntilCount(CSINTTrainingRune currentRune, int createUntilCount, bool willBePlacedAtPlayTable)
	{
		if (createUntilCount == 0) {
			return 0;
		}
		
		int currentRuneIndex = currentRune.currentSlotNo;
		INTTrainingRuneType currentRuneType = currentRune.intTrainingRuneType;
		
		if (willBePlacedAtPlayTable) {
			if (currentRuneIndex >= runeCountInPlayTable) {
//				CCLOG(@"Error:createNextAdjRuneFrom(currentRuneIndex%i) of %i",currentRuneIndex,runeCountInPlayTable);
				return -99;
			}
		}
//		
//		//เช็คว่าอยู่ขอบรึเปล่า
		bool isTopEdge;
		if (willBePlacedAtPlayTable) {
			isTopEdge = (currentRuneIndex >= ((boardRow -1)*boardColumn)) && ( currentRuneIndex < runeCountInPlayTable);
		}else{
//			Debug.Log("TOP:"+currentRuneIndex);
			isTopEdge = (currentRuneIndex >= (((boardRow*2) -1)*boardColumn)) && ( currentRuneIndex < (runeCountInPlayTable*2));
		}
//		
//		
		bool isBottomEdge;
		if (willBePlacedAtPlayTable) {
			isBottomEdge = (currentRuneIndex >= 0 ) && ( currentRuneIndex < boardColumn);
//			
		}else{
			isBottomEdge = (currentRuneIndex >= (boardRow*boardColumn)) && ( currentRuneIndex < ((boardRow*boardColumn)+boardColumn));
		}
		
		
		bool isLeftEdge = currentRuneIndex%boardColumn == 0;
		bool isRightEdge = (currentRuneIndex - (boardColumn -1) )%boardColumn == 0;
//		//random
		int runeCreatedCount = 0;
		List<INTTrainingBoardDirection> randomDirectionCandidates = new List<INTTrainingBoardDirection>();
//		
		while (createUntilCount != 0) {
//			
//			
//			//เช็คว่าด้านที่ติดกัน nil? ถ้า ว่างก็แสดงว่าสามารถสร้าง rune เข้าไปได้ (ยัด slot ใส่ candidate)
			bool isTopNil = false;
			bool isBottomNil = false;
			bool isLeftNil = false;
			bool isRightNil = false;
//			
//			
			if (!isTopEdge){

				isTopNil = runesInTable[currentRuneIndex+boardColumn] == null;
			}
			if (!isBottomEdge){
				isBottomNil = runesInTable[currentRuneIndex-boardColumn] == null;
			}
			if (!isLeftEdge){
				isLeftNil = runesInTable[currentRuneIndex-1] == null;
			}
			if (!isRightEdge){
				isRightNil = runesInTable[currentRuneIndex+1] == null;		
			}
			
			
//			สร้าง candidate array (สร้างทางไหนได้บ้าง)
//			NSNumber* goTop = [NSNumber numberWithInteger:kINTDiamondBoardMoveDirectionTop];
//			NSNumber* goBottom = [NSNumber numberWithInteger:kINTDiamondBoardMoveDirectionBottom];
//			NSNumber* goLeft = [NSNumber numberWithInteger:kINTDiamondBoardMoveDirectionLeft];
//			NSNumber* goRight = [NSNumber numberWithInteger:kINTDiamondBoardMoveDirectionRight];
//			
			randomDirectionCandidates.Clear();

			if (!isTopEdge && isTopNil) {
				randomDirectionCandidates.Add(INTTrainingBoardDirection.kINTTrainingBoardDirectionUp);
			}
			if (!isBottomEdge && isBottomNil) {
				randomDirectionCandidates.Add(INTTrainingBoardDirection.kINTTrainingBoardDirectionDown);
			}
			if (!isLeftEdge && isLeftNil) {
				randomDirectionCandidates.Add(INTTrainingBoardDirection.kINTTrainingBoardDirectionLeft);
			}
			if (!isRightEdge && isRightNil) {
				randomDirectionCandidates.Add(INTTrainingBoardDirection.kINTTrainingBoardDirectionRight);
			}

			if (randomDirectionCandidates.Count == 0) {
				break;
			}

			int randomIndex = UnityEngine.Random.Range(0,randomDirectionCandidates.Count);
			INTTrainingBoardDirection nextRandomDirection = randomDirectionCandidates[randomIndex];
			randomDirectionCandidates.RemoveAt(randomIndex);
			
			CSINTTrainingRune nextRune = null;

			if (nextRandomDirection == INTTrainingBoardDirection.kINTTrainingBoardDirectionUp) {
				nextRune = createRuneAtSlot(currentRuneIndex+boardColumn,currentRuneType);
			}
			else if (nextRandomDirection == INTTrainingBoardDirection.kINTTrainingBoardDirectionDown){
				nextRune = createRuneAtSlot(currentRuneIndex-boardColumn,currentRuneType);	
			}
			else if (nextRandomDirection == INTTrainingBoardDirection.kINTTrainingBoardDirectionLeft){
				nextRune = createRuneAtSlot(currentRuneIndex-1,currentRuneType);		
			}
			else if (nextRandomDirection == INTTrainingBoardDirection.kINTTrainingBoardDirectionRight){
				nextRune = createRuneAtSlot(currentRuneIndex+1,currentRuneType);
			}
			
			if (nextRune != null) {

				int previousRuneCreateCount = createNextAdjacentRuneUntilCount(nextRune,createUntilCount-1,false);
				runeCreatedCount++;
				if (createUntilCount-1 == 0) {
					break;
				}
				runeCreatedCount += previousRuneCreateCount;
				
				createUntilCount -= runeCreatedCount;
//				
			}
		}
		randomDirectionCandidates.Clear();		
		return runeCreatedCount;
	}
	List<CSINTTrainingRune> findAllAdjacentRuneWithRune(CSINTTrainingRune currentRune)
	{
		//    CCLOG(@"find rune");
		List<CSINTTrainingRune> runeSetToReturn = new List<CSINTTrainingRune>();
		runeSetToReturn.Add(currentRune);
		currentRune.isVisited = true;
		//pre stage
		int currentRuneIndex = currentRune.currentSlotNo;
		INTTrainingRuneType currentRuneType = currentRune.intTrainingRuneType;

		//เช็คว่าอยู่ขอบรึเปล่า
		bool isTopEdge = (currentRuneIndex >= ((boardRow -1)*boardColumn)) && ( currentRuneIndex < runeCountInPlayTable);
		bool isBottomEdge = (currentRuneIndex >= 0 ) && ( currentRuneIndex < boardColumn);
		bool isLeftEdge = currentRuneIndex%boardColumn == 0;
		bool isRightEdge = (currentRuneIndex - (boardColumn -1) )%boardColumn == 0;
		//random

		List<INTTrainingBoardDirection> randomDirectionCandidates = new List<INTTrainingBoardDirection>();
		
		while (true) {
			//เช็คว่าด้านที่ติดกัน nil? ถ้า ว่างก็แสดงว่าสามารถสร้าง rune เข้าไปได้ (ยัด slot ใส่ candidate)
			bool isTopNil = false;
			bool isBottomNil = false;
			bool isLeftNil = false;
			bool isRightNil = false;
			
			bool isTopSameType = false;
			bool isBottomSameType = false;
			bool isLeftSameType = false;
			bool isRightSameType = false;
			
			bool isTopVisited = false;
			bool isBottomVisited = false;
			bool isLeftVisited = false;
			bool isRightVisited = false;
			//        CCLOG(@"c1");
			if (!isTopEdge){
				int topIndex = currentRuneIndex+boardColumn;
//				Debug.Log("Not TopEdge:IND:"+topIndex);
				CSINTTrainingRune topRune = runesInTable[topIndex];
				isTopNil = (topRune == null);
				
				if (!isTopNil) {
					isTopSameType = topRune.intTrainingRuneType == currentRuneType;
					isTopVisited = topRune.isVisited;
				}
			}
			
			if (!isBottomEdge){
				int bottomIndex = currentRuneIndex-boardColumn;
				CSINTTrainingRune bottomRune = runesInTable[bottomIndex];
				isBottomNil = (bottomRune == null);
				
				if (!isBottomNil) {

					isBottomSameType = bottomRune.intTrainingRuneType == currentRuneType;
					isBottomVisited = bottomRune.isVisited;
				}
			}
			
			if (!isLeftEdge){
				int leftIndex = currentRuneIndex-1;
				CSINTTrainingRune leftRune = runesInTable[leftIndex];
				isLeftNil = (leftRune == null);
				
				if (!isLeftNil) {
					isLeftSameType = leftRune.intTrainingRuneType == currentRuneType;
					isLeftVisited = leftRune.isVisited;
				}
			}
			
			if (!isRightEdge){
				int rightIndex = currentRuneIndex+1;
				CSINTTrainingRune rightRune = runesInTable[rightIndex];
				isRightNil = (rightRune == null);
				
				if (!isRightNil) {
					isRightSameType = rightRune.intTrainingRuneType == currentRuneType;
					isRightVisited = rightRune.isVisited;
				}
			}
			//        CCLOG(@"c2");
			//STARTHERE!
			//สร้าง candidate array (สร้างทางไหนได้บ้าง)

//			NSNumber* goTop = [NSNumber numberWithInteger:kINTDiamondBoardMoveDirectionTop];
//			NSNumber* goBottom = [NSNumber numberWithInteger:kINTDiamondBoardMoveDirectionBottom];
//			NSNumber* goLeft = [NSNumber numberWithInteger:kINTDiamondBoardMoveDirectionLeft];
//			NSNumber* goRight = [NSNumber numberWithInteger:kINTDiamondBoardMoveDirectionRight];

			randomDirectionCandidates.Clear();
			
			if (!isTopEdge && !isTopNil && !isTopVisited && isTopSameType) {
				randomDirectionCandidates.Add(INTTrainingBoardDirection.kINTTrainingBoardDirectionUp);
			}
			if (!isBottomEdge && !isBottomNil && !isBottomVisited && isBottomSameType) {
				randomDirectionCandidates.Add(INTTrainingBoardDirection.kINTTrainingBoardDirectionDown);
			}
			if (!isLeftEdge && !isLeftNil && !isLeftVisited && isLeftSameType) {
				randomDirectionCandidates.Add(INTTrainingBoardDirection.kINTTrainingBoardDirectionLeft);
			}
			if (!isRightEdge && !isRightNil && !isRightVisited && isRightSameType) {
				randomDirectionCandidates.Add(INTTrainingBoardDirection.kINTTrainingBoardDirectionRight);
			}
			
			//        CCLOG(@"find Adj Directions:%i",[randomDirectionCandidates count]);
			if (randomDirectionCandidates.Count == 0) {
				break;
			}
			//        CCLOG(@"c3");

			int randomIndex = UnityEngine.Random.Range(0,randomDirectionCandidates.Count);
			INTTrainingBoardDirection nextRandomDirection = randomDirectionCandidates[randomIndex];
			randomDirectionCandidates.RemoveAt(randomIndex);
			
			CSINTTrainingRune nextRune = null;
			
			if (nextRandomDirection == INTTrainingBoardDirection.kINTTrainingBoardDirectionUp) {

				nextRune = runesInTable[currentRuneIndex+boardColumn];
				List<CSINTTrainingRune> runeSet = findAllAdjacentRuneWithRune(nextRune);
				runeSetToReturn.AddRange(runeSet);
			}
			else if (nextRandomDirection == INTTrainingBoardDirection.kINTTrainingBoardDirectionDown){

				nextRune = runesInTable[currentRuneIndex-boardColumn];
				//            CCLOG(@"next:Bottom[%i]",currentRuneIndex-boardColumn);
				List<CSINTTrainingRune> runeSet = findAllAdjacentRuneWithRune(nextRune);
				runeSetToReturn.AddRange(runeSet);

				//            CCLOG(@"next:Top[%i]",currentRuneIndex+boardColumn);
			}
			else if (nextRandomDirection == INTTrainingBoardDirection.kINTTrainingBoardDirectionLeft){
				
				nextRune = runesInTable[currentRuneIndex-1];
				//            [runeSetToReturn addObject:nextRune];
				//            CCLOG(@"next:Left[%i]",currentRuneIndex-1);
				List<CSINTTrainingRune> runeSet = findAllAdjacentRuneWithRune(nextRune);

				runeSetToReturn.AddRange(runeSet);
				//            CCLOG(@"next:Top[%i]",currentRuneIndex+boardColumn);
			}
			else if (nextRandomDirection == INTTrainingBoardDirection.kINTTrainingBoardDirectionRight){
				
				nextRune = runesInTable[currentRuneIndex+1];
				//            [runeSetToReturn addObject:nextRune];
				//            CCLOG(@"next:Right[%i]",currentRuneIndex+1);
				List<CSINTTrainingRune> runeSet = findAllAdjacentRuneWithRune(nextRune);
				runeSetToReturn.AddRange(runeSet);
			}
			
		}
		//    CCLOG(@"before return");
//		Debug.Log("LOG:"+runesInTable.Length);
//		for(int i = 0 ; i < runesInTable.Length ; i++)
//		{
//			if(runesInTable[i] != null)
//			{
//				runesInTable[i].isVisited = false;
//			}
//		}
		return runeSetToReturn;
	}
	List<List<CSINTTrainingRune>>findAllAdjacentRuneSetsWithAtleastMemberCount(int memberCount)
	{
		List<List<CSINTTrainingRune>> runeSets = new List<List<CSINTTrainingRune>>();

		for(int i = 0; i < runeCountInPlayTable; i++){
			CSINTTrainingRune tempRune = runesInTable[i];
			if(tempRune != null)
			{
				if(!tempRune.isVisited)
				{
					List<CSINTTrainingRune> runeSet = findAllAdjacentRuneWithRune(tempRune);
					if(runeSet.Count >= memberCount)
					{
						runeSets.Add(runeSet);
					}
				}
			}
		}

		foreach(CSINTTrainingRune tempRune in runesInTable)
		{
			if(tempRune != null)
			{
				tempRune.isVisited = false;
			}
		}
		return runeSets;
	}

	bool destructibleRuneSetExist()
	{
		List<List<CSINTTrainingRune>> destructibleRuneSets = findAllAdjacentRuneSetsWithAtleastMemberCount(3);
		if(destructibleRuneSets.Count > 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	void destroyRuneAtSlot(int runeSlot)
	{
		CSINTTrainingRune runeToDestroy = runesInTable[runeSlot];
		runeToDestroy.FadeToWithinTime(0.0f,fadeOutSpeed);
		Destroy(runeToDestroy.gameObject,fadeOutSpeed);
		runesInTable[runeSlot] = null;
		Vector2 pos = new Vector2(runeToDestroy.transform.position.x,runeToDestroy.transform.position.y);

//		Debug.Log("des");
		string prefabPath = "prefabs/INTTrainingRune/prefab_INTTrainingRuneDestroyEffect";
		Object prefab = Resources.Load(prefabPath, typeof(GameObject));
		GameObject clone = Instantiate(prefab) as GameObject;
		clone.transform.position = new Vector3(pos.x,pos.y,clone.transform.position.z);
		Destroy(clone,destroySpeed);
	}
	void fillTopRunesTableWithColumnOfSlot(int runeSlot)
	{
		int runeCol = runeSlot%boardColumn;
		fillTopRunesTableAtColumn(runeCol);
	}
	void fillTopRunesTableAtColumn(int column)
	{
		int columnOffset = column;
		for(int rowOffset = boardColumn*boardColumn; rowOffset <= (boardColumn*boardColumn*2)-boardColumn;rowOffset+=boardColumn)
		{
			int currentSlot = columnOffset+rowOffset;
			CSINTTrainingRune currentRune = runesInTable[currentSlot];
			if(currentRune == null)
			{
				createRuneAtSlot(currentSlot,randomRuneType());
				break;
			}
		}
	}
	INTTrainingRuneType randomRuneType()
	{
		return (INTTrainingRuneType)UnityEngine.Random.Range(1,5);
	}
	void destroyAllRunes()
	{
		for (int i = 0; i < runesInTable.Length; i++) {
			if (runesInTable[i] != null) {
				destroyRuneAtSlot(i);
			}
		}
	}
	void CountDownDisableClick()
	{
		disableClick = true;
		StopCoroutine("ICountDownDisableClick");
		StartCoroutine("ICountDownDisableClick");
	}
	IEnumerator ICountDownDisableClick()
	{
		while(true)
		{
			bool willBreakLoop = true;
			foreach(CSINTTrainingRune tempRune in runesInTable)
			{
				if(tempRune != null)
				{
					if(tempRune.IsMoving)
					{
						willBreakLoop = false;
					}
				}
			}

			if(willBreakLoop)
			{
				disableClick = false;
				break;
			}
			yield return null;
		}
	}
	//call back
	void updateHitRunes(int numberOfHitRunes)
	{
//		Debug.Log("RuneSetCount:"+numberOfHitRunes);
		DoSendMessage(sendMessageScoreByRuneCount,(object)numberOfHitRunes);
	}

	protected void DoSendMessage( string methodName, object parameter )
	{
		if (sendMessageTarget != null && methodName.Length > 0)
		{
			sendMessageTarget.SendMessage( methodName, parameter, SendMessageOptions.RequireReceiver );
		}
	}
}

using UnityEngine;
using System.Collections;

public class CSFarmUIController : MonoBehaviour {

	Vector2 LeftPanelStartPosition;
	Vector2 RightPanelStartPosition;
	Vector2 LeftPanelPopPosition;
	Vector2 RightPanelPopPosition;

	enum FarmUIPanel {kFarmUIPanelPet, kFarmUIPanelOptions, kFarmUIPanelWOrld, kFarmUIPanelPlayer};

	CSFarmUIPanelPet petPanel;
	CSFarmUIPanelOptions optionsPanel;
	CSFarmUIPanelWorld worldPanel;
	CSFarmUIPanelPlayer playerPanel;


	public GameObject sendMessageTarget = null;
	//Pet Panel
	public string sendMessageMeatMethodName = "";
	public string sendMessageMushroomMethodName = "";

	public string sendMessageBasicBathMethodName = "";

//	public string sendMessageSTRTrainMethodName = "";
//	public string sendMessageAGITrainMethodName = "";
//	public string sendMessageINTTrainMethodName = "";

	void Start()
	{
		LeftPanelStartPosition = new Vector2(-8.78f,0.0f);
		RightPanelStartPosition = new Vector2(8.78f,0.0f);
		LeftPanelPopPosition = new Vector2 (-2.58f,0.0f);
		RightPanelPopPosition = new Vector2 (2.59f,0.0f);

		
		for(int i = 0; i < transform.childCount ; i++)
		{
			Transform tempGameObject = transform.GetChild(i);
			//			Debug.Log("Child-Type:"+tempGameObject.GetType().ToString());
//			Debug.Log("Child:"+tempGameObject);

			if(tempGameObject.name == "PetPanel")
			{

//				CSFarmUIPanel obj = tempGameObject.gameObject.GetComponent<CSFarmUIPanel>();
//				Debug.Log("LOG:"+tempGameObject.gameObject.GetComponent<BoxCollider>());

				petPanel = tempGameObject.gameObject.GetComponent<CSFarmUIPanelPet>();
			}
			else if(tempGameObject.name == "WorldPanel")
			{
				worldPanel = tempGameObject.gameObject.GetComponent<CSFarmUIPanelWorld>();
			}
			else if(tempGameObject.name == "OptionsPanel")
			{
				optionsPanel = tempGameObject.gameObject.GetComponent<CSFarmUIPanelOptions>();
			}
			else if(tempGameObject.name == "PlayerPanel")
			{
				playerPanel = tempGameObject.gameObject.GetComponent<CSFarmUIPanelPlayer>();
			}

			//			Debug.Log("ChildX:"+_menuItems[i]);
		}
	}
	private void DoSendMessage(string methodName){
		if (sendMessageTarget != null && methodName.Length > 0) {
			sendMessageTarget.SendMessage(methodName,SendMessageOptions.RequireReceiver);
		}
	}

	//PetPanel
	void PopPetPanelPressed()
	{
		Debug.Log("CSFarmUIController -> PopPetPanelPressed()");
		PopPetPanel();
	}
	void UnPopPetPanelPressed()
	{
		UnPopPetPanel();
	}
	void PopPetPanel()
	{

		if(petPanel.IsMoving || petPanel.isPop)
		{
			return;
		}
		UnPopAllPanels();
		petPanel.Move(LeftPanelPopPosition);
		petPanel.isPop = true;
	}
	void UnPopPetPanel()
	{

		if(petPanel.IsMoving || !petPanel.isPop)
		{
			return;
		}
		petPanel.Move(LeftPanelStartPosition);
		petPanel.isPop = false;
	}
	void FeedPressed()
	{
		petPanel.trainingMenus.SetActive(false);
		petPanel.bathMenus.SetActive(false);
		petPanel.feedMenus.SetActive(true);
	}
	void BathPressed()
	{
		petPanel.trainingMenus.SetActive(false);
		petPanel.feedMenus.SetActive(false);
		petPanel.bathMenus.SetActive(true);
	}
	void TrainPressed()
	{
		petPanel.bathMenus.SetActive(false);
		petPanel.feedMenus.SetActive(false);
		petPanel.trainingMenus.SetActive(true);
	}
	void MeatPressed()
	{
		UnPopAllPanels();
		DoSendMessage(sendMessageMeatMethodName);
	}
	void MushroomPressed()
	{
		UnPopAllPanels();
		DoSendMessage(sendMessageMushroomMethodName);
	}
	void BasicBathPressed()
	{
		UnPopAllPanels();
		DoSendMessage(sendMessageBasicBathMethodName);
	}
	void STRPressed()
	{

	}
	void AGIPressed()
	{

	}
	void INTPressed()
	{
		CSGameManager.Instance.changeScene("INTTrainingScene");
	}
	//PlayerPanel
	void PopPlayerPanelPressed()
	{
		Debug.Log("CSFarmUIController -> PopPlayerPanelPressed()");
		PopPlayerPanel();
	}
	void UnPopPlayerPanelPressed()
	{
		UnPopPlayerPanel();
	}
	void PopPlayerPanel()
	{
		
		if(playerPanel.IsMoving || playerPanel.isPop)
		{
			return;
		}
		UnPopAllPanels();
		playerPanel.Move(RightPanelPopPosition);
		playerPanel.isPop = true;
	}
	void UnPopPlayerPanel()
	{
		
		if(playerPanel.IsMoving || !playerPanel.isPop)
		{
			return;
		}
		playerPanel.Move(RightPanelStartPosition);
		playerPanel.isPop = false;
	}
	void PlayerPressed()
	{
		playerPanel.monsterMenus.SetActive(false);
		playerPanel.playerMenus.SetActive(true);
	}
	void MonsterPressed()
	{
		playerPanel.playerMenus.SetActive(false);
		playerPanel.monsterMenus.SetActive(true);
	}
	void PlayerProfilePressed()
	{

	}
	void PlayerInventoryPressed()
	{

	}
	void ShopPressed()
	{

	}
	void MonsterStatusPressed()
	{

	}
	void MonsterSkillsPressed()
	{

	}
	void MonsterEquipmentPressed()
	{

	}
	//WorldPanel
	void PopWorldPanelPressed()
	{
		Debug.Log("CSFarmUIController -> PopWorldPanelPressed()");
		PopWorldPanel();
	}
	void UnPopWorldPanelPressed()
	{
		UnPopWorldPanel();
	}
	void PopWorldPanel()
	{
		
		if(worldPanel.IsMoving || worldPanel.isPop)
		{
			return;
		}
		UnPopAllPanels();
		worldPanel.Move(RightPanelPopPosition);
		worldPanel.isPop = true;
	}
	void UnPopWorldPanel()
	{
		
		if(worldPanel.IsMoving || !worldPanel.isPop)
		{
			return;
		}
		worldPanel.Move(RightPanelStartPosition);
		worldPanel.isPop = false;
	}
	void WorldMapPressed()
	{
		
	}
	void PVPPressed()
	{
		
	}
	//OptionsPanel
	void PopOptionsPanelPressed()
	{
		Debug.Log("CSFarmUIController -> PopOptionsPanelPressed()");
		PopOptionsPanel();
	}
	void UnPopOptionsPanelPressed()
	{
		UnPopOptionsPanel();
	}
	void PopOptionsPanel()
	{
		
//		if(optionsPanel.IsMoving || optionsPanel.isPop)
//		{
//			return;
//		}
//		optionsPanel.Move(LeftPanelPopPosition);
//		optionsPanel.isPop = !optionsPanel.isPop;
		UnPopAllPanels();
		optionsPanel.gameObject.SetActive(true);
		optionsPanel.isPop = true;
	}
	void UnPopOptionsPanel()
	{
		
//		if(optionsPanel.IsMoving || !optionsPanel.isPop)
//		{
//			return;
//		}
//		optionsPanel.Move(LeftPanelStartPosition);
//		optionsPanel.isPop = !optionsPanel.isPop;
		optionsPanel.gameObject.SetActive(false);
		optionsPanel.isPop = false;
	}
	//General
	void UnPopAllPanels()
	{
		UnPopPetPanel();
		UnPopPlayerPanel();
		UnPopOptionsPanel();
		UnPopWorldPanel();
	}
}

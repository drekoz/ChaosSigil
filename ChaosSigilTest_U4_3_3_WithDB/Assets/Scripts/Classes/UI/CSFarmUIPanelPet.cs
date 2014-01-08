using UnityEngine;
using System.Collections;

public class CSFarmUIPanelPet : CSFarmUIPanel {

	// Use this for initialization
	public GameObject feedMenus;
	public GameObject bathMenus;
	public GameObject trainingMenus;

	void Start () {
		feedMenus = GameObject.Find("FeedMenus");
		bathMenus = GameObject.Find("BathMenus");
		trainingMenus = GameObject.Find("TrainingMenus");
		bathMenus.SetActive(false);
		trainingMenus.SetActive(false);
	}

}

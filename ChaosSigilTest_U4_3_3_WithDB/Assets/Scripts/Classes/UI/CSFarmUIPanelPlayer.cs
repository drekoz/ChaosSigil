using UnityEngine;
using System.Collections;

public class CSFarmUIPanelPlayer : CSFarmUIPanel {

	public GameObject playerMenus;
	public GameObject monsterMenus;

	// Use this for initialization
	void Start () {
		playerMenus = GameObject.Find("PlayerMenus");
		monsterMenus = GameObject.Find("MonsterMenus");
		playerMenus.SetActive(false);
		monsterMenus.SetActive(true);
	}
}

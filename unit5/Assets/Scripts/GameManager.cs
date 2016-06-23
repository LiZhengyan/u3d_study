using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager Instance =null;
	int m_score=0; //游戏得分
	static int m_hiscore=0; //历史最高分

	int m_ammo=100; //弹药数
	playerCtrl m_player;
	GUIText txt_ammo;
	GUIText txt_hiscore;
	GUIText txt_life;
	GUIText txt_score;
	void Start () {
		Instance=this;
		m_player=GameObject.FindGameObjectWithTag("Player").GetComponent<playerCtrl>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

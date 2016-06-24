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
		txt_ammo=this.transform.FindChild("m_ammo").GetComponent<GUIText>();
		txt_hiscore=this.transform.FindChild("higscore").GetComponent<GUIText>();
		txt_life=this.transform.FindChild("Life").GetComponent<GUIText>();
		txt_score=this.transform.FindChild("score").GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SetScore(int score){
		m_score+=score;
		if(m_score>m_hiscore){
			m_hiscore=m_score;
		}

		txt_score.text="Score "+m_score;
		txt_hiscore.text="High Score "+ m_hiscore; 
	}

	public void SetAmmo(int ammo){
		m_ammo-=ammo;
		if(m_ammo<=0) m_ammo=100;
		txt_ammo.text=m_ammo.ToString()+"/100";
	}
	public void SetLife(int life){
		txt_life.text=life.ToString();
	}
}

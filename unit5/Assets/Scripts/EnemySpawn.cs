using UnityEngine;
using System.Collections;

public class EnemySpawn: MonoBehaviour {
	public Transform m_enemy; //敌人的prefab
	public int m_enemyCount=0;  //敌人的生产数量
	public int m_maxEnemy=3;  //最大数量
	public float m_timer=0 ; //生成敌人的间隔时间
	protected Transform m_transfrom;
	// Use this for initialization
	void Start () {
		m_transfrom=this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_enemyCount>=m_maxEnemy) return;
		m_timer-=Time.deltaTime;
		if(m_timer<=0){
			m_timer=5;
			Transform obj =(Transform) Instantiate(m_enemy,m_transfrom.position,Quaternion.identity);
								//Quaternion.identity 完全对其与世界轴或者父轴
			Enemy enemy=obj.GetComponent<Enemy>();
			enemy.Init(this);
		} 
		
	}
	void OnDrawGizmos(){
		Gizmos.DrawIcon(transform.position,"item.png",true);
	}
}

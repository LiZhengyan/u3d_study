using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	Transform m_transform;  //自身
	playerCtrl m_player;    //Player
	NavMeshAgent m_agent;  //寻路路径
	float m_moveSpeed=0.5f;  //移动速度

	float m_rotspeed=120; //角色旋转速度
	Animator m_ani;   //动画组建
	int m_life=15; //生命值
	float m_timer=2;

	

	void Start () {
		m_transform=this.transform;
		m_player=GameObject.FindGameObjectWithTag("Player").GetComponent<playerCtrl>();
		m_agent=GetComponent<NavMeshAgent>();
		m_ani=GetComponent<Animator>();
		m_agent.SetDestination(m_player.m_transfrom.position);

	
	}
	
	// Update is called once per frame
	void Update () {
		
		//如果猪脚生命为0 就啥都不做
		if (m_player.m_life <= 0)
			return;
		
		//获取当前动画状态
		//在Update函数中，首先获得了一个AnimatorStateInfo对象，
		//它保存着动画的状态，敌人包括待机、跑步、攻击、死亡4种状态，我们根据不同的状态处理不同的逻辑。
		//无论哪种状态，都使用了IsInTransition判断是否是过渡状态，如果是，什么也不做。

		AnimatorStateInfo stateInfo = m_ani.GetCurrentAnimatorStateInfo(0);
		// Èç¹û´¦ÓÚ´ý»ú×´Ì¬
		if (stateInfo.nameHash == Animator.StringToHash("Base Layer.idle") && !m_ani.IsInTransition(0))
		{
			m_ani.SetBool("idle", false);
			
			// ´ý»úÒ»¶¨Ê±¼ä
			m_timer -= Time.deltaTime;
			if (m_timer > 0)
				return;
			
			// Èç¹û¾àÀëÖ÷½ÇÐ¡ÓÚ1.5Ã×£¬½øÈë¹¥»÷¶¯»­×´Ì¬
			if (Vector3.Distance(m_transform.position, m_player.m_transfrom.position) < 1.5f)
			{
				m_ani.SetBool("attack", true);
			}
			else
			{
				// ÖØÖÃ¶¨Ê±Æ÷
				m_timer=1;
				
				// ÉèÖÃÑ°Â·Ä¿±êµã
				m_agent.SetDestination(m_player. m_transfrom.position);
				
				// ½øÈëÅÜ²½¶¯»­×´Ì¬
				m_ani.SetBool("run", true);
			}
		}
		
		// Èç¹û´¦ÓÚÅÜ²½×´Ì¬
		if (stateInfo.nameHash == Animator.StringToHash("Base Layer.run") && !m_ani.IsInTransition(0))
		{
			
			m_ani.SetBool("run", false);
			
			
			// Ã¿¸ô1ÃëÖØÐÂ¶¨Î»Ö÷½ÇµÄÎ»ÖÃ
			m_timer -= Time.deltaTime;
			if (m_timer < 0)
			{
				m_agent.SetDestination(m_player. m_transfrom.position);
				
				m_timer = 1;
			}
			
			// ×·ÏòÖ÷½Ç
			MoveTo();
			
			// Èç¹û¾àÀëÖ÷½ÇÐ¡ÓÚ1.5Ã×£¬ÏòÖ÷½Ç¹¥»÷
			if (Vector3.Distance(m_transform.position, m_player. m_transfrom.position) <= 1.5f)
			{
				//Í£Ö¹Ñ°Â·	
				m_agent.ResetPath();
				// ½øÈë¹¥»÷×´Ì¬
				m_ani.SetBool("attack", true);
			}
		}
		
		// Èç¹û´¦ÓÚ¹¥»÷×´Ì¬
		if (stateInfo.nameHash == Animator.StringToHash("Base Layer.attack") && !m_ani.IsInTransition(0))
		{
			
			// ÃæÏòÖ÷½Ç
			RotateTo();
			
			m_ani.SetBool("attack", false);
			
			// Èç¹û¹¥»÷¶¯»­²¥Íê£¬ÖØÐÂ½øÈë´ý»ú×´Ì¬
			if (stateInfo.normalizedTime >= 1.0f)
			{
				m_ani.SetBool("idle", true);
				
				// ÖØÖÃ¼ÆÊ±Æ÷
				m_timer = 2;
				
//				m_player.OnDamage(1);
			}
		}
		
		// ËÀÍö
		if (stateInfo.nameHash == Animator.StringToHash("Base Layer.death") && !m_ani.IsInTransition(0))
		{
			if (stateInfo.normalizedTime >= 1.0f)
			{
//				OnDeath();
				
			}
		}
		
		
	}


	void MoveTo()
	{
		float speed=m_moveSpeed*Time.deltaTime;
		m_agent.Move(m_transform.TransformDirection(new Vector3(0,0,speed)));
		//把自身的方向坐标。转换为世界坐标。每折移动一个Vector3 
		// m_agent.Move(new Vector3(0,0,0));
	}


	void RotateTo()
	{
		Vector3 oldangle=m_transform.eulerAngles;
		m_transform.LookAt(m_player.m_transfrom);
		float target=m_transform.eulerAngles.y;

		float speed=m_rotspeed*Time.deltaTime;
		float angle= Mathf.MoveTowardsAngle(oldangle.y,target,speed);
		m_transform.eulerAngles=new Vector3(0,angle,0);

	}
}

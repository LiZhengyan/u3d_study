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
	float m_timer=2; //休息时间
	protected EnemySpawn m_spawn; //刷怪实例 在init里面被初始化

	//让spawn 把ins的enemy通过 getcomponent获得对象上的enemy实例，
	//然后把自身实例传过去，让enemy来累加spawn的敌人数目
	//
	public void Init(EnemySpawn spawn){
		m_spawn=spawn;
		m_spawn.m_enemyCount+=1;
	}
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
		//如果在待机状态，。而且不是在过渡阶段
		if (stateInfo.nameHash == Animator.StringToHash("Base Layer.idle") && !m_ani.IsInTransition(0))
		{
			m_ani.SetBool("idle", false);
			//设置为false以后。如果没有其他动作被true，那么最后执行的动作会一直执行（设loop了的）
			
			// 站一会别动
			m_timer -= Time.deltaTime;
			if (m_timer > 0)
				return;
			
			// 距离小于1.5执行攻击
			if (Vector3.Distance(m_transform.position, m_player.m_transfrom.position) < 1.5f)
			{
				m_ani.SetBool("attack", true);
			}
			else
			{
				//等1秒以后，再次待机
				m_timer=1;
				
				//设定路线
				m_agent.SetDestination(m_player. m_transfrom.position);
				
				//开始跑
				m_ani.SetBool("run", true);
			}
		}
		
		// 在跑动过程中
		if (stateInfo.nameHash == Animator.StringToHash("Base Layer.run") && !m_ani.IsInTransition(0))
		{
			
			m_ani.SetBool("run", false);
			//顺手设置为false，不影响跑动
			
			
			// 每隔1秒重新定位玩家位置
			m_timer -= Time.deltaTime;
			if (m_timer < 0)
			{
				m_agent.SetDestination(m_player. m_transfrom.position);
				
				m_timer = 1;
			}
			
			// 追玩家
			MoveTo();
			
			// 距离1.5 开始攻击
			if (Vector3.Distance(m_transform.position, m_player. m_transfrom.position) <= 1.5f)
			{
				//停止寻路
				m_agent.ResetPath();
				// 进入攻击状态
				m_ani.SetBool("attack", true);
			}
		}
		
		// 攻击状态
		if (stateInfo.nameHash == Animator.StringToHash("Base Layer.attack") && !m_ani.IsInTransition(0))
		{
			
			//面向玩家
			RotateTo();
			
			m_ani.SetBool("attack", false);
			
			// 动画播完。重新进入待机
			if (stateInfo.normalizedTime >= 1.0f)
			{
				m_ani.SetBool("idle", true);
				
				// 重置时间
				m_timer = 2;
				
				m_player.OnDamage(1);
			}
		}
		
		//挂掉
		if (stateInfo.nameHash == Animator.StringToHash("Base Layer.death") && !m_ani.IsInTransition(0))
		{
			if (stateInfo.normalizedTime >= 1.0f)
			{
				OnDeath();
				
			}
		}
		
		
	}

	void OnDeath(){
		m_spawn.m_enemyCount-=1;
		GameManager.Instance.SetScore(100);
		Destroy(this.gameObject);
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
	public void OnDamage(int damage){
		m_life-=damage;
		if(m_life<=0){
			m_ani.SetBool("death",true);
			m_agent.ResetPath();
		}
	}
}

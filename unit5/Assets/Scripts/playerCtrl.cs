using UnityEngine;
using System.Collections;

public class playerCtrl : MonoBehaviour {
	public Transform m_transfrom;  //自身的transform
	CharacterController m_ch;  //自身角色控制器
	float m_moveSpeed=3.0f;  //移动速度
	float m_gravity=2.0f;	// 重力数值。就是朝下运动到
	
	public int m_life=5;

	//摄像机
	Transform m_camTransfrom; //摄像机
	Vector3 m_camRot;    //摄像机角度
	float m_camHeight=0.4f;  //摄像机高度
	Transform m_muzzlepoint;  //枪口
	public LayerMask m_layer; //射线射到的碰撞层
	public Transform m_fx;  //碰撞以后的特效
	public AudioClip m_audio;  //射击音效
	private float m_shootTime=0; //射击间隔时间
	// Use this for initialization
	void Start () {
		m_transfrom=this.transform;
		m_ch=this.GetComponent<CharacterController>();
		m_camTransfrom=Camera.main.transform;
		Vector3 pos=m_transfrom.position;  //获取自身postion

		m_muzzlepoint=m_camTransfrom.FindChild("M16/weapon/muzzlepoint").transform;


		pos.y+=m_camHeight; //摄像机高度
		m_camTransfrom.position=pos;  //设置摄像机位置

		m_camTransfrom.rotation=m_transfrom.rotation;  //x设置摄像机旋转角度和玩家一致  
		m_camRot=m_camTransfrom.eulerAngles;
		Screen.lockCursor=true;
	}
	
	// Update is called once per frame
	void Update () {
		Control();
		m_shootTime-=Time.deltaTime;  //原来是0 。减去以后为负数
		if(Input.GetMouseButton(0) && m_shootTime<=0){
			m_shootTime=0.1f; //恢复时间
			this.audio.PlayOneShot(m_audio);   //播放声音
			GameManager.Instance.SetAmmo(1);   //减少子弹
			RaycastHit info;     //保存射线结果
			bool hit=Physics.Raycast(m_muzzlepoint.position,m_camTransfrom.TransformDirection(Vector3.forward),
									out info,100,m_layer);
									//m_muzzlepoint 枪头
									//以摄像机做参照物超前方向 转换为欧啦角 世界欧拉角
									//info射击结果，
									//距离
									//层
			Debug.Log("发射子弹");
			if(hit){
				Debug.Log("射中了");
				if(info.transform.tag.CompareTo("enemy") == 0){
					Debug.Log("射中了。。。。。");
					Enemy enemy_shot= info.transform.GetComponent<Enemy>();
					enemy_shot.OnDamage(3);
				}
				Instantiate(m_fx,info.point,info.transform.rotation);
			}
		}


	}

	//动作控制
	void Control()
	{
		float xm=0,ym=0,zm=0;
		ym-=m_gravity* Time.deltaTime;


		float rh=Input.GetAxis("Mouse X");
		float rv=Input.GetAxis("Mouse Y");

		m_camRot.x-=rv;
		m_camRot.y+=rh;
		m_camTransfrom.eulerAngles=m_camRot; //世界欧拉角

		//让主角方向和摄像机一致

		m_transfrom.eulerAngles=new Vector3(0,m_camRot.y,0); //此处和网上说的不一致。网上说的是z,x,y
		//操作主角移动
		if(Input.GetKey(KeyCode.W))
		{
			zm+=m_moveSpeed*Time.deltaTime;
		}else if(Input.GetKey(KeyCode.S))
		{
			zm-=m_moveSpeed*Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.A))
		{
			xm-=m_moveSpeed*Time.deltaTime;
		}else if(Input.GetKey(KeyCode.D))
		{
			
			xm+=m_moveSpeed*Time.deltaTime;
		}


		m_camTransfrom.position=new Vector3(m_transfrom.position.x,m_transfrom.position.y+m_camHeight,
			m_transfrom.position.z);
		m_ch.Move(m_transfrom.TransformDirection(new Vector3(xm,ym,zm)));
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawIcon(this.transform.position,"item.png");
	}
	public void OnDamage(int damage){
		m_life-=damage;
		GameManager.Instance.SetLife(m_life);
		if(m_life<=0){
			Screen.lockCursor=false;
		}
	}
}

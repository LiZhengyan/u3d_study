using UnityEngine;
using System.Collections;

public class playerCtrl : MonoBehaviour {
	public Transform m_transfrom;
	CharacterController m_ch;
	float m_moveSpeed=3.0f;
	float m_gravity=2.0f;
	
	public int m_life=5;

	//摄像机
	Transform m_camTransfrom;
	Vector3 m_camRot;
	float m_camHeight=0.4f;


	// Use this for initialization
	void Start () {
		m_transfrom=this.transform;
		m_ch=this.GetComponent<CharacterController>();
		m_camTransfrom=Camera.main.transform;
		Vector3 pos=m_transfrom.position;  //获取自身postion

		pos.y+=m_camHeight; //摄像机高度
		m_camTransfrom.position=pos;  //设置摄像机位置

		m_camTransfrom.rotation=m_transfrom.rotation;  //x设置摄像机旋转角度和玩家一致  
		m_camRot=m_camTransfrom.eulerAngles;
		Screen.lockCursor=true;
	}
	
	// Update is called once per frame
	void Update () {
		Control();
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
		m_camTransfrom.eulerAngles=m_camRot;

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
}

using UnityEngine;
using System.Collections;

public class RoteToABC : MonoBehaviour {

	public float target = 270.0F;
	public float speed = 45.0F;
	private Transform m_transform;
	private Vector3 new_rotetion;


	void Start()
	{
		m_transform=this.transform;
		new_rotetion = Camera.main.transform.eulerAngles;
		Debug.Log(new_rotetion.y);
	}
	void Update() {

		Vector3 old_rotetion = this.transform.eulerAngles;

		float angle = Mathf.MoveTowardsAngle(old_rotetion.y, new_rotetion.y, speed * Time.deltaTime);
		m_transform.eulerAngles = new Vector3(0, angle, 0);
		Debug.Log(angle+"=="+speed * Time.deltaTime+"["+old_rotetion.y+"<>"+new_rotetion.y+"]");
		// m_transform.LookAt(Camera.main.transform);

		

	}
}

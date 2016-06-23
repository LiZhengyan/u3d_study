using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {
	public Transform m_transform;
	public Vector3 camerRel;

	// Use this for initialization
	void Start () {

		m_transform=this.transform;
		camerRel=m_transform.TransformDirection(0,1,0);
		Debug.Log(camerRel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void movotoo()
	{
	}

}

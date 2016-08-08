using UnityEngine;
using System.Collections;

public class myYield : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("start1");
		StartCoroutine(Test());
		Debug.Log("start2");
		Debug.Log("start3");
		Debug.Log("start4");
		Debug.Log("start5");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Test()
	{
		Debug.Log("test1 in Test");
		// yield return null;
		yield return new WaitForSeconds(5);
		Debug.Log("test2 in Test");
	}
}

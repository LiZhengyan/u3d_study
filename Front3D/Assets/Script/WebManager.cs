using UnityEngine;
using System.Collections;

public class WebManager : MonoBehaviour
{
    private string wwwurl = "https://www.baidu.com/index.html";

    // Use this for initialization
    void Start()
    {
        StartCoroutine(IGetData());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // IEnumerator:
    IEnumerator IGetData()
    {
        WWW www = new WWW(wwwurl);
        yield return www;

        if (www.error != null)
        {
            // var m_info = www.error;
			Debug.Log(www.error);
            yield return null;
        }
        Debug.Log(www.text);
    }
}

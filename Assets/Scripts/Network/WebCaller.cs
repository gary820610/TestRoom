using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebCaller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Call()
    {
        StartCoroutine(SendMsg());

    }

    public void PrintData(string[] data)
    {
        foreach (string s in data)
        {
            Debug.Log(s);
        }
    }

    IEnumerator SendMsg()
    {
        WWWForm form = new WWWForm();
        form.AddField("Account", "Asato");
        form.AddField("Password", "qwerty");

        UserInfo user = new UserInfo { Account = "Asato", Password = "qwerty" };
        string f = JsonUtility.ToJson(user);
        UnityWebRequest www = UnityWebRequest.Post("https://localhost:44316/api/values", form);
        // www.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else if (www.downloadHandler.text != null)
        {
            Debug.Log(www.downloadHandler.text);
        }
        else Debug.Log("***No Data Received***");
    }
}

public class UserInfo
{
    public string Account;
    public string Password;
}

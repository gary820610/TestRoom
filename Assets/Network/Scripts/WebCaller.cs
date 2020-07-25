using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebCaller : MonoBehaviour
{
    [SerializeField]
    NetworkUIMode _UIMode;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CallServer()
    {
        StartCoroutine(GetMsg());
    }

    public void PostData()
    {
        StartCoroutine(SendMsg(_UIMode.GetInputText("UserAccount"), _UIMode.GetInputText("Password")));

    }

    public void PrintData(string[] data)
    {
        foreach (string s in data)
        {
            Debug.Log(s);
        }
    }

    IEnumerator GetMsg()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://localhost:44316/api/values");
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

    IEnumerator SendMsg(string userAccount, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("Account", userAccount);
        form.AddField("Password", password);

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

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

    public void Register()
    {
        string userName = _UIMode.GetInputText("UserAccount");
        string password = _UIMode.GetInputText("Password");
        if (userName == "" || password == "")
        {
            Debug.LogWarning("帳戶名稱或密碼不可留白");
            return;
        }
        StartCoroutine(SendMsg(userName, password, "https://localhost:44316/api/user/signup", SignUp));
    }

    public void CallServer()
    {
        StartCoroutine(GetMsg("https://localhost:44316/api/user/hello"));
    }

    public void PostData()
    {
        // StartCoroutine(SendMsg(_UIMode.GetInputText("UserAccount"), _UIMode.GetInputText("Password"), "https://localhost:44316/api/values"));
    }

    public void PrintData(string[] data)
    {
        foreach (string s in data)
        {
            Debug.Log(s);
        }
    }

    IEnumerator GetMsg(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
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

    IEnumerator SendMsg(string userAccount, string password, string url, System.Action<string> action)
    {
        WWWForm form = new WWWForm();
        form.AddField("Account", userAccount);
        form.AddField("Password", password);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        // www.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else if (www.downloadHandler.text != null)
        {
            Debug.Log(www.downloadHandler.text);
            action(www.downloadHandler.text);
        }
        else Debug.Log("***No Data Received***");
    }

    void SignUp(string name)
    {
        Debug.Log(name + " sign up successed");
    }
}



public class UserInfo
{
    public string Account;
    public string Password;
}

public class User
{
    public int ID { get; set; }
    public string Account { get; set; }
    public string Password { get; set; }
    public string Mail { get; set; }
}

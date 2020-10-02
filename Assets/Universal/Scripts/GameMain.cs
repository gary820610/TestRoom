using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public UserData User { get; internal set; }

    void Awake()
    {
        InitTest();
        LoadSceneMaster();
    }

    void Update()
    {

    }

    void InitTest()
    {
        TextAsset userData = Resources.Load<TextAsset>("TextAssets/UserDataSample");
        User = JsonConvert.DeserializeObject<UserData>(userData.text);
        User.Init();
    }

    void LoadSceneMaster()
    {

    }
}

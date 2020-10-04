using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class AssetsLoader
{
    static public GameObject LoadPrefab(string prefabPath)
    {
        string path = "Prefabs/" + prefabPath;
        GameObject go = Resources.Load<GameObject>(path);
        return go;
    }

    static public T[] LoadTable<T>(string jsonPath)
    {
        TextAsset table = Resources.Load<TextAsset>("TextAssets/" + jsonPath);
        JArray array = (JArray)JsonConvert.DeserializeObject(table.text);
        T[] indexer = new T[array.Count];
        for (int i = 0; i < array.Count; i++)
        {
            indexer[i] = JsonConvert.DeserializeObject<T>(array[i].ToString());
        }
        return indexer;
    }

    static public T LoadData<T>(string jsonPath)
    {
        TextAsset data = Resources.Load<TextAsset>("TextAssets/" + jsonPath);
        T obj = JsonConvert.DeserializeObject<T>(data.text);
        return obj;
    }

    static public T[] DeserializeJsonArr<T>(string json)
    {
        JArray array = (JArray)JsonConvert.DeserializeObject(json);
        T[] objs = new T[array.Count];
        for (int i = 0; i < array.Count; i++)
        {
            objs[i] = JsonConvert.DeserializeObject<T>(array[i].ToString());
            Debug.Log(objs[i]);
        }
        return objs;
    }
}

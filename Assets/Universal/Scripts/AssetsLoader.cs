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

    static public T[] LoadDataTable<T>(string jsonPath)
    {
        TextAsset table = Resources.Load<TextAsset>("TextAssets/" + jsonPath);
        JArray array = (JArray)JsonConvert.DeserializeObject(table.text);
        T[] indexer = new T[array.Count];
        for (int i = 0; i < array.Count; i++)
        {
            indexer[i] = JsonConvert.DeserializeObject<T>(array[i].ToString());
            Debug.Log(indexer[i]);
        }
        return indexer;
    }
}

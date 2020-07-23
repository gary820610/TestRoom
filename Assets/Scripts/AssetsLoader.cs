using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsLoader
{
    static public GameObject LoadPrefab(string prefabName)
    {
        string path = "Prefabs/" + prefabName;
        GameObject go = Resources.Load<GameObject>(path);
        return go;
    }
}

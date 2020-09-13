using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsLoader
{
    static public GameObject LoadPrefab(string prefabPath)
    {
        string path = "Prefabs/" + prefabPath;
        GameObject go = Resources.Load<GameObject>(path);
        return go;
    }
}

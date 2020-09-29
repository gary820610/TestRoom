using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class RuneIndex
{
    static public Rune[] ProtoRunes { get; set; }
    static public void Init()
    {
        AssetsLoader.LoadDataTable<Rune>(ProtoRunes, "RuneIndex");
    }
}

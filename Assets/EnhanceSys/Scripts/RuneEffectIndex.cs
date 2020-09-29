using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class RuneEffectIndex
{
    static public ShipGear[] ShipRuneEffects { get; set; }
    static public void Init()
    {
        AssetsLoader.LoadDataTable<ShipGear>(ShipRuneEffects, "RuneEffectIndex");
    }
}
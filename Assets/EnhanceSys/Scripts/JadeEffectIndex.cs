using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class JadeEffectIndex
{
    static public ShipGear[] SGJadeEffects { get; set; }
    static public void Init()
    {
        AssetsLoader.LoadDataTable<ShipGear>(SGJadeEffects, "SGJadeEffects");
    }
}

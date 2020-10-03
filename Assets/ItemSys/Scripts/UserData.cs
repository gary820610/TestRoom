using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class UserData
{
    [JsonProperty]
    public string GUID { get; internal set; }

    [JsonProperty]
    public Bag MyBag { get; internal set; }

    [JsonProperty]
    ShipDataTransmit[] MyShipData { get; set; }

    public List<ShipData> MyShips { get; internal set; }

    public void Init()
    {
        MyBag.Init();
        MyShips = new List<ShipData>();
        foreach (ShipDataTransmit tData in MyShipData)
        {
            MyShips.Add(ShipDataHelper.DeserializeShipData(MyBag, tData));
        }
    }
}

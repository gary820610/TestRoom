using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class ShipDataHelper {

    public static ShipGear CreateNewGear (string gearID) {
        ShipGear shipData = new ShipGear ();
        return shipData;
    }

    public static ShipData JsonToData (string json) {
        ShipDataTransmit data = JsonConvert.DeserializeObject<ShipDataTransmit> (json);
        return new ShipData (data);
    }

    public static string DataToJson (ShipData ship) {
        ShipDataTransmit data = new ShipDataTransmit ();
        data.UID = ship.UID;
        data.Name = ship.ShipName;
        data.Gears = ship.Gears.ToArray ();
        return JsonConvert.SerializeObject (data);
    }
}
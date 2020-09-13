using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class ShipDataHelper {
    public static ShipGear GenerateDataTest (ShipType shipType) {
        ShipGear shipData = new ShipGear ();
        switch (shipType) {
            case ShipType.S:
                shipData.maxArmour = 1000;
                shipData.crewNum = 20;
                shipData.cannonNum = 1;
                shipData.maxSpeed = 3;
                shipData.acceleration = 1f;
                shipData.turningSpeed = 75;
                shipData.cannonAtk = 500;
                shipData.strikeAtk = 20;
                shipData.morale = 100;
                shipData.cannonCapacity = shipData.cannonNum * 10;
                break;
            case ShipType.M:
                shipData.maxArmour = 2000;
                shipData.crewNum = 100;
                shipData.cannonNum = 2;
                shipData.maxSpeed = 5;
                shipData.acceleration = 1;
                shipData.turningSpeed = 25;
                shipData.cannonAtk = 500;
                shipData.strikeAtk = 20;
                shipData.morale = 100;
                shipData.cannonCapacity = shipData.cannonNum * 10;
                break;
            case ShipType.L:
                shipData.maxArmour = 3000;
                shipData.crewNum = 500;
                shipData.cannonNum = 3;
                shipData.maxSpeed = 5;
                shipData.acceleration = 10;
                shipData.turningSpeed = 25;
                shipData.cannonAtk = 500;
                shipData.strikeAtk = 20;
                shipData.morale = 100;
                shipData.cannonCapacity = shipData.cannonNum * 10;
                break;
            default:
                shipData.maxArmour = 0;
                shipData.crewNum = 0;
                shipData.cannonNum = 0;
                shipData.maxSpeed = 0;
                shipData.acceleration = 0;
                shipData.turningSpeed = 0;
                shipData.cannonAtk = 0;
                shipData.strikeAtk = 0;
                shipData.morale = 0;
                shipData.cannonCapacity = 0;
                break;
        }
        return shipData;
    }

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

public enum ShipType {
    S,
    M,
    L,
    GEAR
}
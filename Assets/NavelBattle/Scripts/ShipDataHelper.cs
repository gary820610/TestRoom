using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

static public class ShipDataHelper
{
    static public ShipGear CreateNewGear(string gearID)
    {
        ShipGear shipData = new ShipGear();
        return shipData;
    }

    static public ShipData JsonToData(string json)
    {
        ShipDataTransmit data = JsonConvert.DeserializeObject<ShipDataTransmit>(json);
        return new ShipData(data);
    }

    static public string DataToJson(ShipData ship)
    {
        ShipDataTransmit data = new ShipDataTransmit();
        data.UID = ship.UID;
        data.Name = ship.ShipName;
        data.Gears = ship.Gears.ToArray();
        return JsonConvert.SerializeObject(data);
    }

    static public void PlusGearStates(ref ShipGear gearA, ShipGear gearB)
    {
        gearA.MaxArmour += gearB.MaxArmour;
        gearA.MaxSpeed += gearB.MaxSpeed;
        gearA.Acceleration += gearB.Acceleration;
        gearA.TurningSpeed += gearB.TurningSpeed;
        gearA.CannonAtk += gearB.CannonAtk;
        gearA.StrikeAtk += gearB.StrikeAtk;
        gearA.CannonCapacity += gearB.CannonCapacity;
        gearA.MaxMorale += gearB.MaxMorale;
        gearA.CannonNum += gearB.CannonNum;
        gearA.MaxCrewNum += gearB.MaxCrewNum;
    }
}
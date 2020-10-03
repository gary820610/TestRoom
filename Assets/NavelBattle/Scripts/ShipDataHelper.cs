using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

static public class ShipDataHelper
{
    static public ShipData JsonToData(string json)
    {
        return JsonConvert.DeserializeObject<ShipData>(json);
    }

    static public ShipData DeserializeShipData(Bag bag, ShipDataTransmit data)
    {
        List<ShipGear> gears = new List<ShipGear>();
        foreach (string gearUID in data.GearUIDs)
        {
            gears.Add(bag.GetShipGear(gearUID));
        }
        ShipData ship = new ShipData();
        ship.DataInit(data, gears);
        return ship;
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
        gearA.WeaponNum += gearB.WeaponNum;
        gearA.MaxCrewNum += gearB.MaxCrewNum;
    }
}
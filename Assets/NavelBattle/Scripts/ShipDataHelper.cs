using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShipDataHelper
{
    public static ShipData GenerateDataTest(ShipType shipType)
    {
        ShipData shipData = new ShipData();
        switch (shipType)
        {
            case ShipType.S:
                shipData.maxArmour = 1000;
                shipData.crewNum = 20;
                shipData.cannonNum = 1;
                shipData.maxSpeed = 10f;
                shipData.acceleration = 3;
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

    public static ShipData GenerateData(string gearID)
    {
        ShipData shipData = new ShipData();

        return shipData;
    }

    public static void AddGear(ref ShipData ship, ShipData gear)
    {
        ship.maxArmour += gear.maxArmour;
        ship.maxSpeed += gear.maxSpeed;
        ship.turningSpeed += gear.turningSpeed;
        ship.acceleration += gear.acceleration;
        ship.cannonAtk += gear.cannonAtk;
        ship.cannonCapacity += gear.cannonCapacity;
        ship.crewNum += gear.crewNum;
        ship.morale += gear.morale;
        ship.strikeAtk += gear.strikeAtk;
    }
}

public enum ShipType
{
    S,
    M,
    L,
    GEAR
}

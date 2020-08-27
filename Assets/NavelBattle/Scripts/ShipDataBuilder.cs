using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDataBuilder
{
    public static ShipData GenerateData(ShipType shipType)
    {
        ShipData shipData = new ShipData();
        shipData.shipType = (int)shipType;
        switch (shipType)
        {
            case ShipType.S:
                shipData.maxArmour = 1000;
                shipData.crewNum = 20;
                shipData.cannonNum = 1;
                break;
            case ShipType.M:
                shipData.maxArmour = 2000;
                shipData.crewNum = 100;
                shipData.cannonNum = 2;
                break;
            case ShipType.L:
                shipData.maxArmour = 3000;
                shipData.crewNum = 500;
                shipData.cannonNum = 3;
                break;
            default:
                shipData.maxArmour = -1;
                shipData.crewNum = -1;
                shipData.cannonNum = -1;
                break;
        }
        shipData.maxSpeed = 1;
        shipData.acceleration = 1;
        shipData.turningSpeed = 1;
        shipData.cannonAtk = 500;
        shipData.strikeAtk = 20;
        shipData.morale = 100;
        shipData.cannonCapacity = shipData.cannonNum * 10;
        return shipData;
    }
}

public enum ShipType
{
    S,
    M,
    L
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class RuneEnhanceHelper
{


    /// <summary>
    /// MethodID : 101
    /// </summary>
    /// <param name="gear">Gear which needed enhance</param>
    /// <param name="value">Enhanced value</param>
    static public void IncShipArmour(ShipGear gear, float value)
    {
        gear.MaxArmour += value;
    }

    /// <summary>
    /// MethodID : 102
    /// </summary>
    /// <param name="gear"></param>
    /// <param name="value"></param>
    static public void IncShipSpeed(ShipGear gear, float value)
    {
        gear.MaxSpeed += value;
    }
}

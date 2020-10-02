using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData
{
    public int UID { get; internal set; }
    public string ShipName { get; internal set; }
    public string ModelName { get; internal set; }
    public ShipGear State { get; internal set; }
    public List<ShipGear> Gears { get; internal set; }

    public ShipData(ShipDataTransmit dataTransmit, List<ShipGear> gears)
    {
        State = new ShipGear();
        Gears = gears;
        UID = dataTransmit.UID;
        ShipName = dataTransmit.Name;
    }

    public void ModifyGears(List<ShipGear> gears)
    {
        Gears = gears;
        RefreshData(Gears);
    }

    public bool AddGear(ShipGear gear)
    {
        if (Gears.Count >= 10) return false;
        else
        {
            Gears.Add(gear);
            return true;
        }
    }

    void RefreshData(IEnumerable<ShipGear> newGears)
    {
        /*釋放記憶體? 不確定是否多餘*/
        State = null;

        State = new ShipGear();
        foreach (ShipGear gear in newGears)
        {
            State.MaxArmour += gear.MaxArmour;
            State.MaxSpeed += gear.MaxSpeed;
            State.TurningSpeed += gear.TurningSpeed;
            State.Acceleration += gear.Acceleration;
            State.CannonAtk += gear.CannonAtk;
            State.CannonCapacity += gear.CannonCapacity;
            State.CannonNum += gear.CannonNum;
            State.StrikeAtk += gear.StrikeAtk;
            State.MaxMorale += gear.MaxMorale;
            State.MaxCrewNum += gear.MaxCrewNum;
        }
    }
}
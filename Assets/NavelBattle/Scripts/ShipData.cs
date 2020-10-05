using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class ShipData
{
    [JsonProperty]
    public string GUID { get; internal set; }
    [JsonProperty]
    public string ShipName { get; internal set; }
    [JsonProperty]
    public string ModelName { get; internal set; }
    [JsonProperty]
    public ShipGear State { get; internal set; }
    public List<ShipGear> Gears { get; internal set; }

    public ShipData()
    {
        GUID = System.Guid.NewGuid().ToString();
        ShipName = "新船艦";
        ModelName = "default_ship";
    }

    public void DataInit(ShipDataTransmit dataTransmit, List<ShipGear> gears)
    {
        Gears = gears;
        GUID = dataTransmit.GUID;
        ShipName = dataTransmit.Name;
        ModelName = gears.Where(g => g.EnhType == EnhanceType.ShipBody).ToArray()[0].Model;
        RefreshData(Gears);
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
            State.WeaponNum += gear.WeaponNum;
            State.StrikeAtk += gear.StrikeAtk;
            State.MaxMorale += gear.MaxMorale;
            State.MaxCrewNum += gear.MaxCrewNum;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData
{
    public int UID { get => _uid; }
    public string ShipName { get => _shipName; }
    public string ModelName { get => _shipModel; }
    public ShipGear State { get => _myShipStat; }
    public List<ShipGear> Gears { get => _gears; }

    int _uid;
    string _shipName;
    string _shipType;
    string _shipModel;
    ShipGear _myShipStat;
    List<ShipGear> _gears;

    public ShipData(ShipDataTransmit data)
    {
        _myShipStat = new ShipGear();
        _gears = new List<ShipGear>();

        _uid = data.UID;
        _shipName = data.Name;
        foreach (ShipGear gear in data.Gears)
        {
            if (gear.Model != "null") _shipModel = gear.Model;
            _myShipStat.MaxArmour += gear.MaxArmour;
            _myShipStat.MaxSpeed += gear.MaxSpeed;
            _myShipStat.TurningSpeed += gear.TurningSpeed;
            _myShipStat.Acceleration += gear.Acceleration;
            _myShipStat.CannonAtk += gear.CannonAtk;
            _myShipStat.CannonCapacity += gear.CannonCapacity;
            _myShipStat.CannonNum += gear.CannonNum;
            _myShipStat.StrikeAtk += gear.StrikeAtk;
            _myShipStat.MaxMorale += gear.MaxMorale;
            _myShipStat.MaxCrewNum += gear.MaxCrewNum;
            _gears.Add(gear);
        }
    }

    public void ModifyGears(List<ShipGear> gears)
    {
        _gears = gears;
        RefreshData(_gears);
    }

    public bool AddGear(ShipGear gear)
    {
        if (_gears.Count >= 10) return false;
        else
        {
            _gears.Add(gear);
            return true;
        }
    }

    void RefreshData(IEnumerable<ShipGear> newGears)
    {
        /*釋放記憶體? 不確定是否多餘*/
        _myShipStat = null;

        _myShipStat = new ShipGear();
        foreach (ShipGear gear in newGears)
        {
            _myShipStat.MaxArmour += gear.MaxArmour;
            _myShipStat.MaxSpeed += gear.MaxSpeed;
            _myShipStat.TurningSpeed += gear.TurningSpeed;
            _myShipStat.Acceleration += gear.Acceleration;
            _myShipStat.CannonAtk += gear.CannonAtk;
            _myShipStat.CannonCapacity += gear.CannonCapacity;
            _myShipStat.CannonNum += gear.CannonNum;
            _myShipStat.StrikeAtk += gear.StrikeAtk;
            _myShipStat.MaxMorale += gear.MaxMorale;
            _myShipStat.MaxCrewNum += gear.MaxCrewNum;
        }
    }
}
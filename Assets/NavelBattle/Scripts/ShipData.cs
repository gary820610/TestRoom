using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData {
    public int UID { get { return _uid; } }
    public string ShipName { get { return _shipName; } }
    public string ModelName { get { return _shipModel; } }
    public ShipGear State { get { return _myShipStat; } }
    public List<ShipGear> Gears { get { return _gears; } }

    int _uid;
    string _shipName;
    string _shipType;
    string _shipModel;
    ShipGear _myShipStat;
    List<ShipGear> _gears;

    public ShipData (ShipDataTransmit data) {
        _myShipStat = new ShipGear ();
        _gears = new List<ShipGear> ();

        _uid = data.UID;
        _shipName = data.Name;
        foreach (ShipGear gear in data.Gears) {
            if (gear.model != "null") _shipModel = gear.model;
            _myShipStat.maxArmour += gear.maxArmour;
            _myShipStat.maxSpeed += gear.maxSpeed;
            _myShipStat.turningSpeed += gear.turningSpeed;
            _myShipStat.acceleration += gear.acceleration;
            _myShipStat.cannonAtk += gear.cannonAtk;
            _myShipStat.cannonCapacity += gear.cannonCapacity;
            _myShipStat.cannonNum += gear.cannonNum;
            _myShipStat.strikeAtk += gear.strikeAtk;
            _myShipStat.morale += gear.morale;
            _myShipStat.crewNum += gear.crewNum;
            _gears.Add (gear);
        }
    }

    public void ModifyGears (List<ShipGear> gears) {
        _gears = gears;
        RefreshData (_gears);
    }

    public bool AddGear (ShipGear gear) {
        if (_gears.Count >= 10) return false;
        else {
            _gears.Add (gear);
            return true;
        }
    }

    void RefreshData (IEnumerable<ShipGear> newGears) {
        /*釋放記憶體? 不確定是否多餘*/
        _myShipStat = null;

        _myShipStat = new ShipGear ();
        foreach (ShipGear gear in newGears) {
            _myShipStat.maxArmour += gear.maxArmour;
            _myShipStat.maxSpeed += gear.maxSpeed;
            _myShipStat.turningSpeed += gear.turningSpeed;
            _myShipStat.acceleration += gear.acceleration;
            _myShipStat.cannonAtk += gear.cannonAtk;
            _myShipStat.cannonCapacity += gear.cannonCapacity;
            _myShipStat.cannonNum += gear.cannonNum;
            _myShipStat.strikeAtk += gear.strikeAtk;
            _myShipStat.morale += gear.morale;
            _myShipStat.crewNum += gear.crewNum;
        }
    }
}
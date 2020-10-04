using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnhanceIndexer
{
    static List<ShipGear> _jadeEffectsSG;
    static List<ShipGear> _shipRuneEffects;

    static public void Init()
    {
        _shipRuneEffects = AssetsLoader.LoadTable<ShipGear>("RuneEffectIndex").ToList();
        _jadeEffectsSG = AssetsLoader.LoadTable<ShipGear>("SGJadeEffects").ToList();
    }

    static public ShipGear GetShipRuneEff(int effectID)
    {
        return _shipRuneEffects.Where(eff => eff.EnhanceID == effectID).ToArray()[0];
    }

    static public ShipGear GetShipJadeEff(EnhanceType type, int effectID)
    {
        return _jadeEffectsSG.Where(effect => (int)effect.EnhType == (int)type).Where(effect => effect.EnhanceID == effectID).ToArray()[0];
    }
}

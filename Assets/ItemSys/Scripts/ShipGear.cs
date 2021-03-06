﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

public class ShipGear : IItem, IEnhanceable
{
    [JsonProperty]
    public int ItemID { get; internal set; }
    [JsonProperty]
    public ItemType ItemType { get; internal set; }
    [JsonProperty]
    public int Rarity { get; internal set; }
    [JsonProperty]
    public int Amount { get => Amount; set => Mathf.Clamp01(value); }
    [JsonProperty]
    public string Name { get; internal set; }
    [JsonProperty]
    public string Desc { get; internal set; }
    public Texture2D ItemIcon { get; internal set; }

    [JsonProperty]
    public int Exp { get; internal set; }
    [JsonProperty]
    public int Lv { get; internal set; }
    [JsonProperty]
    public EnhanceType EnhType { get; internal set; }
    public bool IsEnhanceable { get => this.Lv >= 10 ? false : true; }
    [JsonProperty]
    public int EnhanceID { get; internal set; }
    [JsonProperty]
    public JadeEnhanceState JadeEnhStat { get; internal set; }

    /// <summary>
    /// A unique id of this particular gear.
    /// </summary>
    [JsonProperty]
    public string GearUID { get; internal set; }
    [JsonProperty]
    public string Model { get; internal set; }
    [JsonProperty]
    public float MaxArmour { get; internal set; }
    [JsonProperty]
    public float MaxSpeed { get; internal set; }
    [JsonProperty]
    public float Acceleration { get; internal set; }
    [JsonProperty]
    public float TurningSpeed { get; internal set; }
    [JsonProperty]
    public float CannonAtk { get; internal set; }
    [JsonProperty]
    public float StrikeAtk { get; internal set; }
    [JsonProperty]
    public float MaxMorale { get; internal set; }
    [JsonProperty]
    public int WeaponNum { get; internal set; }
    [JsonProperty]
    public int CannonCapacity { get; internal set; }
    [JsonProperty]
    public int MaxCrewNum { get; internal set; }

    public event Bag.DecHandler OnDecrease;
    public event EnhanceMaster.RemoveHandler OnRemove;

    public void PlusGearProps(ShipGear gear)
    {
        Debug.Log("強化前能力 : ");
        Debug.Log("Armour === " + MaxArmour);
        Debug.Log("MaxSpeed === " + MaxSpeed);
        Debug.Log("Acceleration === " + Acceleration);
        Debug.Log("TurningSpeed === " + TurningSpeed);
        Debug.Log("CannonAtk === " + CannonAtk);
        this.MaxArmour += gear.MaxArmour;
        this.MaxSpeed += gear.MaxSpeed;
        this.Acceleration += gear.Acceleration;
        this.TurningSpeed += gear.TurningSpeed;
        this.CannonAtk += gear.CannonAtk;
        this.StrikeAtk += gear.StrikeAtk;
        this.CannonCapacity += gear.CannonCapacity;
        this.MaxMorale += gear.MaxMorale;
        this.WeaponNum += gear.WeaponNum;
        this.MaxCrewNum += gear.MaxCrewNum;
        Debug.Log("強化後能力 : ");
        Debug.Log("Armour === " + MaxArmour);
        Debug.Log("MaxSpeed === " + MaxSpeed);
        Debug.Log("Acceleration === " + Acceleration);
        Debug.Log("TurningSpeed === " + TurningSpeed);
        Debug.Log("CannonAtk === " + CannonAtk);
    }

    public void EnhanceBy(IEnhancer enhancer)
    {
        if (enhancer.EnhType != EnhanceType.Jade)
        {
            PlusGearProps(EnhanceIndexer.GetShipRuneEff(enhancer.EffectID));
        }
        else
        {
            PlusGearProps(EnhanceIndexer.GetShipJadeEff(this.EnhType, enhancer.EffectID));
        }
    }

    public void LevelUp()
    {
        Lv++;
        Debug.Log("升級!!! 當前等級 === " + Lv);
    }

    public void ResetExp()
    {
        Exp = 0;
        JadeEnhStat = JadeEnhanceState.Free;
    }

    public void IncExp(int exp)
    {
        Exp += exp;
    }

    public void SetEnhState(JadeEnhanceState state)
    {
        JadeEnhStat = state;
    }

    public void Increase(int amount)
    {
        Debug.Log("ShipGear.Increase hadn't finished yet");
    }

    public void Decrease(int amount)
    {
        OnDecrease(this);
        OnRemove();
    }

    public void Init()
    {
        Debug.Log("ShipGear.Init hadn't finished yet");
    }
}
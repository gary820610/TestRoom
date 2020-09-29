using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipGear : IItem, IEnhanceable
{
    public int ItemID { get; set; }
    public int ItemType { get; set; }
    public int Rarity { get; set; }
    public int Amount { get { return this.Amount; } set { if (value > 0) { value = 1; }; } }
    public string Name { get; set; }
    public string Desc { get; set; }
    public Texture2D ItemIcon { get; set; }


    public int Exp { get; set; }
    public int Lv { get; set; }
    public EnhanceType EnhType { get; set; }
    public bool IsEnhanceable { get { if (this.Lv >= 10) return false; else return true; } }

    /// <summary>
    /// Don't use this property if this object is not an enhancer. Normal ship gear don't need and won't have this id.
    /// </summary>
    /// <value></value>
    public int EnhanceID { get; set; }

    public int GearID { get; set; }
    public string Model { get; set; }
    public float MaxArmour { get; set; }
    public float MaxSpeed { get; set; }
    public float Acceleration { get; set; }
    public float TurningSpeed { get; set; }
    public float CannonAtk { get; set; }
    public float StrikeAtk { get; set; }
    public float Morale { get; set; }
    public int CannonNum { get; set; }
    public int CannonCapacity { get; set; }
    public int CrewNum { get; set; }


    public void PlusGearProps(ShipGear gear)
    {
        this.MaxArmour += gear.MaxArmour;
        this.MaxSpeed += gear.MaxSpeed;
        this.Acceleration += gear.Acceleration;
        this.TurningSpeed += gear.TurningSpeed;
        this.CannonAtk += gear.CannonAtk;
        this.StrikeAtk += gear.StrikeAtk;
        this.CannonCapacity += gear.CannonCapacity;
        this.Morale += gear.Morale;
        this.CannonNum += gear.CannonNum;
        this.CrewNum += gear.CrewNum;
    }

    public void Enhance(IEnhancer enhancer)
    {
        if (enhancer.EnhType != EnhanceType.Jade)
        {
            PlusGearProps(RuneEffectIndex.ShipRuneEffects.Where(rune => rune.EnhanceID == enhancer.EffectID).First());
        }
        else
        {
            PlusGearProps(JadeEffectIndex.SGJadeEffects.Where(effect => effect.EnhType == this.EnhType).Where(effect => effect.EnhanceID == enhancer.EffectID).First());
        }
    }
}
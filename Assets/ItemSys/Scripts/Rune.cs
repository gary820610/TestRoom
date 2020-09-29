using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : IItem, IEnhancer
{
    public int ItemID { get; set; }
    public int ItemType { get; set; }
    public int Rarity { get; set; }
    public int Amount { get { return this.Amount; } set { if (value > 0) { value = 1; }; } }
    public string Name { get; set; }
    public string Desc { get; set; }
    public Texture2D ItemIcon { get; set; }

    public EnhanceType EnhType { get; set; }
    public int EnhanceRate { get; set; }
    public int EffectID { get; set; }

    public int KaiGuangTimes { get; set; }

    public void KaiGuang()
    {

    }
}
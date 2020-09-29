using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jade : IItem, IEnhancer
{
    public int ItemID { get; set; }
    public int ItemType { get; set; }
    public int Rarity { get; set; }
    public int Amount { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public Texture2D ItemIcon { get; set; }

    public EnhanceType EnhType { get { return (int)EnhanceType.Jade; } set { } }
    public int EnhanceRate { get { return 100; } set { } }
    public int EffectID { get { return Rarity * 100; } set { } }
}

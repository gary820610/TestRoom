using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


public class Jade : IItem, IEnhancer
{
    [JsonProperty]
    public int ItemID { get; internal set; }
    [JsonProperty]
    public ItemType ItemType { get; internal set; }
    [JsonProperty]
    public int Rarity { get; internal set; }
    [JsonProperty]
    public int Amount { get; set; }
    [JsonProperty]
    public string Name { get; internal set; }
    [JsonProperty]
    public string Desc { get; internal set; }
    public Texture2D ItemIcon { get; internal set; }

    public EnhanceType EnhType { get => (int)EnhanceType.Jade; }
    public int EnhanceRate { get => 100; }
    public int EffectID { get => Rarity * 100; }

    public event Bag.DecHandler OnDecrease;
    public event EnhanceMaster.RemoveHandler OnRemove;

    public void Decrease(int amount)
    {
        Amount -= amount;
        if (Amount <= 0)
        {
            OnDecrease(this);
            OnRemove();
        }
    }

    public void Increase(int amount)
    {
        Amount += amount;
    }

    public void Init()
    {

    }
}

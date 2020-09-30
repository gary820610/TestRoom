using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Rune : IItem, IEnhancer
{
    [JsonProperty]
    public int ItemID { get; internal set; }
    [JsonProperty]
    public ItemType ItemType { get; internal set; }
    [JsonProperty]
    public int Rarity { get; internal set; }
    public int Amount { get => 1; }
    [JsonProperty]
    public string Name { get; internal set; }
    [JsonProperty]
    public string Desc { get; internal set; }
    public Texture2D ItemIcon { get; set; }

    [JsonProperty]
    public EnhanceType EnhType { get; internal set; }
    [JsonProperty]
    public int EnhanceRate { get; internal set; }
    [JsonProperty]
    public int EffectID { get; internal set; }

    /// <summary>
    /// A unique id of this particular rune.
    /// </summary>
    [JsonProperty]
    public int RuneUID { get; internal set; }
    [JsonProperty]
    public int KaiGuangTimes { get; internal set; }

    public void KaiGuang()
    {
        KaiGuangTimes++;

    }    

    public void Init()
    {
        ItemIndexer.GetRuneData(ItemID);
    }

    public void Increase(int amount)
    {
        throw new System.NotImplementedException();
    }

    public void Decrease(int amount)
    {
        throw new System.NotImplementedException();
    }
}
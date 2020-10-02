using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class Bag
{
    public List<IItem> MyItems { get; internal set; }
    public List<ShipGear> MyShipGears { get; internal set; }
    public List<Rune> MyRunes { get; internal set; }
    public List<Jade> MyJades { get; internal set; }

    [JsonProperty]
    ShipGear[] _shipGearsData;
    [JsonProperty]
    Rune[] _runesData;
    [JsonProperty]
    Jade[] _jadesData;

    public void Init()
    {
        this.MyShipGears = _shipGearsData.ToList();
        this.MyRunes = _runesData.ToList();
        this.MyJades = _jadesData.ToList();
        MyItems.AddRange(MyShipGears);
        MyItems.AddRange(MyRunes);
        MyItems.AddRange(MyJades);
    }

    public void AddItem(int itemID, int amount)
    {
        IItem item = MyItems.Find(i => i.ItemID == itemID);

        if ((int)item.ItemType < 10)
        {
            item.Init();
            MyItems.Add(item);
        }
        else
        {
            if (item is null)
            {
                item = ItemIndexer.GetItemData(itemID);
                item.Increase(amount);
                MyItems.Add(item);
            }
            else
            {
                item.Increase(amount);
            }
        }
    }

    public IItem GetItem(int itemID)
    {
        return MyItems.Find(i => i.ItemID == itemID);
    }

    public ShipGear GetShipGear(int gearUID)
    {
        return MyShipGears.Find(g => g.GearUID == gearUID);
    }
}


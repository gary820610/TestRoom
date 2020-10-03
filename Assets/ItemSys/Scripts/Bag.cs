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
    ShipGear[] ShipGearsData { get; set; }
    [JsonProperty]
    Rune[] RunesData { get; set; }
    [JsonProperty]
    Jade[] JadesData { get; set; }

    public void Init()
    {
        MyItems = new List<IItem>();
        this.MyShipGears = ShipGearsData.ToList();
        this.MyRunes = RunesData.ToList();
        this.MyJades = JadesData.ToList();
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

    public ShipGear GetShipGear(string gearUID)
    {
        return MyShipGears.Find(g => g.GearUID == gearUID);
    }
}


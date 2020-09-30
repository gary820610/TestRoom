using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class Bag
{
    public ShipGear[] MyShipGears { get; set; }
    public Rune[] MyRunes { get; set; }
    public IItem[] Others { get; set; }

    List<IItem> MyItems { get; set; }

    public void AddItem(int itemID, int amount)
    {
        IItem item = MyItems.Find(i => i.ItemID == itemID);

        switch (item.ItemType)
        {
            case ItemType.Uncountable:
                item.Init();
                MyItems.Add(item);
                break;
            case ItemType.Countable:
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
                break;

        }


    }
}


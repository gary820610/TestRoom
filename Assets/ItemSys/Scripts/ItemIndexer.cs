using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemIndexer
{
    static List<IItem> _itemList;
    static List<Rune> _protoRunes;
    static public void Init()
    {
        _protoRunes = AssetsLoader.LoadDataTable<Rune>("RuneIndex").ToList();
    }

    static public IItem GetItemData(int itemID)
    {
        return _itemList.Find(item => item.ItemID == itemID);
    }

    static public Rune GetRuneData(int itemID)
    {
        return _protoRunes.Find(item => item.ItemID == itemID);
    }
}

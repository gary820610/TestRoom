using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    /// <summary>
    /// This identify number of a particular KIND of item.
    /// </summary>
    int ItemID { get; }

    /// <summary>
    /// Define the item-family(gear, enhancer, or other) which this item belongs.
    /// </summary>
    ItemType ItemType { get; }

    /// <summary>
    /// From 1 to 5.
    /// </summary>
    int Rarity { get; }

    int Amount { get;}

    string Name { get; }

    /// <summary>
    /// The text description of this item.
    /// </summary>
    string Desc { get; }

    Texture2D ItemIcon { get; }

    void Init();
    void Increase(int amount);
    void Decrease(int amount);

}
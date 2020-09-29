using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    int ItemID { get; set; }
    int ItemType { get; set; }
    int Rarity { get; set; }
    int Amount { get; set; }
    string Name { get; set; }
    string Desc { get; set; }
    Texture2D ItemIcon { get; set; }
}
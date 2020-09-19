using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag
{
    Dictionary<string, Dictionary<int, IItem>> Items;
    public Bag()
    {
        Items.Add("ShipGears",new Dictionary<int, IItem>());
        Items.Add("Enhancers",new Dictionary<int, IItem>());
    }
}

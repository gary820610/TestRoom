using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag
{
    public List<IItem> MyItems { get; set; }
    public ShipGear[] MyShipGears { get; set; }
    public Rune[] MyRunes { get; set; }

    public IItem[] Others { get; set; }
}


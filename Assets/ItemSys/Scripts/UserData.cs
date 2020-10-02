using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public Bag MyBag { get; set; }
    public ShipDataTransmit[] MyShips { get; set; }

    public void Init()
    {
        MyBag.Init();
    }
}

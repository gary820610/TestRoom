using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGear : IItem, IEnhanceable
{
    [SerializeField]
    public int UID;
    [SerializeField]
    public string name;
    [SerializeField]
    public string gearType;
    [SerializeField]
    public string model;
    [SerializeField]
    public float maxArmour;
    [SerializeField]
    public float maxSpeed;
    [SerializeField]
    public float acceleration;
    [SerializeField]
    public float turningSpeed;
    [SerializeField]
    public float cannonAtk;
    [SerializeField]
    public float strikeAtk;
    [SerializeField]
    public float morale;
    [SerializeField]
    public int cannonNum;
    [SerializeField]
    public int cannonCapacity;
    [SerializeField]
    public int crewNum;

    public int Rarity { get; set; }
    public int Amount { get; set; }
    public int Exp { get; set; }
    public int Level { get; set; }

    public void Enhance(IEnhancer enhancer)
    {
        throw new System.NotImplementedException();
    }
}
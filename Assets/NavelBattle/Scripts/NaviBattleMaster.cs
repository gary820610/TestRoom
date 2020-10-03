using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviBattleMaster : MonoBehaviour
{
    GameMain _main;

    void Start()
    {
        _main = GameObject.FindWithTag("GameMain").GetComponent<GameMain>();
        InitScene(_main.User);
    }

    public void InitScene(UserData player)
    {
        ShipData data = player.MyShips[0];
        string modelPath = "Ships/" + data.ModelName;
        GameObject shipModel = GameObject.Instantiate(AssetsLoader.LoadPrefab(modelPath), this.transform);
        Ship playerShip = shipModel.AddComponent<Ship>();
        playerShip.Init(data);
        shipModel.AddComponent<PlayerShipController>();

        //set tag
        shipModel.tag = "Player";

        //set collider
        SetCollider(shipModel);

        ShowShipStats(playerShip);

    }

    void ShowShipStats(Ship ship)
    {
        ShipGear data = ship.ShipData;
        Debug.Log("ShipID === " + data.GearUID);
        Debug.Log("name === " + data.Name);
        Debug.Log("maxArmour === " + data.MaxArmour);
        Debug.Log("maxSpeed === " + data.MaxSpeed);
        Debug.Log("acceleration === " + data.Acceleration);
        Debug.Log("cannonAtk === " + data.CannonAtk);
        Debug.Log("cannonCapacity === " + data.CannonCapacity);
        Debug.Log("turningSpeed === " + data.TurningSpeed);
        Debug.Log("strikeAtk === " + data.StrikeAtk);
        Debug.Log("cannonNum === " + data.WeaponNum);
        Debug.Log("crewNum === " + data.MaxCrewNum);
    }

    void SetCollider(GameObject ShipModel)
    {
        Collider ShipCollider;
        Rigidbody ShipRigidbody;
        //mesh collider?
        ShipModel.AddComponent<CapsuleCollider>();
        ShipModel.AddComponent<Rigidbody>();

        ShipCollider = ShipModel.GetComponent<CapsuleCollider>();
        ShipRigidbody = ShipModel.GetComponent<Rigidbody>();

        ShipCollider.isTrigger = true;
        ShipRigidbody.useGravity = false;
        ShipRigidbody.isKinematic = true;
    }
}
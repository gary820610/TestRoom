﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetting : MonoBehaviour
{
    /*[SerializeField]
    Transform spawn;*/
    [SerializeField]
    public float DistanceToClose;
    [SerializeField]
    public float DistanceToFlee;
    //public  PlayerShip;

    public static float Distance1;
    public static float Distance2;


    void Start()
    {
        InitTest();
    }

    void InitTest()
    {
        TextAsset EnemyJsonData = Resources.Load<TextAsset>("TextAssets/ShipDataSample");
        ShipData EnemyData = ShipDataHelper.JsonToData(EnemyJsonData.text);

        string EnemyModelPath = "Ships/" + EnemyData.ModelName;
        GameObject EnemyShipModel = GameObject.Instantiate(AssetsLoader.LoadPrefab(EnemyModelPath), this.transform);

        Ship EnemyShip = EnemyShipModel.AddComponent<Ship>();

        EnemyShip.Init(EnemyData);
        EnemyShip.ShipData.cannonNum = 1000;

        EnemyShipModel.AddComponent<EnemyShipController>();


        //set tag
        EnemyShipModel.tag = "Enemy";

        SetCollider(EnemyShipModel);

        Distance1 = DistanceToClose;
        Distance2 = DistanceToFlee;
        //ShowShipStats(EnemyShip);
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
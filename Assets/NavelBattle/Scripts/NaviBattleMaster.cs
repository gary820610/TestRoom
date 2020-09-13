using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviBattleMaster : MonoBehaviour {
    void Start () {
        InitTest ();
    }

    void InitTest () {
        TextAsset jsonData = Resources.Load<TextAsset> ("TextAssets/ShipDataSample");
        ShipData data = ShipDataHelper.JsonToData (jsonData.text);
        string modelPath = "Ships/" + data.ModelName;
        GameObject shipModel = GameObject.Instantiate (AssetsLoader.LoadPrefab (modelPath), this.transform);
        Ship playerShip = shipModel.AddComponent<Ship> ();
        playerShip.Init (data);
        shipModel.AddComponent<PlayerShipController> ();

        ShowShipStats (playerShip);
    }

    void ShowShipStats (Ship ship) {
        ShipGear data = ship.ShipData;
        Debug.Log ("ShipID === " + data.UID);
        Debug.Log ("name === " + data.name);
        Debug.Log ("maxArmour === " + data.maxArmour);
        Debug.Log ("maxSpeed === " + data.maxSpeed);
        Debug.Log ("acceleration === " + data.acceleration);
        Debug.Log ("cannonAtk === " + data.cannonAtk);
        Debug.Log ("cannonCapacity === " + data.cannonCapacity);
        Debug.Log ("turningSpeed === " + data.turningSpeed);
        Debug.Log ("strikeAtk === " + data.strikeAtk);
        Debug.Log ("cannonNum === " + data.cannonNum);
        Debug.Log ("crewNum === " + data.crewNum);
    }

}
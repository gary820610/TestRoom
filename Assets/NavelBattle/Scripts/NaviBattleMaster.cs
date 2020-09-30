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
        Debug.Log ("ShipID === " + data.GearUID);
        Debug.Log ("name === " + data.Name);
        Debug.Log ("maxArmour === " + data.MaxArmour);
        Debug.Log ("maxSpeed === " + data.MaxSpeed);
        Debug.Log ("acceleration === " + data.Acceleration);
        Debug.Log ("cannonAtk === " + data.CannonAtk);
        Debug.Log ("cannonCapacity === " + data.CannonCapacity);
        Debug.Log ("turningSpeed === " + data.TurningSpeed);
        Debug.Log ("strikeAtk === " + data.StrikeAtk);
        Debug.Log ("cannonNum === " + data.CannonNum);
        Debug.Log ("crewNum === " + data.MaxCrewNum);
    }

}
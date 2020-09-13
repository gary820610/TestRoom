﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipController : MonoBehaviour, IShipController {
    Ship _myShip;
    ParticleSystem _pressEffect;
    Vector3 _oriFingerPos;
    Vector3 _endFingerPos;
    Vector3 _fingerPosWorld;

    int _timer = 0;
    float _cdTimer = 2;
    int _maxFirePressTime = 300;
    float _minMoveSlideLength = 100;

    void Start () {
        _myShip = this.gameObject.GetComponent<Ship> ();

        GameObject pressFX = GameObject.Instantiate (AssetsLoader.LoadPrefab ("PressEffect"));
        _pressEffect = pressFX.GetComponent<ParticleSystem> ();
    }

    // Update is called once per frame
    void Update () {
        FireCD ();
        Move ();
    }

    public void Move () {
        if (Input.touchCount <= 0) return;

        switch (Input.GetTouch (0).phase) {
            case TouchPhase.Began:
                _oriFingerPos = new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 10f);
                Ray ray = Camera.main.ScreenPointToRay (_oriFingerPos);
                RaycastHit hit;
                Physics.Raycast (ray, out hit, 1000);
                if (hit.collider.gameObject.layer != LayerMask.NameToLayer ("Water")) {
                    Debug.Log ("Not a water");
                } else {
                    _fingerPosWorld = hit.point;
                }
                break;
            case TouchPhase.Ended:
                _endFingerPos = new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 10f);
                float dist = Vector3.Distance (_oriFingerPos, _endFingerPos);

                if (dist > _minMoveSlideLength) {
                    _oriFingerPos = Camera.main.ScreenToWorldPoint (_oriFingerPos);
                    _endFingerPos = Camera.main.ScreenToWorldPoint (_endFingerPos);
                    var forwardVec = (_endFingerPos - _oriFingerPos).normalized;
                    _myShip.MoveTo (forwardVec);
                    _myShip.StartMoving ();
                } else if (_timer < _maxFirePressTime) {
                    ray = Camera.main.ScreenPointToRay (_endFingerPos);
                    Physics.Raycast (ray, out hit, 1000);
                    if (hit.collider.gameObject.layer != LayerMask.NameToLayer ("Water")) {
                        Debug.Log ("Not a water");
                    } else {
                        Fire (hit.point);
                    }
                }
                _timer = 0;
                break;
        }
    }

    public void Fire (Vector3 target) {
        Debug.Log ("CD ++++++ " + _cdTimer);
        if (_cdTimer < 2) return;
        ShowEffect (target);
        _myShip.Fire (target);
        _cdTimer = 0;
    }

    void FireCD () {
        _cdTimer += Time.deltaTime;
    }

    void ShowEffect (Vector3 fingerPos) {
        fingerPos.y = _myShip.gameObject.transform.position.y;
        _pressEffect.gameObject.transform.position = fingerPos;
        _pressEffect.Play ();
    }
}
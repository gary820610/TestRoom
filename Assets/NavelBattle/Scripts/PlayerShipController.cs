using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipController : MonoBehaviour
{
    [SerializeField]
    ShipModel _playerShip;
    [SerializeField]
    ParticleSystem _pressEffect;
    Vector3 _oriFingerPos;
    Vector3 _endFingerPos;
    Vector3 _fingerPosWorld;

    int _timer;
    int _maxFirePressTime = 300;
    float _minMoveSlideLength = 100;

    // Start is called before the first frame update
    void Start()
    {
        //_playerShip = this.gameObject.GetComponent<ShipModel>();
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ControlShip();
    }

    void ControlShip()
    {
        if (Input.touchCount <= 0) return;

        switch (Input.GetTouch(0).phase)
        {
            case TouchPhase.Began:
                _oriFingerPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10f);
                Ray ray = Camera.main.ScreenPointToRay(_oriFingerPos);
                RaycastHit hit;
                Physics.Raycast(ray, out hit, 1000);
                if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Water"))
                {
                    Debug.Log("Not a water");
                }
                else
                {
                    _fingerPosWorld = hit.point;
                }
                //Vector3 fingerPos = Camera.main.ScreenToWorldPoint(_oriFingerPos);
                break;
            case TouchPhase.Ended:
                _endFingerPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10f);
                float dist = Vector3.Distance(_oriFingerPos, _endFingerPos);

                if (dist > _minMoveSlideLength)
                {
                    _oriFingerPos = Camera.main.ScreenToWorldPoint(_oriFingerPos);
                    _endFingerPos = Camera.main.ScreenToWorldPoint(_endFingerPos);
                    var forwardVec = (_endFingerPos - _oriFingerPos).normalized;
                    _playerShip.MoveTo(forwardVec);
                }
                else if (_timer < _maxFirePressTime)
                {
                    ray = Camera.main.ScreenPointToRay(_endFingerPos);
                    Physics.Raycast(ray, out hit, 1000);
                    if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Water"))
                    {
                        Debug.Log("Not a water");
                    }
                    else
                    {
                        Fire(hit.point);
                        ShowEffect(hit.point);
                    }
                }
                _timer = 0;
                break;
        }
    }

    void ShowEffect(Vector3 fingerPos)
    {
        fingerPos.y = _playerShip.gameObject.transform.position.y;
        _pressEffect.gameObject.transform.position = fingerPos;
        _pressEffect.Play();
    }

    void Fire(Vector3 target)
    {
        _playerShip.Fire(target);
        Debug.LogWarning("FIRE!!!!!!!!");
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

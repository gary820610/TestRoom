using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipController : MonoBehaviour
{
    [SerializeField]
    ShipModel _playerShip;
    [SerializeField]
    GameObject _pressEffect;
    Vector3 _oriFingerPos;
    Vector3 _endFingerPos;

    int _timer;
    int _maxFirePressTime = 30;
    float _minMoveSlideLength = 100;

    // Start is called before the first frame update
    void Start()
    {
        //_playerShip = this.gameObject.GetComponent<ShipModel>();
        _timer = 0;
        _pressEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ControlShip();
    }

    void ControlShip()
    {
        if (_playerShip.IsMoveing)
        {
            _playerShip.ChangeColor(true);
            return;
        }
        else _playerShip.ChangeColor(false);

        if (Input.touchCount <= 0) return;

        switch (Input.GetTouch(0).phase)
        {
            case TouchPhase.Began:
                _oriFingerPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 100f);
                Vector3 fingerPos = Camera.main.ScreenToWorldPoint(_oriFingerPos);
                break;
            case TouchPhase.Stationary:
                _timer += 1;
                if (_timer > 25)
                {
                    Vector3 fingerPosNow = Camera.main.ScreenToWorldPoint(_oriFingerPos);
                    ShowEffect(true, fingerPosNow);
                }
                break;
            case TouchPhase.Ended:
                _endFingerPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 100f);
                float dist = Vector3.Distance(_oriFingerPos, _endFingerPos);

                if (_timer < _maxFirePressTime)
                {
                    Vector3 targetPos = Camera.main.ScreenToWorldPoint(_endFingerPos);
                    Fire(targetPos);
                }
                else if (dist > _minMoveSlideLength)
                {
                    _oriFingerPos = Camera.main.ScreenToWorldPoint(_oriFingerPos);
                    _endFingerPos = Camera.main.ScreenToWorldPoint(_endFingerPos);
                    var forwardVec = (_endFingerPos - _oriFingerPos).normalized * _playerShip.GetShipLength();
                    _playerShip.MoveTo(forwardVec);
                }
                _timer = 0;
                ShowEffect(false, Vector3.zero);
                break;
        }
    }

    void ShowEffect(bool onOff, Vector3 fingerPos)
    {
        _pressEffect.SetActive(onOff);
        fingerPos.y = _playerShip.gameObject.transform.position.y;
        _pressEffect.transform.position = fingerPos;
    }

    void Fire(Vector3 target)
    {
        _playerShip.FireCannon(target);
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

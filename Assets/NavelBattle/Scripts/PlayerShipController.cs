using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipController : MonoBehaviour
{
    ShipModel _playerShip;
    Vector3 _oriFingerPos;
    Vector3 _endFingerPos;
    [SerializeField]
    Slider _powerBar;

    float _timer = 0;
    bool _isAiming = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerShip = this.gameObject.GetComponent<ShipModel>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlShip();
        Aim();
    }

    public void StartAiming()
    {
        _isAiming = true;
        _timer = 0;
    }

    public void FireLeftCannon()
    {
        _playerShip.FireCannon("L", _timer);
        _isAiming = false;
    }

    public void FireRightCannon()
    {
        _playerShip.FireCannon("R", _timer);
        _isAiming = false;
    }

    public float GetTimer()
    {
        return _timer;
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
                Ray ray = Camera.main.ScreenPointToRay(_oriFingerPos);
                RaycastHit hit;
                Physics.Raycast(ray, out hit, 1000);
                if (hit.collider == null || hit.collider.gameObject.tag != "Player")
                {
                    Debug.Log("No collider hit");
                    return;
                }
                else
                {
                    _playerShip.ShowEffect(true);
                }
                break;
            case TouchPhase.Moved:
                break;
            case TouchPhase.Ended:
                _endFingerPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 100f);
                _oriFingerPos = Camera.main.ScreenToWorldPoint(_oriFingerPos);
                _endFingerPos = Camera.main.ScreenToWorldPoint(_endFingerPos);
                var forwardVec = (_endFingerPos - _oriFingerPos).normalized * _playerShip.GetShipLength();
                _playerShip.MoveTo(forwardVec);
                _playerShip.ShowEffect(false);
                break;
        }
    }

    void Aim()
    {
        _powerBar.value = _timer;
        if (!_isAiming) return;

        if (_timer >= 1) _timer = 0;
        _timer += Time.deltaTime;
        _powerBar.value = _timer;
    }
}

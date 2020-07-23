using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviBattleController : MonoBehaviour
{
    [SerializeField]
    ShipModel _playerShip;
    Vector3 oriFingerPos;
    Vector3 endFingerPos;

    float _timer = 0;
    bool _isAiming = false;

    // Start is called before the first frame update
    void Start()
    {

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
                oriFingerPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 100f);
                Ray ray = Camera.main.ScreenPointToRay(oriFingerPos);
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
                endFingerPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 100f);
                oriFingerPos = Camera.main.ScreenToWorldPoint(oriFingerPos);
                endFingerPos = Camera.main.ScreenToWorldPoint(endFingerPos);
                var forwardVec = (endFingerPos - oriFingerPos).normalized * _playerShip.GetShipLength();
                _playerShip.MoveTo(forwardVec);
                _playerShip.ShowEffect(false);
                break;
        }
    }

    void Aim()
    {
        if (!_isAiming) return;

        float time = Time.deltaTime;
        if (_timer >= 2) time = Time.deltaTime * -1;
        else if (_timer <= 0) time = Time.deltaTime;
        _timer += time;
    }
}

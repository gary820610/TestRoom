﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonModel : MonoBehaviour
{
    [SerializeField]
    float _canonHeight;
    [SerializeField]
    float G;
    [SerializeField]
    float _speed = 10;
    float _verticalSpeed;
    float _timer;
    bool _isFired = false;
    Vector3 _target;
    Vector3 _oriPos;
    Quaternion _oriRot;

    // Start is called before the first frame update
    void Start()
    {
        _target = this.transform.position;
        _oriPos = this.transform.position;
        _oriRot = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFired == false) return;
        Move();
    }

    public void Fire(Vector3 target)
    {
        // float angle = Vector3.Angle(this.transform.forward, target);
        // this.transform.Rotate(0, angle, 0);
        this.transform.LookAt(target);
        _target = new Vector3(target.x, _canonHeight, target.z);
        Vector3 cannonball = this.transform.position;
        float distance = Vector3.Distance(this.transform.position, _target);
        float flyTime = distance / _speed;
        float riseTime = flyTime / 2;
        _verticalSpeed = G * riseTime;
        _timer = 0;
        _isFired = true;
    }

    void Move()
    {
        /*Arrived*/
        if (this.transform.position.y < _target.y - 7.5)
        {
            this.gameObject.SetActive(false);
            this.transform.position = _oriPos;
            this.transform.rotation = _oriRot;
            return;
        }

        _timer += Time.deltaTime;

        float test = _verticalSpeed - G * _timer;
        this.transform.Translate(this.transform.forward * Time.deltaTime * _speed, Space.World);
        this.transform.Translate(this.transform.up * test * Time.deltaTime, Space.World);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 from = this.transform.position;
        Vector3 to = from + this.transform.forward;
        Gizmos.DrawLine(from, to);
    }
}

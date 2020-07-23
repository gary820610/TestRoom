using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField]
    float _canonHeight;
    Vector3 _peak;
    [SerializeField]
    Vector3 target;
    Vector3 _target;
    float _speed = 10;
    float _verticalSpeed;
    float _timer;
    float G = 0.98f;
    bool _isFired = false;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Vector3.zero;
        _target = this.transform.position;
        _verticalSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFired == false) return;
        Move();
    }

    public void Fire()
    {
        float angle = Vector3.Angle(this.transform.forward, target);
        this.transform.Rotate(0, angle, 0);
        _target = new Vector3(target.x, _canonHeight, target.z);
        Vector3 cannonball = this.transform.position;
        _peak = (cannonball + _target) / 2;
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
        if (this.transform.position.y < target.y) return;

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

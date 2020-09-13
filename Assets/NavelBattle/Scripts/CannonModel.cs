using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonModel : MonoBehaviour {
    [SerializeField]
    float _canonHeight;
    [SerializeField]
    float G;
    [SerializeField]
    float _speed;
    float _verticalSpeed;
    float _timer;
    bool _isFired = false;
    Vector3 _target;
    Vector3 _oriPos;
    Quaternion _oriRot;

    float _atk;

    // Start is called before the first frame update
    void Start () {
        _target = this.transform.position;
        _oriPos = this.transform.position;
        _oriRot = this.transform.rotation;
        _atk = 0;
    }

    // Update is called once per frame
    void Update () {
        if (_isFired == false) return;
        Move ();
    }

    public void SetAtk (float atk) {
        _atk = atk;
    }

    public float GetAtk () {
        return _atk;
    }

    public void Fire (Vector3 target) {
        this.transform.LookAt (target);
        _target = new Vector3 (target.x, _canonHeight, target.z);
        Vector3 cannonball = this.transform.position;

        /* 0.95為動畫效果的誤差修正參數，因砲彈發射高度比隱沒點高，故以此方法計算時砲彈隱沒點會比目標落點略遠 */
        float distance = Vector3.Distance (this.transform.position, _target * 0.95f);
        float flyTime = distance / _speed;
        float riseTime = flyTime / 2;
        _verticalSpeed = G * riseTime;
        _timer = 0;
        _isFired = true;
    }

    void Move () {
        /*Arrived*/
        if (this.transform.position.y < _target.y - 20) {
            this.gameObject.SetActive (false);
            this.transform.position = _oriPos;
            this.transform.rotation = _oriRot;
            return;
        }

        _timer += Time.deltaTime;

        float test = _verticalSpeed - G * _timer;
        this.transform.Translate (this.transform.forward * Time.deltaTime * _speed, Space.World);
        this.transform.Translate (this.transform.up * test * Time.deltaTime, Space.World);
    }

    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Vector3 from = this.transform.position;
        Vector3 to = from + this.transform.forward;
        Gizmos.DrawLine (from, to);
    }
}
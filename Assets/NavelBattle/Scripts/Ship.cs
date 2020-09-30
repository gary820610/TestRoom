using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public ShipGear ShipData { get => _shipData; }
    ShipGear _shipData;
    NaviMapData _mapData;
    Collider _bodyCol;

    bool _isStart;
    float _speed;
    float _armour;
    Vector3 _moveDir;

    LinkedList<CannonModel> _cannons;
    LinkedListNode<CannonModel> cannonNumber;

    void Start()
    {
        _isStart = false;
    }

    void Update()
    {
        if (!_isStart) return;
        Move();
    }

    public void Init(ShipData data)
    {
        //讀取船艦面板
        _shipData = data.State;
        _shipData.GearUID = data.UID;
        _shipData.Name = data.ModelName;

        //初始化船艦物理數值
        _speed = 0;
        _armour = _shipData.MaxArmour;
        _moveDir = this.transform.forward;

        //初始化砲彈
        _cannons = new LinkedList<CannonModel>();
        LoadCannons(_shipData.CannonCapacity);

        //測量戰場邊界
        MeasureMapBorder();

        _isStart = true;
    }

    public void ChangeColor(bool tof)
    {
        if (tof)
        {
            this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

    public void MoveTo(Vector3 forwardVector)
    {
        forwardVector.y = 0;
        _moveDir = forwardVector;
    }

    public void Fire(Vector3 target)
    {
        Vector3 nowPos = this.gameObject.transform.position;
        CannonModel cannonBall = cannonNumber.Value;
        Vector3 oriPos = this.transform.position;
        cannonBall.gameObject.transform.position = oriPos;
        cannonBall.gameObject.SetActive(true);
        cannonBall.Fire(target);
        if (cannonNumber.Next == null) cannonNumber = _cannons.First;
        cannonNumber = cannonNumber.Next;

    }

    public void SetBorder(NaviMapData mapData)
    {
        _mapData = mapData;
    }

    public void StartMoving()
    {
        _isStart = true;
    }

    void Move()
    {
        Vector3 direction = Vector3.Normalize(_moveDir - this.transform.position);
        float angle = Vector3.Angle(this.transform.forward, _moveDir);
        if (Vector3.Cross(this.transform.forward, _moveDir).y < 0) angle *= -1;

        if (Mathf.Abs(angle) > 5)
        {
            this.transform.Rotate(0, Mathf.LerpAngle(0, _shipData.TurningSpeed * Time.deltaTime, Time.time) * angle / Mathf.Abs(angle), 0);
        }
        else if (Mathf.Abs(angle) > 1)
        {
            this.transform.Rotate(0, Mathf.LerpAngle(_shipData.TurningSpeed * Time.deltaTime, 0, Time.time) * angle / Mathf.Abs(angle), 0);
        }

        if (ReachBorder())
        {
            //停止時固定在1秒內停止
            _speed -= _speed * Time.deltaTime;
            _speed = Mathf.Clamp(_speed, 0, _shipData.MaxSpeed);
            this.transform.Translate(this.transform.forward * _speed * Time.deltaTime, Space.World);
        }
        else
        {
            //起步時依照加速能力加速至極限速度

            _speed += _shipData.Acceleration * Time.deltaTime;
            _speed = Mathf.Clamp(_speed, 0, _shipData.MaxSpeed);
            this.transform.Translate(this.transform.forward * _speed * Time.deltaTime, Space.World);
        }
    }

    bool ReachBorder()
    {
        Vector3 prob = this.transform.position + (this.transform.forward * 5);
        if (prob.x <= _mapData.LeftBorder || prob.x >= _mapData.RightBorder || prob.z <= _mapData.BotBorder || prob.z >= _mapData.TopBorder)
        {
            Debug.LogWarning("Hit the border!!! " + prob);
            return true;
        }
        else return false;
    }

    public void MeasureMapBorder()
    {
        Vector3 topR = Vector3.zero;
        Vector3 botL = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 10f));
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 1000);
        if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Water"))
        {
            Debug.Log("Out of BattleField");
        }
        else
        {
            topR = hit.point;
        }

        ray = Camera.main.ScreenPointToRay(new Vector3(0, 0, 10f));
        Physics.Raycast(ray, out hit, 1000);
        if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Water"))
        {
            Debug.Log("Out of BattleField");
        }
        else
        {
            botL = hit.point;
        }

        SetBorder(new NaviMapData(topR, botL));
    }

    void LoadCannons(int amount)
    {
        GameObject cannonModel = AssetsLoader.LoadPrefab("CannonBall");
        for (int i = 0; i < amount; i++)
        {
            GameObject instance = GameObject.Instantiate(cannonModel);
            instance.transform.position = this.gameObject.transform.position;
            CannonModel cannon = instance.GetComponent<CannonModel>();
            cannon.SetAtk(_shipData.CannonAtk);
            _cannons.AddFirst(cannon);
            instance.gameObject.SetActive(false);
        }
        cannonNumber = _cannons.First;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "cannon":
                Debug.Log("Get hit by a cannon!!!");
                break;
            case "shipBody":
                Debug.Log("Hit enemy ship!!!");
                break;
            case "shipHead":
                Debug.Log("Get hit by enemy ship!!!");
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 from = this.transform.position;
        Vector3 to = this.transform.position + (this.transform.forward * 10);
        Gizmos.DrawLine(from, to);
    }
}
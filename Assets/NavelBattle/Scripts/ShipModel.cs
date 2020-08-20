using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModel : MonoBehaviour
{
    public bool IsMoveing { get { return _isMoving; } }

    [SerializeField]
    GameObject _shipBody;
    [SerializeField]
    GameObject _leftCannon;
    [SerializeField]
    GameObject _rightCannon;
    [SerializeField]
    GameObject _particleSys;
    Vector3 _targetPos;
    [SerializeField]
    MovePlan _movingPlan;

    bool _isMoving = false;

    float _borderTop;
    float _borderBot;
    float _borderLeft;
    float _borderRight;


    LinkedList<CannonModel> _cannons;

    [SerializeField]
    float _maxFireRange;
    LinkedListNode<CannonModel> cannonNumber;



    // Start is called before the first frame update
    void Start()
    {
        _targetPos = this.transform.position;
        SetBorder();
        _cannons = new LinkedList<CannonModel>();
        LoadCannons(10);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    public float GetShipLength()
    {
        return _shipBody.transform.localScale.z * 5;
        // return 20f;
    }

    public void ChangeColor(bool tof)
    {
        if (tof)
        {
            _shipBody.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else _shipBody.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

    public void MoveTo(Vector3 forwardVector)
    {
        forwardVector.y = 0;
        _targetPos = this.transform.position + forwardVector;
        float tarX = Mathf.Clamp(_targetPos.x, _borderLeft, _borderRight);
        float tarZ = Mathf.Clamp(_targetPos.z, _borderBot, _borderTop);
        _targetPos = new Vector3(tarX, _targetPos.y, tarZ);
        _isMoving = true;
    }

    public void FireCannon(Vector3 target)
    {
        CannonModel cannonBall = cannonNumber.Value;
        cannonBall.gameObject.transform.position = this.transform.position;
        cannonBall.gameObject.SetActive(true);
        cannonBall.Fire(target);
        Debug.Log(target);
        if (cannonNumber.Next == null) cannonNumber = _cannons.First;
        cannonNumber = cannonNumber.Next;
    }

    void Move()
    {
        float distance = Vector3.Distance(this.transform.position, _targetPos);
        Vector3 direction = Vector3.Normalize(_targetPos - this.transform.position);
        float angle = Vector3.Angle(this.transform.forward, direction);
        if (Vector3.Cross(this.transform.forward, direction).y < 0) angle *= -1;

        switch (_movingPlan)
        {
            case MovePlan.PLAN_A:
                if (Mathf.Abs(angle) > 0.5)
                {
                    this.transform.Rotate(0, angle * 0.02f, 0);
                }
                else if (distance > 3)
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, _targetPos, 0.02f);
                }
                else _isMoving = false;
                break;
            case MovePlan.PLAN_B:
                if (Mathf.Abs(angle) > 0.5)
                {
                    this.transform.Rotate(0, angle * 0.02f, 0);
                }
                if (distance > 3)
                {
                    this.transform.Translate(this.transform.forward * Mathf.Lerp(0, 0.1f, 0.01f), Space.World);
                }
                else _isMoving = false;
                break;
        }
    }

    void SetBorder()
    {
        Vector3 topR = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 90f));
        Vector3 botL = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 90f));
        _borderTop = topR.z;
        _borderBot = botL.z;
        _borderLeft = botL.x;
        _borderRight = topR.x;
    }

    void LoadCannons(int amount)
    {
        GameObject cannonIdea = AssetsLoader.LoadPrefab("CannonBall");
        for (int i = 0; i < amount; i++)
        {
            GameObject instance = GameObject.Instantiate<GameObject>(cannonIdea);
            // instance.transform.position = new Vector3(300, 300, 300);
            instance.transform.position = _shipBody.transform.position;
            _cannons.AddFirst(instance.GetComponent<CannonModel>());
            instance.gameObject.SetActive(false);
        }
        cannonNumber = _cannons.First;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log("!!!!!! Player Get Hit !!!!!!");
        }
    }

}

enum MovePlan
{
    PLAN_A,
    PLAN_B
}

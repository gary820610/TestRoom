using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModel : MonoBehaviour, IShipModel
{

    ShipData _shipData;

    [SerializeField]
    GameObject _shipBody;
    [SerializeField]
    GameObject _cannonA;
    [SerializeField]
    GameObject _cannonB;
    [SerializeField]
    GameObject _cannonC;
    Vector3 _moveDir;

    float _borderTop;
    float _borderBot;
    float _borderLeft;
    float _borderRight;


    LinkedList<CannonModel> _cannons;

    [SerializeField]
    float _maxFireRange = 1000;
    LinkedListNode<CannonModel> cannonNumber;

    // Start is called before the first frame update
    void Start()
    {
        _moveDir = this.transform.forward;
        SetBorder();
        _cannons = new LinkedList<CannonModel>();
        //LoadCannons(_shipData.cannonCapacity);
        LoadCannons(10);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
        //_moveDir = this.transform.position + (forwardVector * 1000);
        _moveDir = forwardVector * 1000;
    }

    public void Fire(Vector3 target)
    {
        Vector3 nowPos = this.gameObject.transform.position;
        // if (Vector3.Distance(nowPos, target) > _maxFireRange)
        // {
        //     target = target.normalized * _maxFireRange;
        // }
        CannonModel cannonBall = cannonNumber.Value;
        Vector3 oriPos = this.transform.position;
        cannonBall.gameObject.transform.position = oriPos;
        cannonBall.gameObject.SetActive(true);
        cannonBall.Fire(target);
        Debug.Log(target);
        if (cannonNumber.Next == null) cannonNumber = _cannons.First;
        cannonNumber = cannonNumber.Next;
    }

    void Move()
    {
        float distance = Vector3.Distance(this.transform.position, this.transform.position + _moveDir);
        Vector3 direction = Vector3.Normalize(_moveDir - this.transform.position);
        float angle = Vector3.Angle(this.transform.forward, _moveDir);
        if (Vector3.Cross(this.transform.forward, _moveDir).y < 0) angle *= -1;

        if (Mathf.Abs(angle) > 10)
        {
            this.transform.Rotate(0, 10 * angle / Mathf.Abs(angle), 0);
        }

        if (distance > 3)
        {
            //this.transform.position += this.transform.forward * Time.deltaTime * 5;
            this.transform.Translate(this.transform.forward * Mathf.Lerp(0, 5f, 0.1f), Space.World);
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

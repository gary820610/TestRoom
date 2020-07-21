using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModel : MonoBehaviour
{
    [SerializeField]
    GameObject _shipPrefab;
    [SerializeField]
    GameObject _particleSys;
    Vector3 _targetPos;
    [SerializeField]
    MovePlan _movingPlan;



    // Start is called before the first frame update
    void Start()
    {
        ShowEffect(false);
        _targetPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public float GetShipLength()
    {
        // return _shipPrefab.transform.localScale.z;
        return 20f;
    }

    public void ShowEffect(bool state)
    {
        _particleSys.SetActive(state);
    }

    public void MoveTo(Vector3 forwardVector)
    {
        forwardVector.y = 0;
        _targetPos = this.transform.position + forwardVector;
    }

    public void Move()
    {
        float distance = Vector3.Distance(this.transform.position, _targetPos);
        Vector3 direction = Vector3.Normalize(_targetPos - this.transform.position);
        float angle = Vector3.Angle(this.transform.forward, direction);
        if (Vector3.Cross(this.transform.forward, direction).y < 0) angle *= -1;

        switch (_movingPlan)
        {
            case MovePlan.PLAN_A:
                if (Mathf.Abs(angle) > 0.2)
                {
                    this.transform.Rotate(0, angle * 0.02f, 0);
                }
                else if (distance > 3)
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, _targetPos, 0.01f);
                }
                break;
            case MovePlan.PLAN_B:
                if (Mathf.Abs(angle) > 0.2)
                {
                    this.transform.Rotate(0, angle * 0.02f, 0);
                }
                if (distance > 3)
                {
                    this.transform.position += this.transform.forward * 0.1f;
                }
                break;
        }

        if (Mathf.Abs(angle) > 0.1)
        {
            this.transform.Rotate(0, angle * 0.02f, 0);
        }
        else if (distance > 3)
        {

        }
    }
}

enum MovePlan
{
    PLAN_A,
    PLAN_B
}

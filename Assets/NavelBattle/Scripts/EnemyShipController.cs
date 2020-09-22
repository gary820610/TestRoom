using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShipController : MonoBehaviour, IShipController
{
    Ship _EnemyShip;
    
    /*ParticleSystem _pressEffect;
    Vector3 _oriFingerPos;
    Vector3 _endFingerPos;
    Vector3 _fingerPosWorld;*/

    //int _timer = 0;
    float DistanceToPlayer;

    float Distance1 = 7.0f;
    float Distance2 = 15.0f;

    float _cdTimer = 2;

    //int _maxFirePressTime = 300;
    //float _minMoveSlideLength = 100;

    Transform TargetShip;

    enum MoveStateType
    {
        Idle            = 0,
        StayAway        = 1,
        RushToPlayer    = 2
    }
    MoveStateType MoveState = MoveStateType.Idle;

    void Start()
    {
        TargetShip = GameObject.FindWithTag("Player").GetComponent<Transform>();

        this.transform.position = this.transform.position + new Vector3(0.0f, 0.0f, 15.0f);
        this.transform.LookAt(TargetShip);
        _EnemyShip = this.gameObject.GetComponent<Ship>();

        /*GameObject pressFX = GameObject.Instantiate(AssetsLoader.LoadPrefab("PressEffect"));
        _pressEffect = pressFX.GetComponent<ParticleSystem>();*/
    }

    // Update is called once per frame
    void Update()
    {
        FireCD();
        Move();
        Fire(TargetShip.position + new Vector3(0.0f, 0.5f, 0.0f));
    }

    //public void Move(ship ship)
    public void Move()
    {
        DistanceToPlayer = Vector3.Distance(this.transform.position, TargetShip.position);
        var ForwardVec= this.transform.position.normalized;

        if (DistanceToPlayer >= Distance2)
            MoveState = MoveStateType.RushToPlayer;
        else if (DistanceToPlayer >= Distance1 && DistanceToPlayer < Distance2)
            MoveState = MoveStateType.StayAway;
        else if (DistanceToPlayer < Distance1)
            MoveState = MoveStateType.RushToPlayer;


        
        switch (MoveState)
        {
            case MoveStateType.Idle:

                break;

            case MoveStateType.StayAway:
                ForwardVec = (this.transform.position - TargetShip.position).normalized;
                _EnemyShip.MoveTo(ForwardVec);
                _EnemyShip.StartMoving();
                
                break;

            case MoveStateType.RushToPlayer:
                ForwardVec = (TargetShip.position - this.transform.position).normalized;
                _EnemyShip.MoveTo(ForwardVec);
                _EnemyShip.StartMoving();

                break;

        }
    }

    public void Fire(Vector3 target)
    {
        //Debug.Log("CD ++++++ " + _cdTimer);
        if (_cdTimer < 2) return;
        //ShowEffect(target);
        _EnemyShip.Fire(target);
        _cdTimer = 0;
    }

    void FireCD()
    {
        _cdTimer += Time.deltaTime;
    }

}
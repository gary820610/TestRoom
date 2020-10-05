using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShipController : MonoBehaviour, IShipController
{

    Ship _EnemyShip;
    Ship PlayerShip;
    CannonModel PlayerCannon;

    float DistanceToPlayer;

    float Distance1;
    float Distance2;

    float _cdTimer = 2;

    Transform TargetShip;

    GameObject HP;
    GameObject HPBar;
    public Vector3 FaceToCameraDirection;
    float hpPersentage;
    float maxArmour;

    float HitBoard = 0.0f;

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
        PlayerShip = GameObject.FindWithTag("Player").GetComponent<Ship>();
        
        this.transform.position = this.transform.position + new Vector3(0.0f, 0.0f, 15.0f);
        this.transform.LookAt(TargetShip);
        _EnemyShip = this.gameObject.GetComponent<Ship>();

        SetHPBar();
        maxArmour = _EnemyShip.OnGetArmour();

        Distance1 = EnemySetting.Distance1;
        Distance2 = EnemySetting.Distance2;
    }

    // Update is called once per frame
    void Update()
    {
        if (_EnemyShip.OnGetArmour() > 0)
        {
            FireCD();
            Move();
            Fire(TargetShip.position + new Vector3(0.0f, 0.5f, 0.0f));
        }
        SetHPBarPosition();
    }

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

        if (_EnemyShip.ReachBorder())
            HitBoard = 1.5f;

        if (PlayerShip.OnGetArmour()<=0)
        {
            ForwardVec = (TargetShip.position - this.transform.position).normalized;
        }
        else
        {
            if (HitBoard > 0)
            {
                HitBoard = HitBoard - 0.01f;
                //ForwardVec = (Vector3.Scale(TargetShip.position - this.transform.position, new Vector3(-1f, 1f, 1f))).normalized;
                ForwardVec = (TargetShip.position - this.transform.position).normalized;
            }
            else
            {

                switch (MoveState)
                {
                    case MoveStateType.Idle:

                        break;

                    case MoveStateType.StayAway:
                        ForwardVec = (this.transform.position - TargetShip.position).normalized;
                        break;

                    case MoveStateType.RushToPlayer:
                        ForwardVec = (TargetShip.position - this.transform.position).normalized;
                        break;
                }

            }
        }
        _EnemyShip.MoveTo(ForwardVec);
        _EnemyShip.StartMoving();
        Debug.Log(" : " + PlayerShip.OnGetArmour());
    }

    public void Fire(Vector3 target)
    {
        if (_cdTimer < 4) return;
        _EnemyShip.Fire(target, "EnemyCannon");
        _cdTimer = 0;
    }

    void FireCD()
    {
        _cdTimer += Time.deltaTime;
    }

    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "PlayerCannon")
        {
            PlayerCannon = GameObject.FindWithTag("PlayerCannon").GetComponent<CannonModel>();
            OnHit(PlayerCannon);
        }
    }

    void OnTriggerStay(Collider Other)
    {
        if (Other.tag == "Player")
        {
            RaycastHit hit;
            Ray ray = new Ray(this.transform.position, this.transform.forward);
            bool isHit = Physics.Raycast(ray, out hit, 1f, 1 << 0, QueryTriggerInteraction.Collide);

            if (isHit)
            {
                print("Enemy hit Player");
                PauseGame();
            }
        }
    }




    void OnHit(CannonModel Cannon)
    {
        _EnemyShip.OnSetArmour(_EnemyShip.OnGetArmour() - Cannon.GetAtk());
        //Debug.Log("Enemy.OnGetArmour()2 : " + _EnemyShip.OnGetArmour());
        //Debug.Log("Cannon.GetAtk()2 : " + Cannon.GetAtk());

        if (_EnemyShip.OnGetArmour() < 0)
            _EnemyShip.OnSetArmour(0);

        hpPersentage = (_EnemyShip.OnGetArmour() / maxArmour);
        HP.transform.localScale = new Vector3(hpPersentage, 1, 1);

        /*if (_EnemyShip.OnGetArmour() <= 0)
            PauseGame();*/
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void SetHPBarPosition()
    {
        HPBar.transform.rotation = Quaternion.LookRotation(FaceToCameraDirection);
        HPBar.transform.position = this.transform.position + new Vector3(0, 0, -1);
    }

    void SetHPBar()
    {
        HPBar = GameObject.Instantiate(GameObject.Find("HPBar"));
        HP = HPBar.transform.Find("HP").gameObject;

        HPBar.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        FaceToCameraDirection = this.transform.position - GameObject.FindWithTag("MainCamera").transform.position;
        HPBar.transform.rotation = Quaternion.LookRotation(FaceToCameraDirection);
    }
}
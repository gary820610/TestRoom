using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    [SerializeField]
    GameObject m_Cam;
    [SerializeField]
    RectTransform _view;
    [SerializeField]
    RectTransform _pages;

    CameraController _camCon;
    int _viewNum;

    Vector2 _touchPointStart;
    Vector2 _touchPointEnd;
    float _moveSpeed = 0f;
    float _moveDist = 0;
    float _minSpeed = 0.01f;
    float _maxSpeed = 30.0f;
    float _validSpeed = 0.05f;
    float _validDist = 1.0f;

    Direction _dir;
    Vector2 _touchOffSet;


    // Start is called before the first frame update
    void Start()
    {
        Input.multiTouchEnabled = true;
        SortList();
        m_Cam = GameObject.FindGameObjectWithTag("MainCamera"); ;
        _camCon = m_Cam.GetComponent<CameraController>();
        _viewNum = 0;
        _dir = Direction.NODIRECTION;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount < 1)
        {
            return;
        }

        switch (Input.GetTouch(0).phase)
        {
            case TouchPhase.Began:
                _touchPointStart = Input.GetTouch(0).position;
                _touchOffSet = new Vector2(Screen.width / 2, Screen.height / 2) - _touchPointStart;
                break;
            case TouchPhase.Moved:
                var deltaPos = Input.GetTouch(0).deltaPosition;
                var newPos = new Vector2(Input.GetTouch(0).position.x - (Screen.width / 2), Input.GetTouch(0).position.y - (Screen.height / 2));
                var moveVec = deltaPos.y * _view.rect.height / Screen.height;
                var newPos2 = _pages.anchoredPosition + new Vector2(0, moveVec);
                _pages.anchoredPosition = newPos + _touchOffSet;

                Debug.Log("___delta : " + deltaPos);
                Debug.Log("newPos : " + newPos);
                Debug.Log("newPos2 : " + newPos2);

                // if (_dir == Direction.NODIRECTION)
                // {
                //     if (Mathf.Abs(deltaPos.x) >= Mathf.Abs(deltaPos.y))
                //     {
                //         _dir = Direction.HORIZONTAL;
                //         var moveVec = deltaPos.x * _view.rect.width / Screen.width;
                //         _pages.anchoredPosition += new Vector2(moveVec, 0);
                //     }
                //     else
                //     {
                //         _dir = Direction.VERTICAL;
                //         var moveVec = deltaPos.y * _view.rect.height / Screen.height;
                //         _totalDist += deltaPos.y;
                //         _pages.anchoredPosition += new Vector2(0, moveVec);
                //     }
                // }

                // if (_dir == Direction.HORIZONTAL)
                // {
                //     var moveVec = deltaPos.x * _view.rect.width / Screen.width;
                //     _pages.anchoredPosition += new Vector2(moveVec, 0);
                // }
                // else if (_dir == Direction.VERTICAL)
                // {
                //     var moveVec = deltaPos.y * _view.rect.height / Screen.height;                //

                //     Debug.Log("moveVec" + moveVec);
                //     Debug.Log("deltaPos.y" + deltaPos.y);
                //     _pages.anchoredPosition += new Vector2(0, moveVec);
                // }

                break;
            case TouchPhase.Ended:
                _touchPointEnd = Input.GetTouch(0).position;
                _moveDist = _touchPointEnd.y - _touchPointStart.y;
                _moveSpeed = Input.GetTouch(0).deltaPosition.x;
                // Debug.Log("_moveDist : " + _moveDist);
                // Debug.Log("_pages.anchoredPosition" + _pages.anchoredPosition);
                // if (Mathf.Abs(_moveSpeed) < _validSpeed)
                //     _moveSpeed = 0f;
                // if (_moveSpeed * _moveDist < 1)
                //     _moveSpeed = 0f;
                // if (Mathf.Abs(_moveDist) < _validDist)
                //     _moveSpeed = 0f;
                // Slide(_moveSpeed);
                _dir = Direction.NODIRECTION;
                _pages.anchoredPosition = new Vector2(0, 0);
                break;
        }
    }

    void SortList()
    {

    }

    void Slide(float moveSpeed)
    {

    }
}

enum Direction
{
    NODIRECTION,
    HORIZONTAL,
    VERTICAL
}

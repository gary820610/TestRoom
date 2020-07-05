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



    // Start is called before the first frame update
    void Start()
    {
        Input.multiTouchEnabled = true;
        SortList();
        m_Cam = GameObject.FindGameObjectWithTag("MainCamera"); ;
        _camCon = m_Cam.GetComponent<CameraController>();
        _viewNum = 0;
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
                break;
            case TouchPhase.Moved:
                var deltaPos = Input.GetTouch(0).deltaPosition;
                if (Mathf.Abs(deltaPos.x) >= Mathf.Abs(deltaPos.y))
                {
                    var moveVec = (deltaPos.x / Screen.width) * _view.rect.width;
                    _view.anchoredPosition += new Vector2(moveVec, 0);
                }
                else
                {
                    var moveVec = (deltaPos.y / Screen.height) * _view.rect.height;
                    _view.anchoredPosition += new Vector2(0, moveVec);
                }
                break;
            case TouchPhase.Ended:
                _touchPointEnd = Input.GetTouch(0).position;
                _moveDist = _touchPointEnd.x - _touchPointStart.x;
                _moveSpeed = Input.GetTouch(0).deltaPosition.x;

                // if (Mathf.Abs(_moveSpeed) < _validSpeed)
                //     _moveSpeed = 0f;
                // if (_moveSpeed * _moveDist < 1)
                //     _moveSpeed = 0f;
                // if (Mathf.Abs(_moveDist) < _validDist)
                //     _moveSpeed = 0f;
                // Slide(_moveSpeed);
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

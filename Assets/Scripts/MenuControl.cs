using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    [SerializeField]
    GameObject m_Cam;
    [SerializeField]
    UIModel _UIManager;

    CameraController _camCon;

    Vector2 _touchPointStart;
    Vector2 _touchPointEnd;
    float _moveSpeed = 0f;
    float _moveDist = 0;
    Direction _dir;
    // Vector2 _touchOffSet;


    // Start is called before the first frame update
    void Start()
    {
        Input.multiTouchEnabled = true;
        m_Cam = GameObject.FindGameObjectWithTag("MainCamera"); ;
        _camCon = m_Cam.GetComponent<CameraController>();
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
                // _touchOffSet = new Vector2(Screen.width / 2, Screen.height / 2) - _touchPointStart;
                break;
            case TouchPhase.Moved:
                var deltaPos = Input.GetTouch(0).deltaPosition;
                var fingerPos = Input.GetTouch(0).position;
                switch (_dir)
                {
                    case Direction.NODIRECTION:
                        if (Mathf.Abs(deltaPos.x) >= Mathf.Abs(deltaPos.y)) _dir = Direction.HORIZONTAL;
                        else _dir = Direction.VERTICAL;
                        break;
                    case Direction.HORIZONTAL:
                        fingerPos.y = _touchPointStart.y;
                        break;
                    case Direction.VERTICAL:
                        fingerPos.x = _touchPointStart.x;
                        break;
                }
                _UIManager.MovePage(_touchPointStart, fingerPos);
                break;

            // var newPos = new Vector2(Input.GetTouch(0).position.x - (Screen.width / 2), Input.GetTouch(0).position.y - (Screen.height / 2));
            // var moveVec = deltaPos.y * _view.rect.height / Screen.height;
            // var newPos2 = _pages.anchoredPosition + new Vector2(0, moveVec);
            // _pages.anchoredPosition = newPos + _touchOffSet;

            // Debug.Log("___delta : " + deltaPos);
            // Debug.Log("newPos : " + newPos);
            // Debug.Log("newPos2 : " + newPos2);

            case TouchPhase.Ended:
                _touchPointEnd = Input.GetTouch(0).position;
                switch (_dir)
                {
                    case Direction.NODIRECTION:
                        break;
                    case Direction.HORIZONTAL:
                        _moveDist = _touchPointEnd.x - _touchPointStart.x;
                        _UIManager.SlidePage(new Vector2(_moveDist, 0));
                        break;
                    case Direction.VERTICAL:
                        _moveDist = _touchPointEnd.y - _touchPointStart.y;
                        _UIManager.SlidePage(new Vector2(0, _moveDist));
                        break;
                }
                _dir = Direction.NODIRECTION;
                break;
        }
    }
}

enum Direction
{
    NODIRECTION,
    HORIZONTAL,
    VERTICAL
}

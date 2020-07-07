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
    float _moveSpeed = 0;
    float _moveDist = 0;
    eSlideDirection _dir;

    // Start is called before the first frame update
    void Start()
    {
        Input.multiTouchEnabled = true;
        // m_Cam = GameObject.FindGameObjectWithTag("MainCamera"); ;
        // _camCon = m_Cam.GetComponent<CameraController>();
        _dir = eSlideDirection.NODIRECTION;
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
                var fingerPos = Input.GetTouch(0).position;
                switch (_dir)
                {
                    case eSlideDirection.NODIRECTION:
                        if (Mathf.Abs(deltaPos.x) >= Mathf.Abs(deltaPos.y))
                            _dir = eSlideDirection.HORIZONTAL;
                        else _dir = eSlideDirection.VERTICAL;
                        break;
                    case eSlideDirection.HORIZONTAL:
                        fingerPos.y = _touchPointStart.y;
                        break;
                    case eSlideDirection.VERTICAL:
                        fingerPos.x = _touchPointStart.x;
                        break;
                }
                var fingerPosVP = Camera.main.ScreenToViewportPoint(fingerPos);
                var touchBeganVP = Camera.main.ScreenToViewportPoint(_touchPointStart);
                _UIManager.MovePage(touchBeganVP, fingerPosVP);
                break;

            case TouchPhase.Ended:
                _touchPointEnd = Input.GetTouch(0).position;
                switch (_dir)
                {
                    case eSlideDirection.NODIRECTION:
                        break;
                    case eSlideDirection.HORIZONTAL:
                        _moveDist = _touchPointEnd.x - _touchPointStart.x;
                        _UIManager.SlidePage(new Vector2(_moveDist, 0));
                        break;
                    case eSlideDirection.VERTICAL:
                        _moveDist = _touchPointEnd.y - _touchPointStart.y;
                        _UIManager.SlidePage(new Vector2(0, _moveDist));
                        break;
                }
                _dir = eSlideDirection.NODIRECTION;
                break;
        }
    }
}

enum eSlideDirection
{
    NODIRECTION,
    HORIZONTAL,
    VERTICAL
}

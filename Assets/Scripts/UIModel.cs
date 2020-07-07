using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModel : MonoBehaviour
{
    [SerializeField]
    PageModel _pages;
    Vector2 _screenCenter;
    float _validSlideSpeed;
    float _validSlideDist;
    float _realWidth;
    float _realHeight;

    // Start is called before the first frame update
    void Start()
    {
        _screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        _validSlideSpeed = Screen.width * 0.4f * Time.deltaTime;
        _validSlideDist = Screen.width * 0.4f;
        _realWidth = this.gameObject.GetComponent<RectTransform>().rect.width;
        _realHeight = this.gameObject.GetComponent<RectTransform>().rect.height;
    }

    public void MovePage(Vector2 fingerBegan, Vector2 fingerPos)
    {
        fingerBegan = new Vector2(fingerBegan.x * _realWidth, fingerBegan.y * _realHeight);
        fingerPos = new Vector2(fingerPos.x * _realWidth, fingerPos.y * _realHeight);
        Vector2 offSet = (_screenCenter - fingerBegan) + (_pages.NowPos - _pages.OriPos);
        fingerPos = new Vector2(fingerPos.x - _screenCenter.x, fingerPos.y - _screenCenter.y);
        _pages.Move2Finger(fingerPos, offSet);
    }

    public void SlidePage(Vector2 vector)
    {
        _pages.ResetOffSet();
        if (vector.x == 0)
        {
            if (vector.y <= _validSlideDist * -1)
            {
                _pages.Move2Bot();
            }
            else if (vector.y >= _validSlideDist)
            {
                _pages.Move2Top();
            }
            else _pages.MoveBack();
        }
        else
        {
            if (vector.x <= _validSlideDist * -1)
            {
                _pages.Move2Left();
            }
            else if (vector.x >= _validSlideDist)
            {

                _pages.Move2Right();
            }
            else _pages.MoveBack();
        }
    }
}

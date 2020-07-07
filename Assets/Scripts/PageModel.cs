using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageModel : MonoBehaviour
{
    public Vector2 NowPos { get { return _nowPage.AnchorPos; } }
    public Vector2 OriPos { get { return Vector2.zero; } }
    RectTransform _myRect;
    RectNode _nowPage;
    RectNode[] _pageList;
    Vector2 _targetPos;
    Vector2 _tarOffSet;

    // Start is called before the first frame update
    void Start()
    {
        _myRect = this.GetComponent<RectTransform>();
        _pageList = this.GetComponentsInChildren<RectNode>();
        InitNodes();
    }

    void Update()
    {
        MoveTo();
    }

    void InitNodes()
    {
        foreach (RectNode node in _pageList)
        {
            node.AnchorPos = node.gameObject.GetComponent<RectTransform>().anchoredPosition;
            if (node.AnchorPos == Vector2.zero) _nowPage = node;
        }
    }

    public void Move2Finger(Vector2 fingerPos, Vector2 offSet)
    {
        _targetPos = fingerPos;
        _tarOffSet = offSet;
    }

    public void MoveBack()
    {
        _targetPos = _nowPage.AnchorPos;
    }

    public void Move2Top()
    {
        _targetPos = _nowPage.Top.AnchorPos;
        _nowPage = _nowPage.Top;
    }

    public void Move2Bot()
    {
        _targetPos = _nowPage.Bottom.AnchorPos;
        _nowPage = _nowPage.Bottom;
    }

    public void Move2Right()
    {
        _targetPos = _nowPage.Right.AnchorPos;
        _nowPage = _nowPage.Right;
    }

    public void Move2Left()
    {
        _targetPos = _nowPage.Left.AnchorPos;
        _nowPage = _nowPage.Left;
    }

    public void ResetOffSet()
    {
        _tarOffSet = Vector2.zero;
    }

    void MoveTo()
    {
        _myRect.anchoredPosition = Vector2.Lerp(_myRect.anchoredPosition, _targetPos + _tarOffSet, 0.25f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectNode : MonoBehaviour {
    public Vector2 AnchorPos { get; set; }
    [SerializeField]
    RectNode _top;
    [SerializeField]
    RectNode _right;
    [SerializeField]
    RectNode _bottom;
    [SerializeField]
    RectNode _left;
    public RectNode Top { get { return _top; } }
    public RectNode Bottom { get { return _bottom; } }
    public RectNode Right { get { return _right; } }
    public RectNode Left { get { return _left; } }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipController
{
    void Move();
    void Fire(Vector3 target);
    void MeasureMapBorder();
}

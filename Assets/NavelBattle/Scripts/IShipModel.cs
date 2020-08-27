using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipModel
{
    void MoveTo(Vector3 target);
    void Fire(Vector3 target);
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipModel
{
    void InitShipModel(List<string> components);
    void MoveTo(Vector3 target);
    void Fire(Vector3 target);
}
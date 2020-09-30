using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnhancer
{
    EnhanceType EnhType { get; }
    int EnhanceRate { get; }
    int EffectID { get; }
}
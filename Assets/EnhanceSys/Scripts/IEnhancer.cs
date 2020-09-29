using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnhancer
{
    EnhanceType EnhType { get; set; }
    int EnhanceRate { get; set; }
    int EffectID { get; set; }
}
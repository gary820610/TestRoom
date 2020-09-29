using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnhanceable
{
    int EnhanceID { get; set; }
    int Exp { get; set; }
    int Lv { get; set; }
    bool IsEnhanceable { get; }
    EnhanceType EnhType { get; set; }

    void Enhance(IEnhancer enhancer);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public interface IEnhanceable
{
    /// <summary>
    /// Don't use this property if this object is not an enhancer. Normal gear don't need and won't have this id.
    /// </summary>
    int EnhanceID { get; }

    int Exp { get; }

    /// <summary>
    /// From 0 to 9.
    /// </summary>
    int Lv { get; }

    bool IsEnhanceable { get; }

    /// <summary>
    /// Define the enhance-family which this gear belongs. A gear can only be enhanced by an enhancer of the same EnhType.
    /// </summary>
    EnhanceType EnhType { get; }

    /// <summary>
    /// Current enhanced state by jade.
    /// </summary>
    JadeEnhanceState JadeEnhStat { get; }

    void EnhanceBy(IEnhancer enhancer);
    void LevelUp();
    void ResetExp();
    void IncExp(int exp);
    void SetEnhState(JadeEnhanceState state);
}

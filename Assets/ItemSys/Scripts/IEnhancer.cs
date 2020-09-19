using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnhancer
{
    string EnhanceType { get; }
    int SuccessRate { get; }
    int Effect { get; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class EnhSysSettings
{
    /// <summary>
    /// 玉珮可提供經驗值的基數。
    /// </summary>
    /// <value>100</value>
    static public int BaseJadeExp => 100;

    /// <summary>
    /// 裝備升段所需經驗值的基數。裝備升段所需經驗 = (增加倍數 ^ 裝備段數) * 經驗值基數。
    /// </summary>
    /// <value>1000</value>
    static public int BaseLvUpExp => 1000;

    /// <summary>
    /// 裝備升段所需經驗值的增加倍數。裝備升段所需經驗 = (增加倍數 ^ 裝備段數) * 經驗值基數。
    /// </summary>
    /// <value>1.5</value>
    static public float LvUpExpRatio => 1.5f;

    /// <summary>
    /// 裝備的段數上限。
    /// </summary>
    /// <value>9</value>
    static public int GearMaxLv => 9;
}

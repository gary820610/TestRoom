using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceMaster : MonoBehaviour
{
    IEnhanceable m_Gear;
    IEnhancer m_Enhancer;

    void Start()
    {
        ItemIndexer.Init();
        EnhanceIndexer.Init();
        
    }

    void Update()
    {

    }

    public void SelectGear(IEnhanceable gear)
    {
        m_Gear = gear;
    }

    public void UnselectGear()
    {
        m_Gear = null;
    }

    public void SelectEnhancer(IEnhancer enhancer)
    {
        m_Enhancer = enhancer;
    }

    public void UnselectEnhancer()
    {
        m_Enhancer = null;
    }

    public void EnhanceByRune()
    {
        if (!CheckTypeMatch())
        {
            Debug.Log("EnhanceType not match");
            return;
        }

        m_Gear.LevelUp();

        if (!RollDice(m_Enhancer.EnhanceRate))
        {
            Debug.Log("Enhance failed");
            return;
        }

        m_Gear.EnhanceBy(m_Enhancer);
    }

    public void EnhanceByJade()
    {
        if (m_Enhancer.EnhType != EnhanceType.Jade)
        {
            Debug.Log("This enhancer is not a Jade");
            return;
        }

        m_Gear.IncExp(EnhSysSettings.BaseJadeExp * ((Jade)m_Enhancer).Rarity);
        m_Gear.SetEnhState((JadeEnhanceState)((Jade)m_Enhancer).Rarity);

        if (m_Gear.Exp > Mathf.Pow(EnhSysSettings.LvUpExpRatio, m_Gear.Lv) * EnhSysSettings.LvUpExpRatio)
        {
            m_Gear.LevelUp();
            m_Gear.ResetExp();
            m_Gear.EnhanceBy(m_Enhancer);            
        }
    }

    public bool RollDice(int rate)
    {
        var chance = Random.Range(0, 99);
        if (chance < rate) return true;
        else return false;
    }

    public bool CheckTypeMatch()
    {
        if (m_Enhancer.EnhType != m_Gear.EnhType)
        {
            return false;
        }
        else return true;
    }    
}

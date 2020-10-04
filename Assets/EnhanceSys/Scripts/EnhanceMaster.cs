using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceMaster : MonoBehaviour
{
    GameMain _main;
    IEnhanceable m_Gear;
    IEnhancer m_Enhancer;

    [SerializeField]
    GameObject m_GearPock;
    [SerializeField]
    GameObject m_RunePock;
    [SerializeField]
    GameObject m_JadePock;
    [SerializeField]
    GameObject m_EnhPanel;

    public delegate void SelectHandler();
    public event SelectHandler OnSelectEnhcer;
    public delegate void RemoveHandler();

    void Start()
    {
        _main = GameObject.FindWithTag("GameMain").GetComponent<GameMain>();
        foreach (IEnhanceable gear in _main.User.MyBag.MyShipGears)
        {
            GameObject go = GameObject.Instantiate(AssetsLoader.LoadPrefab("GearUI"));
            go.transform.SetParent(m_GearPock.transform);
            GearUIWrapper grid = go.GetComponent<GearUIWrapper>();
            grid.InjectGearData(gear);
            grid.SetEnhBtn(OpenEnhPanel);
            ((ShipGear)gear).OnRemove += grid.RemoveThis;
        }

        foreach (IEnhancer enhcer in _main.User.MyBag.MyRunes)
        {
            GameObject go = GameObject.Instantiate(AssetsLoader.LoadPrefab("EnhUI"));
            go.transform.SetParent(m_RunePock.transform);
            EnhencerUIWrapper grid = go.GetComponent<EnhencerUIWrapper>();
            grid.InjectGearData(enhcer);
            grid.SetEnhBtn(SelectEnhancer);
            OnSelectEnhcer += grid.Unselect;
            ((Rune)enhcer).OnRemove += grid.RemoveThis;
        }

        foreach (IEnhancer enhcer in _main.User.MyBag.MyJades)
        {
            GameObject go = GameObject.Instantiate(AssetsLoader.LoadPrefab("EnhUI"));
            go.transform.SetParent(m_JadePock.transform);
            EnhencerUIWrapper grid = go.GetComponent<EnhencerUIWrapper>();
            grid.InjectGearData(enhcer);
            grid.SetEnhBtn(SelectEnhancer);
            OnSelectEnhcer += grid.Unselect;
            ((Jade)enhcer).OnRemove += grid.RemoveThis;
        }

        ItemIndexer.Init();
        EnhanceIndexer.Init();
        CloseEnhPanel();
    }

    void Update()
    {

    }

    public void SelectEnhancer(IEnhancer enhancer)
    {
        UnselectEnhancer();
        m_Enhancer = enhancer;
    }

    public void OpenEnhPanel(IEnhanceable gear)
    {
        SelectGear(gear);
        m_EnhPanel.SetActive(true);
    }

    public void CloseEnhPanel()
    {
        UnselectGear();
        UnselectEnhancer();
    }

    public void EnhanceByRune()
    {
        if (!CheckTypeMatch())
        {
            Debug.Log("EnhanceType not match");
            return;
        }

        if (m_Gear.Lv >= 9)
        {
            Debug.Log("已達強化極限");
            return;
        }

        m_Gear.LevelUp();
        Debug.Log("強化成功機率 === " + m_Enhancer.EnhanceRate + "%");

        if (!RollDice(m_Enhancer.EnhanceRate))
        {
            Debug.Log("Enhance failed");
            ((Rune)m_Enhancer).Decrease(1);
            return;
        }

        m_Gear.EnhanceBy(m_Enhancer);
        ((Rune)m_Enhancer).Decrease(1);
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

        Debug.Log(((ShipGear)m_Gear).Name + " 經驗值 = " + m_Gear.Exp);

        if (m_Gear.Exp > Mathf.Pow(EnhSysSettings.LvUpExpRatio, m_Gear.Lv) * EnhSysSettings.BaseLvUpExp)
        {
            m_Gear.LevelUp();
            m_Gear.ResetExp();
            m_Gear.EnhanceBy(m_Enhancer);
        }

        ((Jade)m_Enhancer).Decrease(1);
    }

    void Reload()
    {

    }

    void SelectGear(IEnhanceable gear)
    {
        m_Gear = gear;
    }

    void UnselectGear()
    {
        m_Gear = null;
    }

    void UnselectEnhancer()
    {
        m_Enhancer = null;
        OnSelectEnhcer();
    }

    bool RollDice(int rate)
    {
        var chance = Random.Range(0, 99);
        if (chance < rate) return true;
        else return false;
    }

    bool CheckTypeMatch()
    {
        if (m_Enhancer.EnhType != m_Gear.EnhType)
        {
            return false;
        }
        else return true;
    }
}

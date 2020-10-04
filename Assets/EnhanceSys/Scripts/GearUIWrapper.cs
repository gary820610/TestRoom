using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearUIWrapper : MonoBehaviour, IUIWrapper
{
    IEnhanceable MyGear { get; set; }
    [SerializeField]
    GameObject m_EnhanceBtn;
    [SerializeField]
    Text m_GearName;
    System.Action<IEnhanceable> OnBtnClick;

    public void InjectGearData(IEnhanceable gear)
    {
        MyGear = gear;
        m_GearName.text = ((ShipGear)gear).Name;
    }

    public void SetEnhBtn(System.Action<IEnhanceable> action)
    {
        OnBtnClick = action;
        m_EnhanceBtn.GetComponent<Button>().onClick.AddListener(ActWrapper);
    }

    public void RemoveThis()
    {
        GameObject.Destroy(this.gameObject);
    }

    void ActWrapper()
    {
        OnBtnClick(MyGear);
    }
}

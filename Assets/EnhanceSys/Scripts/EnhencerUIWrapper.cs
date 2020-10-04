using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhencerUIWrapper : MonoBehaviour, IUIWrapper
{
    IEnhancer MyEnhencer { get; set; }
    System.Action<IEnhancer> OnClick;
    [SerializeField]
    Image m_Img;
    [SerializeField]
    Color _selectColor;
    [SerializeField]
    Color _unselectColor;

    void Start()
    {
        m_Img.color = _unselectColor;
    }

    public void InjectGearData(IEnhancer enhcer)
    {
        MyEnhencer = enhcer;
    }

    public void SetEnhBtn(System.Action<IEnhancer> action)
    {
        OnClick = action;
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(ActWrapper);
        btn.onClick.AddListener(Select);
    }

    public void Select()
    {
        m_Img.color = _selectColor;
    }

    public void Unselect()
    {
        m_Img.color = _unselectColor;
    }

    void ActWrapper()
    {
        OnClick(MyEnhencer);
    }

    public void RemoveThis()
    {
        GameObject.Destroy(this.gameObject);
    }
}

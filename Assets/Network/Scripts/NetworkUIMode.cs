using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUIMode : MonoBehaviour
{
    Dictionary<string, InputField> _inputFields;
    // Start is called before the first frame update
    void Start()
    {
        _inputFields = new Dictionary<string, InputField>();
        InputField[] myInputs = this.gameObject.GetComponentsInChildren<InputField>();
        foreach (InputField input in myInputs)
        {
            _inputFields.Add(input.gameObject.name, input);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetInputText(string fieldName)
    {
        if (!_inputFields.ContainsKey(fieldName))
        {
            Debug.Log("No field name : " + fieldName);
            return "";
        }
        else
        {
            return _inputFields[fieldName].text;
        }
    }
}

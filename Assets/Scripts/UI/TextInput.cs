using System;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    InputField input;
    InputField.SubmitEvent onSubmit;
    InputField.OnChangeEvent onChange;
    public Text output;

    void Start()
    {
        input = this.GetComponent<InputField>();
        onSubmit = new InputField.SubmitEvent();
        onSubmit.AddListener(SubmitInput);

        input.onEndEdit = onSubmit;

    }

    private void SubmitInput(string arg0)
    {
        //not the final way of doing this
        if (arg0 == "attack")
        {

        }
        Debug.Log("text was " + arg0);
    }

    public void CheckInput(string input)
    {
        Debug.Log("text was " + input);
    }

}

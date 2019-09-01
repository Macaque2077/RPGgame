using System;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    //public GameObject textOutput;

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
        Debug.Log("text was " + arg0);
        //CommandSorter sortText = new CommandSorter();
        //set the output text to the result of the command sorter
        output.text = CommandSorter.Parse(arg0);
        input.text = "";
        Debug.Log("text was " + arg0);
    }

    public void CheckInput(string input)
    {
        Debug.Log("text was " + input);
    }

}

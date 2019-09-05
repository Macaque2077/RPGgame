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
        //sets the input as the relevant input component in the GUI
        input = this.GetComponent<InputField>();
        onSubmit = new InputField.SubmitEvent();
        onSubmit.AddListener(SubmitInput);

        input.onEndEdit = onSubmit;

    }

    private void SubmitInput(string inputText)
    {
        Debug.Log("text was " + inputText);
        //CommandSorter sortText = new CommandSorter();
        //set the output text to the result of the command sorter
        output.text = CommandSorter.Parse(inputText);
        input.text = "";
        Debug.Log("text was " + inputText);
    }

    public void CheckInput(string input)
    {
        Debug.Log("text was " + input);
    }

}

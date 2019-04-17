using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Yarn;

public class DialogueUIHandler : Yarn.Unity.DialogueUIBehaviour
{
    /// The object that contains the dialogue and the options.
    /** This object will be enabled when conversation starts, and 
     * disabled when it ends.
     */
    public GameObject dialogueContainer;

    /// The UI element that displays lines
    public Text lineText;

    /// A delegate (ie a function-stored-in-a-variable) that
    /// we call to tell the dialogue system about what option
    /// the user selected
    private Yarn.OptionChooser SetSelectedOption;

    /// How quickly to show the text, in seconds per character
    [Tooltip("How quickly to show the text, in seconds per character")]
    public float textSpeed = 0.025f;

    public float endDelay = 1f;

    /// The buttons that let the user choose an option
    public List<Button> optionButtons;

    /// Animator for the UI elements
    public Animator uiAnim;

    void Awake(){
        // Start by hiding the container, line and option buttons
        if (dialogueContainer != null)
            dialogueContainer.SetActive(false);

        lineText.gameObject.SetActive(false);

        foreach (var button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public override IEnumerator DialogueComplete()
    {
        //Debug.Log("Complete!");

        // Hide the dialogue interface.
        if (dialogueContainer != null)
            dialogueContainer.SetActive(false);

        yield break;
    }

    public override IEnumerator DialogueStarted()
    {
        // Enable the dialogue controls.
        if (dialogueContainer != null)
            dialogueContainer.SetActive(true);

        yield break;
    }

    public override IEnumerator RunCommand(Command command)
    {
        //TODO:
        throw new System.NotImplementedException();
    }

    public override IEnumerator RunLine(Line line)
    {
        // Show the text
        lineText.gameObject.SetActive(true);

        if (textSpeed > 0.0f)
        {
            // Display the line one character at a time
            var stringBuilder = new StringBuilder();

            foreach (char c in line.text)
            {
                stringBuilder.Append(c);
                lineText.text = stringBuilder.ToString();
                yield return new WaitForSeconds(textSpeed);
            }
        }
        else
        {
            // Display the line immediately if textSpeed == 0
            lineText.text = line.text;
        }

        //Gives the player a little bit of time at the end
        yield return new WaitForSeconds(endDelay);

        // Hide the text and prompt
        lineText.gameObject.SetActive(false);
    }

    public override IEnumerator RunOptions(Options optionsCollection, OptionChooser optionChooser)
    {
        //TODO: Implement a timer that defaults to a certain option.

   
        // Do a little bit of safety checking
        if (optionsCollection.options.Count > optionButtons.Count)
        {
            Debug.LogWarning("There are more options to present than there are" +
                             "buttons to present them in. This will cause problems.");
        }

        // Display each option in a button, and make it visible
        int i = 0;
        foreach (var optionString in optionsCollection.options)
        {
            optionButtons[i].gameObject.SetActive(true);
            optionButtons[i].GetComponentInChildren<Text>().text = optionString;
            i++;
        }
        uiAnim.SetInteger("OptionsNum", i);

        // Record that we're using it
        SetSelectedOption = optionChooser;

        // Wait until the chooser has been used and then removed (see SetOption below)
        while (SetSelectedOption != null)
        {
            yield return null;
        }

        // Hide all the buttons
        foreach (var button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    /// Called by buttons to make a selection.
    public void SetOption(int selectedOption)
    {

        // Call the delegate to tell the dialogue system that we've
        // selected an option.
        SetSelectedOption(selectedOption);

        // Now remove the delegate so that the loop in RunOptions will exit
        SetSelectedOption = null;
    }
}

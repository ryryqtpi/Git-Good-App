using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour {

	private CommandLineInterpreter cli;
	public Canvas mainCanvas;
	public Text displayTextBox;
	public Text instructionsTextBox;
	public InputField commandLine;
	public ScrollRect scrollRect;
	public int lineCount = 0;

	public bool exerciseInProgress = false;

	// Use this for initialization
	void Start () {
		cli = gameObject.GetComponent<CommandLineInterpreter> ();
		commandLine.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		commandLine.ActivateInputField();
	}

	public void SetIntructionsText(string instructions)
	{
		instructionsTextBox.text = instructions;
	}

	public bool UserPressedEnter()
	{
		return Input.GetButtonDown ("Submit");
	}

	// Sends a string to the console
	public void SendToConsole() 
	{
		if (UserPressedEnter()) 
		{
			// Trim command before sending it
			string trimmed_command = commandLine.text.Trim ();

			if (trimmed_command == "") 
			{
				return;
			}

			Command command = new Command (commandLine.text.Trim ());

			PrintToConsole (command.Output(displayTextBox.text, lineCount));
			cli.HandleCommand(trimmed_command);

			// Update the UI after making changes
			ForceUpdateConsoleUI();
		}
	}

	public void ForceUpdateConsoleUI() 
	{
		scrollRect.verticalScrollbar.value=0f;
	}

	public void ClearConsole()
	{
		// Clear the console text box
		displayTextBox.text = "";
	}

	public void PrintToConsole(string message)
	{
		displayTextBox.text += message;

		ForceUpdateConsoleUI();

		commandLine.text = "";
		lineCount++;
	}
}

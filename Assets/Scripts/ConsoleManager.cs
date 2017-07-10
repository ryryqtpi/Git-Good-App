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
	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		cli = gameObject.GetComponent<CommandLineInterpreter> ();
		commandLine.text = "";
	}

	// Update is called once per frame
	void Update ()
	{
		commandLine.ActivateInputField();
	}

	public void SetIntructionsText(string instructions)
	{
		instructionsTextBox.text = instructions;
	}

	public void ResetInstructionsText(int id){
		string instructions = "<b>exercises</b>          <b>exercise</b>          <b>profile</b>          ";
		if (id == 1) {
			instructions += "<b>" + id + "</b>";
		} else {
			instructions += "<b><1-"+id+"></b>";
		}
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

//			if (trimmed_command == "")
//			{
//				return;
//			}

			Entry entry = new Entry (trimmed_command);

			PrintToConsole (entry.Output(displayTextBox.text, lineCount));
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

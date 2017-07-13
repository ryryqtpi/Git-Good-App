using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour {

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
		string instructions = "<b>exercises</b>          <b>profile</b>          ";
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

	public void ScrollToTop()
	{
		scrollRect.normalizedPosition = new Vector2(0, 1);
	}
	public void ScrollToBottom()
	{
		scrollRect.normalizedPosition = new Vector2(0, 0);
	}

	public void ClearConsole()
	{
		// Clear the console text box
		displayTextBox.text = "";
		ClearCommandLine ();
		ScrollToBottom ();
	}

	public void ClearCommandLine()
	{
		commandLine.text = "";	
	}

	public void SetCommandLine(string command){
		commandLine.text = command;
	}

	public void PrintToConsole(string message)
	{
		displayTextBox.text += message;
		commandLine.text = "";
		lineCount++;
		ScrollToBottom ();
	}

	public void PrintUserEntryToConsole(string message, string username)
	{
		if (message.Trim () != "") {
			displayTextBox.text += (username + "$ " + message + "\n");
			lineCount++;
		}
		ClearCommandLine ();
		ScrollToBottom ();
	}
}

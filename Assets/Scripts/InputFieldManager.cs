using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour {

	public Canvas mainCanvas;
	public Text consoleTextBox;
	public InputField commandLine;
	public ScrollRect scrollRect;

	// Use this for initialization
	void Start () {
		commandLine.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		commandLine.ActivateInputField();
	}

	// Sends a string to the console
	public void SendToConsole() {
		if (Input.GetButtonDown ("Submit")) {
			print ("SendToConsole: " + commandLine.text);
			string currText = consoleTextBox.text;
			string commandText = commandLine.text.Trim();
			if (commandText != "") {
				if (consoleTextBox.text != "") {
					consoleTextBox.text = currText + "\n" + commandText;
				} else {
					consoleTextBox.text = commandText;
				}
			}
			commandLine.text = "";
			ForceUpdateConsole();
		}

	}

	public void ForceUpdateConsole() {
		scrollRect.verticalScrollbar.value=0f;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Step : MonoBehaviour {

	public string answer;
	public string instruction;
	public string correct_response;
	public string error_response;
	public Command[] commands;

	public void populate (JSONNode stepJSON)
	{
		this.answer = stepJSON["answer"];
		this.instruction = stepJSON["instruction"];
		this.correct_response = stepJSON["correct_response"];
		this.error_response = stepJSON["error_response"];

		int count = stepJSON ["commands"].Count;
		this.commands = new Command[count];

		for(int c=0; c<count; c++){
			var commandJSON = stepJSON["commands"][c];
//			GameObject go = new GameObject ();
//			Command command = go.AddComponent<Command> ();
			Command command = new Command();
			command.populate (commandJSON);
			this.commands[c] = command;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

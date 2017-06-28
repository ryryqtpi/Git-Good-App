﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CommandLineInterpreter : MonoBehaviour {

	private ConsoleManager cm;
	public string loginSceneName;

	// Use this for initialization
	void Start () 
	{
		// Do something
		cm = gameObject.GetComponent<ConsoleManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Do something
	}

	public void HandleCommand(string command)
	{
		// Command Name: Clear
		// Discription: Clears the console view
		if (command == "clear") 
		{
			cm.ClearConsole ();
		} 

		// Command Name: Login
		// Discription: Loads the login page in the comsole view
		else if (command == "login") 
		{
			SceneManager.LoadScene (loginSceneName, LoadSceneMode.Single);
		}

		// If a valid command is not found, return an error message
		else 
		{
			cm.PrintToConsole ("\n<color=#ff0000ff>ERROR: command not recognized</color>\n");
		}
	}
}
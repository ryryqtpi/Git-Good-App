using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Command {

	public string command_name;
	public string argument;

	public void populate (JSONNode commandJSON){
		this.command_name = commandJSON["name"];
		this.argument = commandJSON["argument"];
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}

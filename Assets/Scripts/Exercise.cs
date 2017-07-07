using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class Exercise : MonoBehaviour
{
	
	public string exercise_name;
	public string description;
	public string level;
	public bool completed = false;
	public Step[] steps;

	public void populate(JSONNode exerciseJSON){
		this.exercise_name = exerciseJSON["name"];
		this.description = exerciseJSON["description"];
		this.level = exerciseJSON["level"];

		int count = exerciseJSON ["steps"].Count;
		this.steps = new Step [count];

		for(int s=0; s<count; s++){
			var stepJSON = exerciseJSON["steps"][s];
			Step step = new Step();
			step.populate (stepJSON);
			steps[s] = step;
		}
	}

	public override string ToString(){
		string ret = exercise_name;
		ret += "\n    Level: " + level;
		ret += "\n    Description: " + description;
		return ret;
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

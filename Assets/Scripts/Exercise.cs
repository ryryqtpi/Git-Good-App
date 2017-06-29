using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class Exercise
{
	public string exercise_name;
	public string discription;
	public string difficulty_level;
	public bool completed = false;
	public Step[] steps;

	public void populate(JSONNode exerciseJSON){
		this.exercise_name = exerciseJSON["name"];
		this.discription = exerciseJSON["description"];
		this.difficulty_level = exerciseJSON["difficulty"];

		int count = exerciseJSON ["steps"].Count;
		this.steps = new Step [count];

		for(int s=0; s<count; s++){
			var stepJSON = exerciseJSON["steps"][s];
//			GameObject go = new GameObject ();
//			Step step = go.AddComponent<Step> ();
			Step step = new Step();
			step.populate (stepJSON);
			steps[s] = step;
		}
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

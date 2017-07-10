using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExerciseManager : MonoBehaviour {

	public Exercise[] exercises;
	public ConsoleManager cm;
	public APIInterface api;

	// Use this for initialization
	void Start ()
	{
		cm = GameObject.FindGameObjectWithTag ("ConsoleManager").GetComponent<ConsoleManager> ();
		api = GameObject.FindGameObjectWithTag ("API").GetComponent<APIInterface>();
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	public string LastExercise(){
		return exercises [exercises.Length].ToString();
	}

	public void StartExercise(int id)
	{
		cm.PrintToConsole(exercises[id].ToString());
		cm.SetIntructionsText ("Press enter to start exercise " + (id+1) + "...\n");
	}

	public void EndExercise(string exercise_name)
	{
		Debug.Log ("Ending exercise: " + exercise_name);
	}

	public void SaveExercises(Exercise[] new_exercises){
		exercises = new_exercises;
	}

	public string ExercisesString()
	{
		string ret = "";
		for (int e = 0; e < exercises.Length; e++) {
			ret += (e + 1) + ". " + exercises[e].ToString() + "\n";
		}
		return ret;
	}

	public string ExerciseString(int id){
		if (exercises [id] != null) {
			return exercises [id].ToString ();
		} else {
			return "Invalid exercise ID: "+id+". User Level must be >= Exercise Level.\n";
		}
	}

	public void RunExercise(ref int exercise_started, ref int step_id, string input){
		Step step;

		if (step_id < 0) {
			step_id++;
			step = exercises [exercise_started].steps [step_id];
			cm.PrintToConsole (step.BoldString());
			cm.SetIntructionsText (step.CommandsString());
			return;
		} else {
			step = exercises [exercise_started].steps [step_id];
		}

		if (step.answer == input) {
			cm.PrintToConsole (step.correct_response+"\n");
			step_id++;
			if (step_id >= exercises [exercise_started].steps.Length) {
				cm.PrintToConsole ("Completed Exercise " + (exercise_started+1) + ": " + exercises [exercise_started].exercise_name + "!\n");
				step_id = -1;
				exercise_started = -1;
				StartCoroutine (api.PostIncrementUserLevel());
			} else {
				step = exercises [exercise_started].steps [step_id];
				cm.PrintToConsole (step.BoldString());
				cm.SetIntructionsText (step.CommandsString());
			}
		} else {
			cm.PrintToConsole (step.error_response+"\n");
		}
	}

}

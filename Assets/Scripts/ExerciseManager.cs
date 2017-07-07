using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExerciseManager : MonoBehaviour {

	public Exercise[] exercises;
	public ConsoleManager cm;
//	public APIInterface api;

	// Use this for initialization
	void Start ()
	{
		cm = GameObject.FindGameObjectWithTag ("ConsoleManager").GetComponent<ConsoleManager> ();
//		api = GameObject.FindGameObjectWithTag ("API").GetComponent<APIInterface>();
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	public string LastExercise(){
		return exercises [exercises.Length].ToString();
	}

	public void StartExercise(string exercise_name)
	{
		Debug.Log ("Starting exercise: " + exercise_name);
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

	public void StartStep(int exercise_id, int step_id){
		Step step = exercises [exercise_id].steps [step_id];
		cm.PrintToConsole (step.ToString ());

	}
}

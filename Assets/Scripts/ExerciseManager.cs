using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseManager : MonoBehaviour {

	public Exercise[] exercises;
	public ConsoleManager cm;
	public APIInterface api;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void StartExercise(string exercise_name)
	{
		Debug.Log ("Starting exercise: " + exercise_name);
	}

	public void EndExercise(string exercise_name)
	{
		Debug.Log ("Ending exercise: " + exercise_name);
	}

	public void SaveExerciseList(Exercise[] exercises)
	{
		this.exercises = exercises;
	}

	public void PrintExercises()
	{
		cm.PrintToConsole ("\n<b>Exercises</b>");
		for (int e = 0; e < exercises.Length; e++) {
			cm.PrintToConsole ("\n" + (e + 1) + ". " + exercises [e].exercise_name);
		}
	}
}

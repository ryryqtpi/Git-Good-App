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
		cm = GameObject.FindGameObjectWithTag ("ConsoleManager").GetComponent<ConsoleManager> ();
		api = GameObject.FindGameObjectWithTag ("API").GetComponent<APIInterface> ();
		api.UpdateExercises();
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

	public void PrintExercises()
	{
		exercises = GetComponentsInChildren<Exercise> ();
//		Debug.Log (exercises[0].exercise_name);

		cm.PrintToConsole ("\n<b>Exercises</b>");
		for (int e = 0; e < exercises.Length; e++) {
			cm.PrintToConsole ("\n" + (e + 1) + ". " + exercises [e].exercise_name);
		}
	}
}

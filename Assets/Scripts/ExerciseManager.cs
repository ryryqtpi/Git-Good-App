using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseManager : MonoBehaviour {

	public GameObject exercisePrefab;

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

	public void GenerateSampleExercise()
	{
		GameObject go = Instantiate (exercisePrefab, transform);
		Exercise exercise = go.GetComponent<Exercise> ();

		// Creating an exercise
		exercise.name = "Exercise #1";
		exercise.discription = "A breief tutorial on how to use the <b>[cd]</b> and <b>[ls]</b> commands in the terminal.";
		exercise.skills = "<b>[cd]</b> and <b>[ls]</b>";
	}
}

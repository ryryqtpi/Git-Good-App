using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CommandLineInterpreter : MonoBehaviour {

	private ConsoleManager cm;
	public APIInterface api;
	public ExerciseManager em;
	int state = 0;

	public string loginSceneName;

	void Start () 
	{
		cm = gameObject.GetComponent<ConsoleManager>();
		api = GameObject.FindGameObjectWithTag ("API").GetComponent<APIInterface>();
		em = GameObject.FindGameObjectWithTag ("ExerciseManager").GetComponent<ExerciseManager>();
	}
	
	void Update () 
	{
		
	}

	public void HandleCommand(string command)
	{
		if (command == "clear") 
		{
			cm.ClearConsole ();
		} 
		else if (command == "login") 
		{
			SceneManager.LoadScene (loginSceneName, LoadSceneMode.Single);
		}
		else if (command == "exercises") 
		{
			em.PrintExercises ();
			state = 1;
		}
		else
		{
			switch (state) {
			case 0:
				
				break;
			case 1:
				
				break;
			}
			cm.PrintToConsole ("\n<color=#ff0000ff>ERROR: command not recognized</color>");
		}
	}
}

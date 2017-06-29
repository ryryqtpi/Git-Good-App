using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exercise : MonoBehaviour
{
	
	public string exercise_name;
	public string discription;
	public string difficulty_level;
	public bool completed = false;

	public List<Step> steps;

	public Exercise(string name, string description, string difficulty_level){
		this.exercise_name = name;
		this.discription = description;
		this.difficulty_level = difficulty_level;
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

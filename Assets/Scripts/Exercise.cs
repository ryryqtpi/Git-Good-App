using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exercise : MonoBehaviour
{

	public string name;
	public string discription;
	public string skills;

	public List<string> steps;

	public Text nameText;
	public Text discriptionText;
	public Text skillsText;


	// Use this for initialization
	void Start ()
	{
		nameText.text = name;
		discriptionText.text = discription;
		skillsText.text = skills;
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void AddStep(string step)
	{
		steps.Add (step);

		/*foreach (string text in steps)
		{
			Debug.Log (text);
		}*/
	}
}

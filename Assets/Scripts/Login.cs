using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

	private string username = "";
	private string password = "";
	public ConsoleManager cm;

	// Use this for initialization
	void Start () {
		cm.SetIntructionsText("Please enter your <color=#ffffffff>GitHub</color> account/username.");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

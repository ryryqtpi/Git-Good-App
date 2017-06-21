using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

	public InputField commandLine;
	public string sceneName = "";

	// Use this for initialization
	void Start () {
		commandLine.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		commandLine.ActivateInputField();
	}

	public void UpdateConsole() {
		if (Input.GetButtonDown ("Submit")) {
			commandLine.text = "";
			SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		}
	}
}

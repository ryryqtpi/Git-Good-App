using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelayMessage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//point the text child object of the input field here
	public Text inputText;
	string messageToSend;

	public void Send()
	{
		if(Input.GetButtonDown("Submit")){
			messageToSend = inputText.text;
		}
	}
}

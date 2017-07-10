using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Webpage : MonoBehaviour {

	public string _url = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Open ()
	{
		Debug.Log ("Opening webpage... " + _url);
		Application.OpenURL(_url);
	}
}

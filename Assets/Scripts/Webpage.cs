using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Webpage {

	public string _url = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Open (string url)
	{
		_url = url;
		Debug.Log ("Opening webpage... " + _url);
		Application.OpenURL(_url);
	}
}

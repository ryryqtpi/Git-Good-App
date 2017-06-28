// Original Author: Laik http://answers.unity3d.com/users/185453/1-1.html
// Modified by Ryan Feldman 06/26/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typewriter : MonoBehaviour {

	public Text textBox;

	void OnGUI() {
		GUIStyle textStyle = new GUIStyle();
		textStyle.normal.textColor = Color.white;
		textStyle.wordWrap = true;
		textStyle.richText = true;
	}


	//Store all your text in this string array
	string[] goalText = new string[]{"<b>Welcome!</b> Please enter your GitHub username.", "2. You can click to skip to the next text", "3.All text is stored in a single string array", "4. Ok, now we can continue","5. End Kappa"};
	int currentlyDisplayingText = 0;
	void Awake () {
		StartCoroutine(AnimateText());
	}
	//This is a function for a button you press to skip to the next text
	public void SkipToNextText(){
		StopAllCoroutines();
		currentlyDisplayingText++;
		//If we've reached the end of the array, do anything you want. I just restart the example text
		if (currentlyDisplayingText>goalText.Length) {
			currentlyDisplayingText=0;
		}
		StartCoroutine(AnimateText());
	}
	//Note that the speed you want the typewriter effect to be going at is the yield waitforseconds (in my case it's 1 letter for every      0.03 seconds, replace this with a public float if you want to experiment with speed in from the editor)
	IEnumerator AnimateText(){

		for (int i = 0; i < (goalText[currentlyDisplayingText].Length+1); i++)
		{
			textBox.text = goalText[currentlyDisplayingText].Substring(0, i);
			yield return new WaitForSeconds(.03f);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
			// NEXT TEXT ITEM
			SkipToNextText();
		}
	}
}

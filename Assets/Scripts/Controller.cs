using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;
using System.Linq;

public class Controller : MonoBehaviour
{
	public string username;
	public string token;
	public User user;

	public InputField commandLine;
	public GameObject profilePicture;
	public GameObject UserPrefab;
	public List<string> inputHistory = new List<string>();
	public int history_state = 0;

	public ConsoleManager cm;
	public ExerciseManager em;
	public APIInterface api;
	public ExpCalculator exp;

	int state = 0;
	int exercise_started = -1;
	int step = -1;

	void Start () 
	{
		api = GameObject.FindGameObjectWithTag ("API").GetComponent<APIInterface>();
		cm = GameObject.FindGameObjectWithTag ("ConsoleManager").GetComponent<ConsoleManager> ();
		em = GameObject.FindGameObjectWithTag ("ExerciseManager").GetComponent<ExerciseManager> ();

		// Create an empty user
		GameObject go = Instantiate (UserPrefab);

		// User & ExpCalculator require each other
		user = go.AddComponent<User>();
		exp = go.AddComponent<ExpCalculator> ();

		DontDestroyOnLoad (go);

		CanvasGroup profile = GameObject.Find("Profile").GetComponent<CanvasGroup>();
		profile.alpha = 0;

		cm.SetIntructionsText ("Type your GitHub username, then press enter.");
		string message = "Username: ";
		cm.PrintToConsole (message);
	}

	void Update ()
	{
		if ((Input.GetKey (KeyCode.RightControl) || Input.GetKey (KeyCode.LeftControl)) && Input.GetKeyDown (KeyCode.C)) {
			QuitCurrentExercise ();	
		} else if((Input.GetKey (KeyCode.RightControl) || Input.GetKey (KeyCode.LeftControl)) && Input.GetKeyDown (KeyCode.L)){
			cm.ClearConsole ();
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			HistoryUp ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			HistoryDown ();
		}
	}

	void HistoryUp(){
		if (history_state > 0) {
			history_state--;
			cm.SetCommandLine (inputHistory[history_state]);
		}
	}

	void HistoryDown(){
		if (history_state < (inputHistory.Count - 1)) {
			history_state++;
			cm.SetCommandLine (inputHistory [history_state]);
		} else if (history_state == (inputHistory.Count - 1)) {
			cm.ClearCommandLine ();
		}
	}

	void QuitCurrentExercise()
	{
		cm.PrintToConsole ("^C\n");
		if ((state == 2) && (exercise_started != -1)) {
			cm.ClearConsole ();
			cm.ResetInstructionsText (api.exercise_limit);
			exercise_started = -1;
			step = -1;
		} else {
			Debug.Log ("Unable to quit exercise because no exercise has been started.");
		}
	}

	void UpdateProfileDisplay()
	{
		CanvasGroup profile = GameObject.Find("Profile").GetComponent<CanvasGroup>();
		profile.alpha = 1;

		Text usernameText = GameObject.Find ("Username").GetComponent<Text> ();
		usernameText.text = user.username;
	}

	public void RouteInput()
	{
		if (Input.GetButtonDown ("Submit")) 
		{
			switch (state) 
			{
			case 0: //prompting for username
				Debug.Log ("Username entered");
				SubmitUsername ();
				break;
			case 1: //prompting for token
				Debug.Log ("Token entered");
				SubmitToken ();
				break;
			case 2: //successfully logged in
				SaveHistory(commandLine.text);
				if (exercise_started >= 0) {
					em.RunExercise (ref exercise_started, ref step, commandLine.text);
				} else {
					SubmitMenuChoice();
				}
				break;
			}
		}
	}

	public void SubmitMenuChoice(){

		if (commandLine.text == "") {
			// Do something
			cm.ScrollToBottom();
		} else if (commandLine.text == "exercises") {
			cm.ClearConsole ();
			cm.PrintToConsole ("\n<b>Exercises</b>\n");
			cm.PrintToConsole (em.ExercisesString ());
			cm.ScrollToTop ();
		} else if (commandLine.text == "profile") {
			cm.ClearConsole ();
			cm.PrintToConsole ("\n<b>Profile</b>\n");
			cm.PrintToConsole (user.ToString ());
			cm.ScrollToTop ();
		} else {
			int id_int;
			bool parsed = Int32.TryParse(commandLine.text, out id_int);
			if (parsed) {
				id_int--;
				cm.ClearConsole ();
				em.StartExercise (id_int);
				cm.ScrollToTop ();
				exercise_started = id_int;
			} else {
				cm.PrintToConsole ("<color=#ff0000ff>ERROR: command not recognized</color>\n");
			}
		}
	}

	public void SaveHistory(string command){
		if (commandLine.text != "") {
			inputHistory.Add (command);
		}
		history_state = inputHistory.Count;
	}

	public void SubmitUsername()
	{
		
		if (commandLine.text != "") 
		{
			username = commandLine.text;
			string url = "https://api.github.com/users/" + username;
			StartCoroutine(CheckGitHubUserExists(url));
			cm.ClearCommandLine ();
		}
	}

	public void SubmitToken()
	{

		if (commandLine.text != "") 
		{
			token = commandLine.text;
			cm.PrintToConsole ("*****************");
			string url = "https://api.github.com/users/" + username + "?access_token=" + token;
			StartCoroutine(GetGitHubUser(url));
			cm.ClearCommandLine ();
		}

	}

	public void ResizeImage()
	{
		profilePicture.GetComponent<RectTransform>().sizeDelta = new Vector2(75,75);
	}

	IEnumerator CheckGitHubUserExists(string url){
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.Send();

		if (www.isError) {
			Debug.Log (www.error);
		} else {
			var json = JSON.Parse (www.downloadHandler.text);

			if (json ["message"] != null) {
				cm.PrintToConsole ("\nError: " + json ["message"] + ". Please try again.\nUsername:");
			} else {
				cm.PrintToConsole (username+"\nAccess Token: ");
				cm.SetIntructionsText ("Type your GitHub Access Token, then press enter. Click <b>here</b> for help getting started.");
				state = 1;
			}
		}
	}

	public void InstructionsTextClicked()
	{
		if (this.state == 1) {
			Webpage helpPage = new Webpage ();
			helpPage.Open ("https://github.com/blog/1509-personal-api-tokens");
		}
	}

	IEnumerator GetGitHubUser(string url)
	{
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.Send();

		if (www.isError)
		{
			Debug.Log(www.error);
		}
		else
		{
			var json = JSON.Parse (www.downloadHandler.text);

			if (json["total_private_repos"].IsNull)
			{
				cm.PrintToConsole ("\nAccess token could not be verified. Please try again.\nAccess Token: ");

			} else if(json["message"] != null)
			{
				cm.PrintToConsole ("\nError: "+json ["message"]+". Please try again.\nAccess Token: ");
			} else 
			{
				user.populateGitHub(json);

				var www_image = new WWW(user.avatar_url);
				yield return www_image; // waits until image is downloaded

				//display image as a UI texture
				Texture2D texture = new Texture2D(1, 1);
				www_image.LoadImageIntoTexture(texture);
				profilePicture.GetComponent<RawImage> ().texture = texture;

				UpdateProfileDisplay ();

				cm.PrintToConsole("\nSuccess!\n");

				state = 2;
				api.GetUser (ref user);
			}
		}
	}

}
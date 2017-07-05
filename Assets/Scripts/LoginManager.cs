using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
	public string username;
	public string token;
	public User user;

	public InputField commandLine;
	public ConsoleManager cm;
	public GameObject profilePicture;
	public GameObject UserPrefab;
	public APIInterface api;

	int state = 0;

	// Use this for initialization
	void Start () 
	{
		api = GameObject.FindGameObjectWithTag ("API").GetComponent<APIInterface>();

		GameObject go = Instantiate (UserPrefab);
		user = go.AddComponent<User>();
		DontDestroyOnLoad (go);

		string message = "Welcome to Git-Good! Please sign in to your GitHub account to continue.\n\nUsername: ";
		cm.PrintToConsole (message);
	}

	void Update ()
	{
		
	}

	public void TryLogin()
	{
		if (Input.GetButtonDown ("Submit")) 
		{
			switch (state) {
			case 0:
				Debug.Log ("Username entered");
				SubmitUsername ();
				break;
			case 1:
				Debug.Log ("Token entered");
				SubmitToken ();
				break;
			case 2:
				api.UpdateExercises ();
				SceneManager.LoadScene("Exercise");
				break;
			}
		}
	}

	public void SubmitUsername()
	{
		if (commandLine.text != "") 
		{
			username = commandLine.text;
			string url = "https://api.github.com/users/" + username;
			StartCoroutine(CheckGitHubUserExists(url));
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
				state = 1;
			}
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

				cm.PrintToConsole("\nSuccess!\n");
				state = 2;

				api.GetUser (ref user);
			}
		}
	}
}
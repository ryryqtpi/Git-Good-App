using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class LoginManager : MonoBehaviour
{
	public string username;
	public string token;
	public User user;

	public InputField commandLine;
	public ConsoleManager cm;
	public GameObject profilePicture;
	public GameObject gitHubUserPrefab;

	int state = 0;

	// Use this for initialization
	void Start () 
	{
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
			GameObject go = Instantiate (gitHubUserPrefab);
			User user = go.AddComponent<User>();

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
				//Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);
				//GetComponent<SpriteRenderer>().sprite = sprite;

				profilePicture.GetComponent<RawImage> ().texture = texture;
				//profilePicture.GetComponent<RawImage> ().SetNativeSize ();
				//ResizeImage();

				cm.PrintToConsole("\nSuccess!");
				state = 2;
			}
		}
	}
}
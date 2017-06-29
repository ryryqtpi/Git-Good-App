using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class LoginManager : MonoBehaviour
{
    public string username = "";
	public string token = "";

	public InputField commandLine;

	public ConsoleManager cm;

	public GameObject profilePicture;
	public GameObject gitHubUserPrefab;

	public bool usernameSubmited = false;
	public bool tokenSubmited = false;
	public bool authenticated = false;

	private bool flag = false;

	public User user;

	// Use this for initialization
	void Start () 
	{
		string message = "Welcome to Git-Good! Please sign in to your GitHub account to continue.\n\nUsername: ";
		cm.PrintToConsole (message);
	}

	void Update ()
	{
		if (!flag) 
		{
			if (usernameSubmited && tokenSubmited && authenticated) 
			{
				cm.PrintToConsole("\nSuccess!");
				flag = true;
			}
		}
	}

	public void TryLogin()
	{
		if (Input.GetButtonDown ("Submit")) 
		{
			if (!usernameSubmited) {
				SubmitUsername ();
			} else if (usernameSubmited && !tokenSubmited) {
				SubmitToken ();
			}
		}
	}

	public void SubmitUsername()
	{
		if (commandLine.text != "") 
		{
			username = commandLine.text;
			usernameSubmited = true;

			string message = username + "\nAccess Token: ";
			cm.PrintToConsole (message);

		}
	}

	public void SubmitToken()
	{
		if (commandLine.text != "") 
		{
			token = commandLine.text;
			tokenSubmited = true;

			cm.PrintToConsole ("*****************");

			AuthUser (username, token);
		}
	}

	public void AuthUser(string un, string tk)
	{
		string url_attribute = "https://api.github.com/users/" + un + "?access_token=" + tk;
		StartCoroutine(GetJSON(url_attribute));
	}	

	public void ResizeImage()
	{
		profilePicture.GetComponent<RectTransform>().sizeDelta = new Vector2(75,75);
	}

	IEnumerator GetJSON(string url)
	{
		GameObject go = Instantiate (gitHubUserPrefab);
		User user = go.AddComponent<User>();

		cm.PrintToConsole ("\nAccessing account information...");


		UnityWebRequest www = UnityWebRequest.Get(url);

		yield return www.Send();

		if (www.isError)
		{
			Debug.Log(www.error);
		}
		else
		{
			Debug.Log (www.downloadHandler.text);
			var json = JSON.Parse (www.downloadHandler.text);

			if (json["total_private_repos"].IsNull)
            {
                Debug.Log("Invalid Access Token for user " + this.username);
				cm.PrintToConsole ("\nAccess token could not be verified. Please try again.");

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

				authenticated = true;
            }
		}
	}
}

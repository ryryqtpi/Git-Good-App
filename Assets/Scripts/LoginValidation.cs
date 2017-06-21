using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoginValidation : MonoBehaviour {

	//public string token = "b3601abb8df2e1b2f128c03c148e221dfb89e1cf";
	public string username = "tammiet";
	public string avatar_url;
	public Image myImage;

	// Use this for initialization
	void Start () {
		string url_attribute = "https://api.github.com/users/" + username;
		StartCoroutine(GetJSON(url_attribute));

	}

	IEnumerator GetJSON(string url)
	{
		UnityWebRequest www = UnityWebRequest.Get(url);

		yield return www.Send();

		if (www.isError)
		{
			Debug.Log(www.error);
		}
		else
		{
			Debug.Log(www.downloadHandler.text);
			JsonUtility.FromJsonOverwrite(www.downloadHandler.text, this);
			string image_url = (this.avatar_url);
			var www_image = new WWW(image_url);
			yield return www_image;
			Texture2D texture = new Texture2D(1, 1);
			www_image.LoadImageIntoTexture(texture);
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);
			GetComponent<SpriteRenderer>().sprite = sprite;

		}
	}
}

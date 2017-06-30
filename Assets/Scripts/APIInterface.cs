using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using SimpleJSON;

public class APIInterface : MonoBehaviour {

//	const string BASE_URL = "http://localhost:5000/";
	const string BASE_URL = "https://super-confusing-baby.herokuapp.com/";

	User user;

	void Start () {
		
	}

	void Update ()
	{
		
	}

	public void GetExercises(int user_level=1){
		string exercise_url = BASE_URL+"exercises.json?difficulty="+user_level;
		StartCoroutine(GetJSON_Exercises(exercise_url));
	}

	public void GetUser(ref User new_user){
		user = new_user;
		Debug.Log ("Checking our API for "+user.username);
		string user_url = BASE_URL+"users.json?username="+user.username;
		Debug.Log ("GET request to our slow ass Heroku app... Could take up to 10s.");
		StartCoroutine(GetJSON_User(user_url));
	}

	IEnumerator GetJSON_User(string url){
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.Send();
		if (www.isError) {
			Debug.Log (www.error);
		} else {
			var json = JSON.Parse (www.downloadHandler.text);
			if (json ["Error"] != null) {
				Debug.Log (json ["Error"]);
			} else {
				user.populateAPI (json);
			}
		}
	}

    IEnumerator GetJSON_Exercises(string url)
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

			int count = json.Count;
			Exercise[] exercises = new Exercise[count];

			for (int e=0; e<count; e++) {
				GameObject go = new GameObject ("Exercise: "+json[e]["name"]);
				Exercise exercise = go.AddComponent<Exercise> ();
				exercise.populate (json [e]);
				exercises[e] = exercise;
			}

        }

    }
}

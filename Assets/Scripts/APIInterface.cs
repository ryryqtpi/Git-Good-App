using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using SimpleJSON;

public class APIInterface : MonoBehaviour {

//	const string BASE_URL = "http://localhost:3000/";
	const string BASE_URL = "https://super-confusing-baby.herokuapp.com/";

	User user;
	public ExerciseManager em;

	void Start () {
		em = GameObject.FindGameObjectWithTag ("ExerciseManager").GetComponent<ExerciseManager> ();
	}

	void Update ()
	{
		
	}

	public void UpdateExercises(){
		string exercise_url = BASE_URL+"exercises.json?level="+user.level;
		StartCoroutine(GetJSON_Exercises(exercise_url));
	}

	public void GetUser(ref User new_user){
		user = new_user;
		Debug.Log ("Checking API for "+user.username+ " via GET request to our slow ass Heroku app... Could take up to 10s.");
		StartCoroutine(GetJSON_User());
	}

	IEnumerator GetJSON_User(){
		string url = BASE_URL + "users.json?username=" + user.username;
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.Send();
		if (www.isError) {
			Debug.Log (www.error);
		} else {
			var json = JSON.Parse (www.downloadHandler.text);
			if (json ["Error"] != null) {
				Debug.Log (json ["Error"]);
				StartCoroutine (PostNewUser ());
			} else {
				Debug.Log ("Got user from API.");
				user.populateAPI (json);
				UpdateExercises ();
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
			Debug.Log ("Got "+exercises.Length+" exercises");
			em.SaveExercises (exercises);
        }
    }

	IEnumerator PostNewUser(){
		string url = BASE_URL + "users";
		WWWForm form = new WWWForm();
		form.AddField("commit", "Create User");
		form.AddField("user[username]", user.username);
		form.AddField("user[level]", "1");
		UnityWebRequest www = UnityWebRequest.Post(url, form);

		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log("User created!");
			StartCoroutine(GetJSON_User());
		}
	}

	public IEnumerator PostIncrementUserLevel(){
		user.level++;
		string url = BASE_URL + "users/" + user.id+"?level=" + user.level;
		byte[] myData = System.Text.Encoding.UTF8.GetBytes("level=" + user.level);
		UnityWebRequest www = UnityWebRequest.Put(url, myData);

		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log("User updated to level "+user.level);
			UpdateExercises ();
		}
	}
}

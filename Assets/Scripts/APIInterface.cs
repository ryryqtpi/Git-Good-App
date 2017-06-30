using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using SimpleJSON;

public class APIInterface : MonoBehaviour {
	
	void Start () {
		Debug.Log ("Started!");

		int user_level = 1;
		string exercise_url = "http://localhost:5000/exercises.json?difficulty="+user_level;
//		string exercise_url = "https://super-confusing-baby.herokuapp.com/exercises.json?difficulty="+user_level;

		StartCoroutine(GetJSON_Exercises(exercise_url));
	}

	void Update ()
	{
		
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

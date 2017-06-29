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
			List<Exercise> exercises = new List<Exercise> ();
			var json = JSON.Parse (www.downloadHandler.text);

			//Exercises
			for (int e=0; e<json.Count; e++) {
				var exerciseJSON = json[e];
				exercises.Add (
					new Exercise().populate(
						exerciseJSON["name"], exerciseJSON["description"], exerciseJSON["difficulty"]
					)
				);

				//Steps
				for(int s=0; s<exerciseJSON["steps"].Count; s++){
					var stepJSON = exerciseJSON["steps"][s];

					Debug.Log(step["instruction"]);

					//Commands
					for(int c=0; c<stepJSON["commands"].Count; c++){
						var command = stepJSON["commands"][c];

						Debug.Log(command["name"]);
					}
				}

			}
				
        }
    }
}

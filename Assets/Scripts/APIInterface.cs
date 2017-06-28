using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class APIInterface : MonoBehaviour {

	// Use this for initialization
	void Start () {

		
	}

    public void GetExercises()
    {
        string exercise_url_attribute = "https://super-confusing-baby.herokuapp.com/exercises/1.json";
        StartCoroutine(GetJSON_Exercises(exercise_url_attribute));

        string steps_url_attribute = "https://super-confusing-baby.herokuapp.com/exercises/1/steps.json";
        StartCoroutine(GetJSON_Steps(steps_url_attribute));

        //string commands_url_attribute = "https://super-confusing-baby.herokuapp.com/exercises/1/steps/1/commands.json";
        //StartCoroutine(GetJSON_Steps(commands_url_attribute));
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
            Debug.Log(www.downloadHandler.text);
            ExerciseAPI exercise = JsonUtility.FromJson<ExerciseAPI>(www.downloadHandler.text);

            //Player[] player = JsonHelper.FromJson<Player>(jsonString);
            //ExerciseAPI e = JsonHelper.FromJson<ExerciseAPI>(www.downloadHandler.text);

            Debug.Log("Exercise: " + exercise.name);
            Debug.Log("Description: " + exercise.description);
            Debug.Log("Difficulty: " + exercise.difficulty);

        }
    }
    IEnumerator GetJSON_Steps(string url)
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
            string step_object = Regex.Match(www.downloadHandler.text, @"\[(.*?)\]").Groups[1].Value;
            var regex = new Regex("{.*?}");
            var matches = regex.Matches(step_object);
            int number = 2;
            StepAPI[] stepArray = new StepAPI[2];
            for (int i = 0; i < number; i++)
            {

                stepArray[i] = JsonUtility.FromJson<StepAPI>(matches[i].ToString());
                Debug.Log(stepArray[i].correct_response);
            }

        }
    }
    IEnumerator GetJSON_Commands(string url)
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
            //CommandAPI Command = JsonUtility.FromJson<CommandAPI>(www.downloadHandler.text);
            CommandAPI[] Command = JsonHelper.FromJson<CommandAPI>(www.downloadHandler.text);
            Debug.Log("CommandID: " + Command[0].id);
            Debug.Log("Command name: " + Command[0].name);
            Debug.Log("Command url: " + Command[0].url);

        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}

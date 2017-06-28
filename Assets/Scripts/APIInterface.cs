using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class APIInterface : MonoBehaviour {

    public void GetExercises()
    {
        string exercise_url_attribute = "https://super-confusing-baby.herokuapp.com/exercises/1.json";
        StartCoroutine(GetJSON_Exercises(exercise_url_attribute));

        string steps_url_attribute = "https://super-confusing-baby.herokuapp.com/exercises/1/steps.json";
        StartCoroutine(GetJSON_Steps(steps_url_attribute));

        string commands_url_attribute = "https://super-confusing-baby.herokuapp.com/exercises/1/steps/1/commands.json";
        StartCoroutine(GetJSON_Commands(commands_url_attribute));
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
            //Debug.Log(www.downloadHandler.text);
            ExerciseAPI exercise = JsonUtility.FromJson<ExerciseAPI>(www.downloadHandler.text);
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
            //Debug.Log(www.downloadHandler.text);
            MatchCollection matches = JsonParser.FromJson(www.downloadHandler.text); //converts JSON arrays to a MatchCollection object
            var step_list = new List<StepAPI>();
            foreach (var item in matches) //Converts MatchesCollection object to a list of StepAPI objects
            {
                var step_class = new StepAPI();
                step_class = JsonUtility.FromJson < StepAPI > (item.ToString());
                step_list.Add(step_class);
            }
        }
    }
    IEnumerator GetJSON_Commands(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.Send();

        if (www.isError)
        {
            Debug.Log("error" + www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
            MatchCollection matches = JsonParser.FromJson(www.downloadHandler.text); //converts JSON arrays to a MatchCollection 
            var command_list = new List<CommandAPI>();
            foreach (var item in matches) //Converts MatchesCollection object to a list of CommandAPI objects
            {
                var command_class = new CommandAPI();
                command_class = JsonUtility.FromJson<CommandAPI>(item.ToString());
                command_list.Add(command_class);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
/* Parses arrays of JSON objects and returns a MatchesCollection object
 * 
 * */
public class JsonParser {
    public static MatchCollection FromJson(string json)
    {
        string step_object = Regex.Match(json, @"\[(.*?)\]").Groups[1].Value;
        var regex = new Regex("{.*?}");
        var matches = regex.Matches(step_object);
        return (matches);
    }
}

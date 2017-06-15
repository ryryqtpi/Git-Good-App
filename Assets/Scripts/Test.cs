using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

class Test: MonoBehaviour {
	void Start() {
		StartCoroutine(GetText());
		Debug.Log ("Starting HTTP Request");
	}

	IEnumerator GetText() {
		UnityWebRequest www = UnityWebRequest.Get("https://api.github.com/users/cashc");
		yield return www.Send();

		if (www.isError) {
			Debug.Log (www.error);
		} else {
			Debug.Log(www.downloadHandler.text);
		}
	}
}
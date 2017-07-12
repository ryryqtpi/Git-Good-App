using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(User))]
public class ExpCalculator : MonoBehaviour {

	public User _user;
	public float _rawExp = 0.0f;
	public float _scaledExp = 0.0f;
	public float _followersMultiplier = 5001.0f;
	public float _followingMultiplier = 1001.0f;
	public float _privateReposMultiplier = 1001.0f;
	public float _publicReposMultiplier = 15001.0f;
	public float _publicGistsMultiplier = 2501.0f;
	public float _collaboratorsMultiplier = 10001.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		_user = GetComponent<User> ();
	}

	public float Calculate()
	{
		_rawExp = (int.Parse (_user.followers) * _followersMultiplier) + (int.Parse (_user.owned_private_repos) * _privateReposMultiplier) + (int.Parse (_user.public_repos) * _publicReposMultiplier) + (int.Parse (_user.public_gists) * _publicGistsMultiplier) + (int.Parse (_user.collaborators) * _collaboratorsMultiplier) + (int.Parse (_user.following) * _followingMultiplier);
		_scaledExp = (Mathf.Pow(2,_user.level) * 100) + _rawExp;
		Debug.Log ("level: " +_user.level);
		Debug.Log ("raw exp: " +_rawExp);
		Debug.Log ("scaled exp: " +_scaledExp);

		return _scaledExp;
	}

	public void SetUser(User _newUser)
	{
		_user = _newUser;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCalculator : MonoBehaviour {

	public User _user;
	public float _experience = 0.0f;
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
		_experience = (int.Parse(_user.followers) * _followersMultiplier) + (int.Parse(_user.owned_private_repos) * _privateReposMultiplier) + (int.Parse(_user.public_repos) * _publicReposMultiplier) + (int.Parse(_user.public_gists) * _publicGistsMultiplier) + (int.Parse(_user.collaborators) * _collaboratorsMultiplier) + (int.Parse(_user.following) * _followingMultiplier);
		Debug.Log (_experience);
		return _experience;
	}

	public void SetUser(User _newUser)
	{
		_user = _newUser;
	}
}

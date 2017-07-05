using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class User : MonoBehaviour {
	
	public string id;
	public int level;
	public string api_created_at;
	public string api_updated_at;
	public string username;
	public string github_id;
	public string avatar_url;
	public string html_url;
	public string full_name;
	public string company;
	public string blog;
	public string location;
	public string email;
	public string hireable;
	public string bio;
	public string public_repos;
	public string public_gists;
	public string followers;
	public string following;
	public string created_at;
	public string updated_at;
	public string total_private_repos;
	public string owned_private_repos;
	public string collaborators;
	public string two_factor_authentication;

	public void populateGitHub(JSONNode json){
		this.username = json["login"];
		this.github_id = json["id"];
		this.avatar_url = json["avatar_url"];
		this.html_url = json["html_url"];
		this.full_name = json["name"];
		this.company = json["company"];
		this.blog = json["blog"];
		this.location = json["location"];
		this.email = json["email"];
		this.hireable = json["hireable"];
		this.bio = json["bio"];
		this.public_repos = json["public_repos"];
		this.public_gists = json["public_gists"];
		this.followers = json["followers"];
		this.following = json["following"];
		this.created_at = json["created_at"];
		this.updated_at= json["updated_at"];
		this.total_private_repos = json["total_private_repos"];
		this.owned_private_repos = json["owned_private_repos"];
		this.collaborators = json["collaborators"];
		this.two_factor_authentication = json["two_factor_authentication"];
	}

	public void populateAPI(JSONNode json){
		this.id = json ["id"];
		this.level = (int)json ["level"];
		this.api_updated_at = json ["created_at"];
		this.api_created_at = json ["updated_at"];

	}

	public override string ToString(){
		string ret = "username: " + username;
		ret += "\nid: " + id;
		ret += "\nlevel: " + level;
		return ret;
	}

	public string StringRepresentation(){
		string ret = "username: " + username;
		ret += "\nid: " + id;
		ret += "\nlevel: " + level;
		return ret;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}

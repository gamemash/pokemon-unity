using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;
using UnityEngine.Playables;

public class DoorEntering : MonoBehaviour {
	
	public string EntranceName;
	public GameObject Player;

	// Use this for initialization
	void Start ()
	{
		if (Data.Get("Entrance") == EntranceName) {
			Player.GetComponent<PlayerControllerScript>().SetPosition(transform.position - new Vector3(0.5f, 0.5f, 0.5f));
		}
	}
	
	
	// Update is called once per frame
	void Update()
	{
		
	}
}

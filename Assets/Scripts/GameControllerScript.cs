using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
	
	// Use this for initialization
	void Start ()
	{
		//SpawnPlayer();
	}

	private void SpawnPlayer()
	{
		var playerPrefab = Resources.Load("Prefabs/AshContainer") as GameObject;
		var player = Instantiate(playerPrefab);
		player.name = "PlayerContainer";
	}

	// Update is called once per frame
	void Update () {
		// if (Input.GetButtonDown("Submit")) {
		// 	Player.GetComponent<PlayerControllerScript>().ReceiveInput = !Player.GetComponent<PlayerControllerScript>().ReceiveInput;
		// }
	}
}

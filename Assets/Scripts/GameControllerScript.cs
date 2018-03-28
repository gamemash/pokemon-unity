using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameControllerScript : MonoBehaviour
{
	// Use this for initialization
	void Awake ()
	{
		SpawnPlayer();
	}

	private void SpawnPlayer()
	{
		var playerPrefab = Resources.Load("Prefabs/AshContainer") as GameObject;
		var playerContainer = Instantiate(playerPrefab);
		playerContainer.name = "PlayerContainer";
	}

	// Update is called once per frame
	void Update () {
		// if (Input.GetButtonDown("Submit")) {
		// 	Player.GetComponent<PlayerControllerScript>().ReceiveInput = !Player.GetComponent<PlayerControllerScript>().ReceiveInput;
		// }
	}


	public static void BindPlayerToDirector(PlayableDirector director)
	{
        foreach (var output in director.playableAsset.outputs) {
            // identify the tracks that you want to bind
            switch (output.streamName) {
                case "Player":
                    director.SetGenericBinding(output.sourceObject, GameObject.Find("PlayerContainer/Ash"));
                    break;
                case "PlayerContainer":
                    director.SetGenericBinding(output.sourceObject, GameObject.Find("PlayerContainer"));
                    break;
            }
        }
	}
}

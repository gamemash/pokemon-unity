using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{

	public GameObject Player;

	// Use this for initialization
	void Start ()
	{
		var AshPrefab = Resources.Load("Prefabs/AshContainerPrefab") as GameObject;
		var gameObject =  Instantiate(AshPrefab);
		gameObject.name = "PlayerContainer";
	}
	
	// Update is called once per frame
	void Update () {
		// if (Input.GetButtonDown("Submit")) {
		// 	Player.GetComponent<PlayerControllerScript>().ReceiveInput = !Player.GetComponent<PlayerControllerScript>().ReceiveInput;
		// }
	}
}

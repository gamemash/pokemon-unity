using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameControllerScript : MonoBehaviour
{

	public GameObject Camera;
	private GameObject _player;
	
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
		_player = playerContainer.transform.Find("Ash").gameObject;
		Camera.transform.parent = _player.transform;
	}

	private void SpawnBattleTransition()
	{
		var prefab = Resources.Load("Prefabs/BattleTransitionContainer") as GameObject;
		Instantiate(prefab);
	}

	public void DemandFocus()
	{
		GetPlayer().ReceiveInput = false;
	}

	public void ReleaseFocus()
	{
		
		GetPlayer().ReceiveInput = true;
	}

	private PlayerControllerScript GetPlayer()
	{
		return _player.GetComponent<PlayerControllerScript>();
	}
	

	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown("Submit"))
		{
			//SpawnBattleTransition();
		}
		
	}

	public static GameControllerScript GetActiveGameController()
	{
		return GameObject.Find("GameController").GetComponent<GameControllerScript>();
	}
}

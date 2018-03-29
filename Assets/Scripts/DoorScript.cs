using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{


	public string EntranceName;
	public string SceneName;
	private Animator _animator;

	// Use this for initialization
	void Start ()
	{
		_animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
    public void GoThrough(GameObject player)
    {
	    StartCoroutine(ThroughDoorAnimation(player.GetComponent<PlayerControllerScript>()));
    }
	
    IEnumerator ThroughDoorAnimation(PlayerControllerScript player)
    {
	    _animator.SetBool("Open", true);
        yield return new WaitForSeconds(0.5f);
	    player.MoveForward(2);
        yield return new WaitForSeconds(2);
	    _animator.SetBool("Open", false);
	    yield return new WaitForSeconds(0.5f);
	    LoadScene();
	    
    }
	
    private void LoadScene()
    {
        Data.Set("Entrance", EntranceName);
        SceneManager.LoadScene(SceneName);
    }
}

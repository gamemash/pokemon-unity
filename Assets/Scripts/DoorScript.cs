using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
	private bool _open = false;
	private Animator _animator;

	// Use this for initialization
	void Start ()
	{
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space)) {
			
			_open = !_open;
			_animator.SetBool("Open", _open);
		}
		
	}
}

using System;
using lib;
using UnityEngine;
using UnityEngine.Playables;

public class Arrival : MonoBehaviour
{
    public string EntranceName;

    private bool _playing;
    private float _sinceStartOfAnimation;
    private PlayableDirector _director;

    public void Start()
    {
        _director = GetComponent<PlayableDirector>();

        if (Data.Get("Entrance") == EntranceName) {
            SetPosition();
            if (_director != null)
                PlayAnimation();
        }
    }

    public void FixedUpdate()
    {
        if (_playing) {
            _sinceStartOfAnimation += Time.fixedDeltaTime;
            if (_sinceStartOfAnimation > _director.duration) {
                _playing = false;
                SetPosition();
            }
        }
    }

    private void SetPosition()
    {
        GameObject.Find("PlayerContainer/Ash").GetComponent<PlayerControllerScript>().SetPosition(transform.position);
    }

    private void PlayAnimation()
    {
        GameControllerScript.BindPlayerToDirector(_director);
        _playing = true;
        _director.Play();
    }
}
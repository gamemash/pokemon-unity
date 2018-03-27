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
        // foreach (var output in _director.playableAsset.outputs)
        // U//{
        // U//    Debug.Log(output.streamName);
        // U//    // identify the tracks that you want to bind
        // U//    //if (output.streamName.StartsWith("BindMe"))
        // U//    //{
        // U//    //    // go.GetComponent<> may be necessary if the track uses a component and
        // U//    //    // not a game object
        // U//    //    director.SetGenericBinding(output.sourceObject, go);
        // U//    //}
        // U//}
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
        
        GameObject.Find("PlayerController/Ash").GetComponent<PlayerControllerScript>().SetPosition(transform.position);
    }

    private void PlayAnimation()
    {
        _playing = true;
        _director.Play();
    }
}
using UnityEngine;
using UnityEngine.Playables;

namespace lib
{
    public class PathwayWithDirector: MonoBehaviour
    {
        public string EntranceName;
        public GameObject Player;
        private PlayableDirector _director;
        

        private float _startOfAnimation;
        private bool _playing;

        // Use this for initialization
        void Start ()
        {
            _director = GetComponent<PlayableDirector>();
            if (Data.Get("Entrance") == EntranceName) {
                
                Player.GetComponent<PlayerControllerScript>().SetPosition(transform.position - new Vector3(0.5f, 0.5f));
                _startOfAnimation = 0.0f;
                _director.Play();
                _playing = true;
            }
        }
        
        // Update is called once per frame
        void FixedUpdate ()
        {
            _startOfAnimation += Time.fixedDeltaTime;
            if (_startOfAnimation > _director.duration && _playing) {
                Debug.Log("end of animation");
                _playing = false;
                Player.GetComponent<PlayerControllerScript>().SetPosition(transform.position - new Vector3(0.5f, 0.5f));
            }
        }
        
    }
}
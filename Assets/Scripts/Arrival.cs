using lib;
using UnityEngine;
using UnityEngine.Playables;

namespace DefaultNamespace
{
    public class Arrival: MonoBehaviour
    {
        public string EntranceName;
        public GameObject Player;
        
        private bool _playing;
        private float _sinceStartOfAnimation;
        private PlayableDirector _director;

        public void Start()
        {
            _director = GetComponent<PlayableDirector>();
            if (Data.Get("Entrance") == EntranceName)
            {
                SetPosition();
                if (_director != null)
                    PlayAnimation();
            }
        }
        
        public void FixedUpdate()
        {
            if (_playing)
            {
                _sinceStartOfAnimation += Time.fixedDeltaTime;
                if (_sinceStartOfAnimation > _director.duration)
                {
                    _playing = false;
                    SetPosition();
                }
            }
        }

        private void SetPosition()
        {
            Player.GetComponent<PlayerControllerScript>().SetPosition(transform.position);
        }

        private void PlayAnimation()
        {

            _playing = true;
            _director.Play();
        }
    }
}
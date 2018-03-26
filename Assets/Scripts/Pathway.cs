using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace lib
{
    public class Pathway: MonoBehaviour
    {
        public string GoToScene;
        public string EntranceName;
        
        private PlayableDirector _director;
        private float _startOfAnimation;
        private bool _playing;

        public virtual void Start ()
        {
            _director = GetComponent<PlayableDirector>();
        }
        
        public void GoThrough(GameObject gameObject)
        {
            _director.Play();
            _startOfAnimation = 0.0f;
            _playing = true;
        }
        
        
        // Update is called once per frame
        void FixedUpdate ()
        {
            _startOfAnimation += Time.fixedDeltaTime;
            if (_playing && _startOfAnimation > _director.duration) {
                LoadScene();
            }
        }

        public virtual void LoadScene()
        {
            Data.Set("Entrance", EntranceName);
            SceneManager.LoadScene(GoToScene);
        }

    }
}
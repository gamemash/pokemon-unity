using lib;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    public string SceneName;
    public string EntranceName;
    private bool _playing;
    private float _sinceStartOfAnimation;
    private PlayableDirector _director;

    public void Start()
    {
        _director = GetComponent<PlayableDirector>();
        if (_director != null)
        {
            foreach (var output in _director.playableAsset.outputs)
            {
                Debug.Log(output.sourceObject);
            }
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
                LoadScene();
            }
        }
    }

    public void PlayAnimation()
    {

        _playing = true;
        _director.Play();
    }

    public void GoThrough()
    {
        if (_director == null)
        {
            LoadScene();
        }
        else
        {
            PlayAnimation();
        }
    }

    private void LoadScene()
    {
        Data.Set("Entrance", EntranceName);
        SceneManager.LoadScene(SceneName);
    }
}

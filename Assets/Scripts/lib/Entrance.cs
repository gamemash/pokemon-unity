using System.Collections;
using lib;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    public string SceneName;
    public string EntranceName;
    
    public void Start()
    {
        if (Data.Get("Entrance") == EntranceName) {
            StartCoroutine(PlayArrivalAnimation(GameObject.Find("PlayerContainer/Ash")));
        }
    }
    
    public virtual IEnumerator PlayArrivalAnimation(GameObject gameObject)
    {
        SetPosition(gameObject);
        LoadScene();
        yield return null;
    }

    public virtual IEnumerator PlayEntranceAnimation(GameObject gameObject)
    {
        LoadScene();
        yield return null;
    }

    public void GoThrough(GameObject gameObject)
    {
        StartCoroutine(PlayEntranceAnimation(gameObject));
    }

    public void LoadScene()
    {
        Data.Set("Entrance", EntranceName);
        SceneManager.LoadScene(SceneName);
    }
    
    private void SetPosition(GameObject gameobject)
    {
        gameobject.GetComponent<PlayerControllerScript>().SetPosition(transform.position);
    }
}
